using System.Collections.Generic;
using System.Threading.Tasks;
using localinezationBackend.Models;
using localinezationBackend.Services.Context;
using Microsoft.EntityFrameworkCore;

namespace Backend_localinezationBackend.Services
{
    public class MediaService
    {
        private readonly DataContext _context;

        public MediaService(DataContext context)
        {
            _context = context;
        }

        public async Task<List<Media>> GetAllMediaAsync()
        {
            return await _context.Medias.ToListAsync();
        }

        // Add more methods here as needed
    }
}