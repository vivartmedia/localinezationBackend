// Importing necessary namespaces and models for database interaction and logging
using localinezationBackend.Models;
using localinezationBackend.Models.DTO;
using localinezationBackend.Services.Context;
using Microsoft.EntityFrameworkCore;

// Define the namespace for service classes
namespace Backend_localinezationBackend.Services
{
    // MediaService class to handle media-related data operations
    public class MediaService
    {
        // Private field for database context
        private readonly DataContext _context;
        // Private field for logging
        private readonly ILogger<MediaService> _logger;

        // Constructor to inject dependencies
        public MediaService(DataContext context, ILogger<MediaService> logger)
        {
            _context = context;
            _logger = logger;
        }

        // Adds a new media item to the database
        public bool AddMediaItem(MediaItemModel newMediaItem)
        {
            _context.Add(newMediaItem);
            // Saves changes and checks if at least one record was affected
            return _context.SaveChanges() > 0;
        }

        // Retrieves all media items from the database
        public IEnumerable<MediaItemModel> GetAllMediaItems()
        {
            return _context.MediaInfo;
        }

        // Retrieves media items by user ID
        public IEnumerable<MediaItemModel> GetItemsByUserId(int userId)
        {
            return _context.MediaInfo.Where(item => item.UserID == userId);
        }

        // Retrieves media items by their original language
        public IEnumerable<MediaItemModel> GetItemsByOriginalLanguage(string originallanguage)
        {
            return _context.MediaInfo.Where(item => item.OriginalLanguage == originallanguage);
        }

        // Retrieves all published media items
        public IEnumerable<MediaItemModel> GetPublishedItems()
        {
            return _context.MediaInfo.Where(item => item.IsPublished == true);
        }

        // Retrieves a specific media item by its ID
        public MediaItemModel? GetMediaItemById(int id)
        {
            return _context.MediaInfo.SingleOrDefault(item => item.ID == id);
        }

        // Updates a media item in the database
        public bool UpdateMediaItem(MediaItemModel mediaUpdate)
        {
            _context.Update(mediaUpdate);
            return _context.SaveChanges() > 0;
        }

        // Marks a media item as deleted rather than removing it completely
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

        // Adds a new translation request to the database
        public bool AddTranslationRequest(TranslationRequestModel request)
        {
            _context.TranslationRequests.Add(request);
            return _context.SaveChanges() > 0;
        }

        // Adds a translation to the database after checking the corresponding request exists
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

        // Retrieves media items with translations by user ID
        public IEnumerable<MediaItemModel> GetUserMediaWithTranslations(int userId)
        {
            return _context.MediaInfo
                           .Include(m => m.TranslationRequests!)
                           .ThenInclude(tr => tr!.Translations!)
                           .Where(m => m.UserID == userId)
                           .ToList();
        }

        // Retrieves media items translated by a specific user
        public IEnumerable<MediaItemModel> GetTranslatedMediaByUser(int translatorUserId)
        {
            return _context.Translations
                           .Include(t => t.TranslationRequest)
                           .ThenInclude(tr => tr!.Media)
                           .Where(t => t.TranslatorUserId == translatorUserId &&
                                       t.TranslationRequest != null &&
                                       t.TranslationRequest.Media != null)
                           .Select(t => t.TranslationRequest!.Media!)
                           .Distinct()
                           .ToList();
        }

        // Retrieves translations associated with a specific request ID
        public IEnumerable<TranslationDTO> GetTranslationsByRequestId(int requestId)
        {
            _logger.LogInformation("Fetching translations for request ID: {RequestId}", requestId);
            var translations = _context.Translations
                                       .Where(t => t.TranslationRequestId == requestId)
                                       .Select(t => new TranslationDTO
                                       {
                                           Id = t.Id,
                                           TranslationRequestId = t.TranslationRequestId,
                                           TranslatorUserId = t.TranslatorUserId,
                                           TranslatedText = t.TranslatedText,
                                           IsApproved = t.IsApproved,
                                           Language = t.Language,
                                           IsGuest = t.IsGuest
                                       })
                                       .ToList();

            _logger.LogInformation("Fetched {Count} translations", translations.Count);
            return translations;
        }

        // Retrieves translations done by a specific translator user ID
        public IEnumerable<TranslationDTO> GetTranslationsByTranslatorUserId(int translatorUserId)
        {
            _logger.LogInformation("Fetching translations for translator user ID: {TranslatorUserId}", translatorUserId);
            var translations = _context.Translations
                                       .Where(t => t.TranslatorUserId == translatorUserId)
                                       .Select(t => new TranslationDTO
                                       {
                                           Id = t.Id,
                                           TranslationRequestId = t.TranslationRequestId,
                                           TranslatorUserId = t.TranslatorUserId,
                                           TranslatedText = t.TranslatedText,
                                           IsApproved = t.IsApproved,
                                           Language = t.Language,
                                           IsGuest = t.IsGuest
                                       })
                                       .ToList();

            _logger.LogInformation("Fetched {Count} translations", translations.Count);
            return translations;
        }

        // Retrieves translations associated with a specific media ID
        public IEnumerable<TranslationDTO> GetTranslationsByMediaId(int mediaId)
        {
            var translations = _context.Translations
                .Include(t => t.TranslationRequest)
                    .ThenInclude(tr => tr.Media)
                .Where(t => t.TranslationRequest.MediaId == mediaId)
                .Select(t => new TranslationDTO
                {
                    Id = t.Id,
                    TranslationRequestId = t.TranslationRequestId,
                    TranslatorUserId = t.TranslatorUserId,
                    TranslatedText = t.TranslatedText,
                    IsApproved = t.IsApproved,
                    Language = t.Language,
                    IsGuest = t.IsGuest
                }).ToList();

            return translations;
        }

        // Retrieves translation requests associated with a specific media ID
        public IEnumerable<TranslationRequestDTO> GetTranslationRequestsByMediaId(int mediaId)
        {
            var requests = _context.TranslationRequests
                .Include(tr => tr.Media)
                .Include(tr => tr.RequestReferences)
                .Where(tr => tr.MediaId == mediaId)
                .Select(tr => MappingService.MapToDTO(tr))
                .ToList();
            return requests;
        }

        // Retrieves translation requests made by a specific user ID
        public IEnumerable<TranslationRequestDTO> GetTranslationRequestsByUserId(int userId)
        {
            var requests = _context.TranslationRequests
                .Include(tr => tr.Media)
                .Include(tr => tr.RequestReferences)
                .Where(tr => tr.RequestorUserId == userId)
                .Select(tr => MappingService.MapToDTO(tr))
                .ToList();
            return requests;
        }
    }
}
