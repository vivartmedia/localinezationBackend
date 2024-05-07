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

        public bool AddMediaItem(MediaItemModel newMediaItem)
        {
            _context.Add(newMediaItem);
            return _context.SaveChanges() != 0;
        }

        public IEnumerable<MediaItemModel> GetAllMediaItems()
        {
            return _context.MediaInfo;
        }

        public IEnumerable<MediaItemModel> GetItemsByUserId(int userId)
        {
            return _context.MediaInfo.Where(item => item.UserID == userId);
        }

        public IEnumerable<MediaItemModel> GetItemsByOriginalLanguage(string originallanguage)
        {
            return _context.MediaInfo.Where(item => item.OriginalLanguage == originallanguage);
        }
         public IEnumerable<MediaItemModel> GetPublishedItems()
        {
            return _context.MediaInfo.Where(item => item.IsPublished == true);
        }
        public List<MediaItemModel> GetAllItemsByTags(string tag)
        {
            var allItems = GetAllMediaItems().ToList();
            var filteredItems = allItems.Where(item => item.Tags.Split(",").Contains(tag)).ToList();
            return filteredItems;
        }
        public MediaItemModel GetMediaItemById(int id)
        {
            return _context.MediaInfo.SingleOrDefault(item => item.ID == id);
        }
        public bool UpdateMediaItem(MediaItemModel mediaUpdate)
        {
            _context.Update<MediaItemModel>(mediaUpdate);
            return _context.SaveChanges() != 0;
        }
        public bool DeleteMediaItem(MediaItemModel mediaToDelete)
        {
            mediaToDelete.IsDeleted = true;
            _context.Update<MediaItemModel>(mediaToDelete);
            return _context.SaveChanges() != 0;
        }

    }
}