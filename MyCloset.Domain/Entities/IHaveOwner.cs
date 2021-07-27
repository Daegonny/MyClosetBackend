
namespace MyCloset.Domain.Entities
{
	public interface IHaveOwner
	{
		Account Account { get; }
		bool AssertIsOwnedBy(Account account);
	}
}
