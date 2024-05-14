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
        private readonly ILogger<MediaService> _logger;

        public MediaService(DataContext context, ILogger<MediaService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public bool AddMediaItem(MediaItemModel newMediaItem)
        {
            _context.Add(newMediaItem);
            return _context.SaveChanges() > 0;
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

        // public List<MediaItemModel> GetAllItemsByTags(string tag)
        // {
        //     return _context.MediaInfo
        //                    .Where(item => item.Tags != null && item.Tags.Contains(tag))
        //                    .ToList();
        // }

        public MediaItemModel? GetMediaItemById(int id)
        {
            return _context.MediaInfo.SingleOrDefault(item => item.ID == id);
        }


        public bool UpdateMediaItem(MediaItemModel mediaUpdate)
        {
            _context.Update(mediaUpdate);
            return _context.SaveChanges() > 0;
        }

        public bool DeleteMediaItem(int mediaId)
        {
            var mediaItem = _context.MediaInfo.SingleOrDefault(item => item.ID == mediaId);
            if (mediaItem != null)
            {
                mediaItem.IsDeleted = true;
                _context.Update(mediaItem);
                return _context.SaveChanges() > 0;
            }
            return false;
        }


        public bool AddTranslationRequest(TranslationRequestModel request)
        {
            _context.TranslationRequests.Add(request);
            return _context.SaveChanges() > 0;
        }

        public bool AddTranslation(TranslationModel translation)
        {
            var requestExists = _context.TranslationRequests.Any(tr => tr.Id == translation.TranslationRequestId);
            if (!requestExists)
            {
                throw new InvalidOperationException("Translation request not found.");
            }

            try
            {
                _context.Add(translation);
                return _context.SaveChanges() > 0;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Failed to add translation.");
                throw new InvalidOperationException("Could not add translation. Please check the details and try again.");
            }
        }


        public IEnumerable<MediaItemModel> GetUserMediaWithTranslations(int userId)
        {
            return _context.MediaInfo
                           .Include(m => m.TranslationRequests!)
                           .ThenInclude(tr => tr!.Translations!)
                           .Where(m => m.UserID == userId)
                           .ToList();
        }



        public IEnumerable<MediaItemModel> GetTranslatedMediaByUser(int translatorUserId)
        {
            return _context.Translations
                           .Include(t => t.TranslationRequest)
                           .ThenInclude(tr => tr!.Media) // Asserting tr is not null
                           .Where(t => t.TranslatorUserId == translatorUserId &&
                                       t.TranslationRequest != null &&
                                       t.TranslationRequest.Media != null)
                           .Select(t => t.TranslationRequest!.Media!) // null-forgiving operator, asserting non-null
                           .Distinct()
                           .ToList();
        }


        public IEnumerable<TranslationRequestModel> GetTranslationRequestsByUserId(int userId)
        {
            return _context.TranslationRequests
                           .Include(tr => tr.Media)
                           .Where(tr => tr.RequestorUserId == userId)
                           .ToList();
        }

        public IEnumerable<TranslationRequestModel> GetTranslationRequestsByMediaId(int mediaId)
        {
            return _context.TranslationRequests
                           .Include(tr => tr.Media)
                           .Where(tr => tr.MediaId == mediaId)
                           .ToList();
        }

        public IEnumerable<TranslationModel> GetTranslationsByRequestId(int requestId)
        {
            return _context.Translations
                           .Where(t => t.TranslationRequestId == requestId)
                           .ToList();
        }

        public IEnumerable<TranslationModel> GetTranslationsByTranslatorUserId(int translatorUserId)
        {
            // This approach uses the null-forgiving operator to suppress nullable warnings after thorough checks.
            return _context.Translations
                           .Include(t => t.TranslationRequest!)
                               .ThenInclude(tr => tr.Media!)
                           .Where(t => t.TranslatorUserId == translatorUserId &&
                                       t.TranslationRequest != null &&
                                       t.TranslationRequest.Media != null)
                           .Select(t => t) // Assuming you are returning the TranslationModel directly
                           .ToList();
        }


    }

}
