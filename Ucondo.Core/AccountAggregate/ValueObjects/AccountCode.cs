using Ardalis.SharedKernel;

namespace Ucondo.Core.AccountAggregate.ValueObjects;

public class AccountCode : ValueObject, IComparable<AccountCode>
{
	public IReadOnlyList<int> Segments { get; }

	private AccountCode(IEnumerable<int> segments) => Segments = segments.ToArray();

	public static AccountCode Parse(string code)
	{
		if (string.IsNullOrWhiteSpace(code)) throw new ArgumentException("O codigo é obrigatório.");
		var parts = code.Split('.');
		var segs = parts.Select(p =>
		{
			if (!int.TryParse(p, out var n) || n < 0 || n > 999)
				throw new ArgumentException("Cada segmento tem de estar dentro: 0..999.");
			return n;
		}).ToArray();
		return new AccountCode(segs);
	}

	public override string ToString() => string.Join('.', Segments);

	public int Depth => Segments.Count;

	public bool StartsWith(AccountCode parent)
	{
		if (parent.Segments.Count >= Segments.Count) return false;
		for (int i = 0; i < parent.Segments.Count; i++)
			if (Segments[i] != parent.Segments[i])
				return false;
		return true;
	}

	protected override IEnumerable<object> GetEqualityComponents() => (IEnumerable<object>)Segments;

	public static AccountCode CreateChild(AccountCode parent, int childNumber /*0..999*/)
	{
		if (childNumber is < 0 or > 999) throw new ArgumentOutOfRangeException(nameof(childNumber));
		var newSegs = parent.Segments.Concat(new[] { childNumber });
		return new AccountCode(newSegs);
	}

	public int CompareTo(AccountCode? other)
	{
		if (other is null) return 1;
		var a = Segments;
		var b = other.Segments;
		var len = Math.Min(a.Count, b.Count);
		for (int i = 0; i < len; i++)
		{
			var cmp = a[i].CompareTo(b[i]);
			if (cmp != 0) return cmp;
		}

		return a.Count.CompareTo(b.Count);
	}
}