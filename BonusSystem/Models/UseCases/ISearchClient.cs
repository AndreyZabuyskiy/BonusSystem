using System.Threading.Tasks;

namespace BonusSystem.Models.UseCases
{
    public interface ISearchClient
    {
        public Task<Client> SearchByPhoneNumberAsync(string phoneNumber);
        public Task<Client> SearchByNumberCardAsync(int numberCard);
    }
}
