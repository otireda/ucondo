using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Principal;
using Ardalis.SharedKernel;
using Ucondo.Core.AccountAggregate.ValueObjects;
using Ucondo.Core.Enums;

namespace Ucondo.Core.AccountAggregate;

public class Account : EntityBase, IAggregateRoot
{
	public string Code { get; private set; } = null!;
	public string Name { get; private set; } = null!;
	public bool AllowsPostings { get; private set; }
	public AccountType Type { get; private set; }
	public string? ParentCode { get; private set; }
	public int Depth { get; set; }
	public Account? Parent { get; private set; }

	public Account() {}

	public Account(string code, string name, bool allowsPostings, AccountType type, int depth, Account? parent)
	{
		Code = code;
		Name = name;
		AllowsPostings = allowsPostings;
		Type = type;
		Parent = parent;
		ParentCode = parent?.Code;
		Depth = depth;
	}

	public static Account Create(
		int depth,
		string code,
		string name,
		bool allowsPostings,
		AccountType type,
		Account? parent,
		bool parentAcceptsChildren,
		bool codeIsUnique,
		bool sameTypeAsParent,
		bool codeMatchesParentPrefix)
	{
		if (!codeIsUnique) throw new Exception("O código da conta já existe.");
		if (parent != null)
		{
			if (!parentAcceptsChildren) throw new Exception("A conta pai não aceita contas filhas.");
			if (!sameTypeAsParent) throw new Exception("O tipo da conta filha deve ser igual ao tipo da conta pai.");
			if (!codeMatchesParentPrefix) throw new Exception("O código da conta filha deve começar com o código da conta pai.");
		}

		return new Account(code, name, allowsPostings, type, depth, parent);
	}

	public void UpdateName(string newName) => Name = newName;

	public void SetAllowsPostings(bool allows, bool hasChildren)
	{
		if (allows && hasChildren)
			throw new Exception("Contas com contas filhas não podem permitir lançamentos diretos.");
		AllowsPostings = allows;
	}
}