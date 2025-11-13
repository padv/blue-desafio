using AgendaApp.Domain.Entities;

namespace AgendaApp.Domain.Interfaces
{
    public interface IContactRepository
    {
        Task<Contact?> GetByIdAsync(Guid id);
        Task<IEnumerable<Contact>> GetAllAsync();
        Task<bool> ExistsByEmailAsync(string email);
        Task AddAsync(Contact contact);
        Task UpdateAsync(Contact contact);
        Task SaveChangesAsync();
        Task<List<Contact>> SearchAsync(string term);
    }
}