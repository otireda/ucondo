using Ardalis.SharedKernel;
using Ucondo.Core.AccountAggregate.ValueObjects;
using Ucondo.Core.Enums;

namespace Ucondo.Core.AccountAggregate;

public class Account : EntityBase, IAggregateRoot
{
	public AccountCode Code { get; private set; } = default!;
	public string Name { get; private set; } = default!;
	public bool AllowsPostings { get; private set; }
	public AccountType Type { get; private set; }
	public int? ParentId { get; private set; }
	public Account? Parent { get; private set; }

	public Account() {}

	public Account(AccountCode code, string name, bool allowsPostings, AccountType type, Account? parent)
	{
		Code = code;
		Name = name;
		AllowsPostings = allowsPostings;
		Type = type;
		Parent = parent;
		ParentId = parent?.Id;
	}

	public static Account Create(
		AccountCode code,
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

		return new Account(code, name, allowsPostings, type, parent);
	}

	public void UpdateName(string newName) => Name = newName;

	public void SetAllowsPostings(bool allows, bool hasChildren)
	{
		if (allows && hasChildren)
			throw new Exception("Contas com contas filhas não podem permitir lançamentos diretos.");
		AllowsPostings = allows;
	}
}