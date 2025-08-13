using System.Reflection;
using Ardalis.ListStartupServices;
using Ardalis.SharedKernel;
using FastEndpoints;
using FastEndpoints.Swagger;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Ucondo.Core.AccountAggregate;
using Ucondo.Infrastructure.Data;

var builder = WebApplication.CreateBuilder();

builder.Services.Configure<CookiePolicyOptions>(options =>
{
	options.CheckConsentNeeded = context => true;
	options.MinimumSameSitePolicy = SameSiteMode.None;
});

builder.Services.AddFastEndpoints()
	.SwaggerDocument(o => { o.ShortSchemaNames = true; });

ConfigureMediatR();

builder.Services.AddInfrastructureServices(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
	app.UseDeveloperExceptionPage();
	// app.UseShowAllServicesMiddleware(); // see https://github.com/ardalis/AspNetCoreStartupServices
}
else
{
	app.UseDefaultExceptionHandler(); // from FastEndpoints
	app.UseHsts();
}

app.UseFastEndpoints();
app.Run();

void ConfigureMediatR()
{
	var mediatRAssemblies = new[]
	{
		Assembly.GetAssembly(typeof(Account)), // Core
		// Assembly.GetAssembly(typeof(CreateContributorCommand)) // UseCases
	};
	builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(mediatRAssemblies!));
	builder.Services.AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
	builder.Services.AddScoped<IDomainEventDispatcher, MediatRDomainEventDispatcher>();
}

void AddShowAllServicesSupport()
{
	// add list services for diagnostic purposes - see https://github.com/ardalis/AspNetCoreStartupServices
	builder.Services.Configure<ServiceConfig>(config =>
	{
		config.Services = new List<ServiceDescriptor>(builder.Services);

		// optional - default path to view services is /listallservices - recommended to choose your own path
		config.Path = "/listservices";
	});
}