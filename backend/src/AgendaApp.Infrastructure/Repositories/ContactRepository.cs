
using AgendaApp.Domain.Entities;
using AgendaApp.Domain.Interfaces;
using AgendaApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AgendaApp.Infrastructure.Repositories
{
    public class ContactRepository : IContactRepository
    {
        private readonly ApplicationDbContext _context;

        public ContactRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Contact?> GetByIdAsync(Guid id)
        {
            return await _context.Contacts.FindAsync(id);
        }

        public async Task<IEnumerable<Contact>> GetAllAsync()
        {
            return await _context.Contacts
                .OrderBy(c => c.Nome)
                .ToListAsync();
        }

        public async Task<bool> ExistsByEmailAsync(string email)
        {
            return await _context.Contacts
                .AnyAsync(c => c.Email == email);
        }

        public async Task AddAsync(Contact contact)
        {
            await _context.Contacts.AddAsync(contact);
        }

        public async Task UpdateAsync(Contact contact)
        {
            _context.Contacts.Update(contact);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<List<Contact>> SearchAsync(string term)
        {
        return await _context.Contacts
            .Where(c => c.Nome.ToLower().Contains(term) || c.Email.Contains(term, StringComparison.CurrentCultureIgnoreCase))
            .OrderBy(c => c.Nome)
            .ToListAsync();
        }
    }
}