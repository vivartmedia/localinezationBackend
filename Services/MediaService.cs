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

        //Leo requestd to get any media by it's id
        public MediaItemModel GetMediaItemById(int id)
        {
            return _context.MediaInfo.SingleOrDefault(item => item.ID == id);
        }

        //adds new translation request
        public bool AddTranslationRequest(TranslationRequestModel request)
        {
            _context.TranslationRequests.Add(request);
            return _context.SaveChanges() > 0;
        }


        //adds new Translation text
        public bool AddTranslation(TranslationModel translation)
        {
            _context.Add(translation);
            return _context.SaveChanges() != 0;
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



        // fetch all media items uploaded by a given user along with any translation requests and its translations
        public IEnumerable<MediaItemModel> GetUserMediaWithTranslations(int userId)
        {
            return _context.MediaInfo.Include(m => m.TranslationRequests).ThenInclude(tr => tr.Translations).Where(m => m.UserID == userId).ToList();
        }

        //fetch translations by a Translator userId
        public IEnumerable<MediaItemModel> GetTranslatedMediaByUser(string translatorUserId)
        {
            // Fetch all translated media for a given user, ensuring all translation requests and their associated media items are non-null.
            return _context.Translations
                           .Include(t => t.TranslationRequest) // Include TranslationRequest to access related data
                           .ThenInclude(tr => tr.Media) // Include Media to access the media details
                           .Where(t => t.TranslatorUserId == translatorUserId) // Filter translations by the translator's user ID
                           .Where(t => t.TranslationRequest != null && t.TranslationRequest.Media != null) // Ensure TranslationRequest and Media are not null
                           .Select(t => t.TranslationRequest!.Media!) // Safely access Media with null-forgiving operators after checks
                           .Distinct() // Remove duplicate entries
                           .ToList(); // Execute query and convert to list
        }





        public IEnumerable<TranslationRequestModel> GetUserTranslationRequests(int userId)
        {
            return _context.TranslationRequests
                           .Include(tr => tr.Translations)
                           .Where(tr => tr.RequestorUserId == userId)
                           .ToList();
        }


    }
}