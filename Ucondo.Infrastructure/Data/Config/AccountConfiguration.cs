using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ucondo.Core.AccountAggregate;
using Ucondo.Core.AccountAggregate.ValueObjects;

namespace Ucondo.Infrastructure.Data.Config;

public class AccountConfiguration : IEntityTypeConfiguration<Account>
{
	public void Configure(EntityTypeBuilder<Account> builder)
	{
		builder.ToTable("Accounts");
		builder.HasKey(x => x.Code);

		builder.Ignore( x => x.Id );
		builder.Property(x => x.Name).HasMaxLength(200).IsRequired();

		var codeComparer = new ValueComparer<AccountCode>(
			(l, r) => l.ToString() == r.ToString(),
			v => v.ToString().GetHashCode(),
			v => AccountCode.Parse(v.ToString()));

		builder.Property(x => x.Code)
			.HasMaxLength(200)
			.IsRequired();

		builder.HasIndex(x => x.Code).IsUnique();

		builder.Property(x => x.Type).HasConversion<int>().IsRequired();
		builder.Property(x => x.AllowsPostings).IsRequired();

		builder.Property(x => x.ParentCode)
			.HasMaxLength(200);

		builder.Property(x => x.Depth);
		
		builder.HasOne(x => x.Parent)
			.WithMany()
			.HasForeignKey(x => x.ParentCode)
			.OnDelete(DeleteBehavior.Restrict);
	}
}