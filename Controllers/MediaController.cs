// Import necessary namespaces and models
using Backend_localinezationBackend.Services;
using localinezationBackend.Models;
using localinezationBackend.Models.DTO;
using Microsoft.AspNetCore.Mvc;

// Define the namespace for this controller class
namespace Backend_localinezationBackend.Controllers
{
    // Marks the class as a controller with API capabilities and sets the base route to the name of the controller
    [ApiController]
    [Route("[controller]")]
    public class MediaController : ControllerBase
    {
        // Dependency injection to use MediaService within the controller
        private readonly MediaService _data;

        // Constructor to inject the MediaService
        public MediaController(MediaService data)
        {
            _data = data;
        }

        // Endpoint to add a new media item; responds to POST requests
        [HttpPost]
        [Route("AddMediaItem")]
        public ActionResult AddMediaItem(MediaItemModel newMediaItem)
        {
            // Attempts to add the media item, returns success or error message
            if (_data.AddMediaItem(newMediaItem))
                return Ok("Media item added successfully.");
            return BadRequest("Failed to add media item.");
        }

        // Endpoint to retrieve all media items; responds to GET requests
        [HttpGet]
        [Route("GetAllMediaItems")]
        public ActionResult<IEnumerable<MediaItemModel>> GetAllMediaItems()
        {
            // Returns all media items
            return Ok(_data.GetAllMediaItems());
        }

        // Endpoint to retrieve media items by user ID; responds to GET requests
        [HttpGet]
        [Route("GetItemsByUserId/{userId}")]
        public ActionResult<IEnumerable<MediaItemModel>> GetItemsByUserId(int userId)
        {
            // Returns media items associated with a specific user ID
            return Ok(_data.GetItemsByUserId(userId));
        }

        // Endpoint to retrieve media items by their original language; responds to GET requests
        [HttpGet]
        [Route("GetItemsByOriginalLanguage/{originallanguage}")]
        public ActionResult<IEnumerable<MediaItemModel>> GetItemsByOriginalLanguage(string originallanguage)
        {
            // Returns media items filtered by their original language
            return Ok(_data.GetItemsByOriginalLanguage(originallanguage));
        }

        // Endpoint to retrieve published media items; responds to GET requests
        [HttpGet]
        [Route("GetPublishedItems")]
        public ActionResult<IEnumerable<MediaItemModel>> GetPublishedItems()
        {
            // Returns all published media items
            return Ok(_data.GetPublishedItems());
        }

        // Endpoint to retrieve a specific media item by ID; responds to GET requests
        [HttpGet]
        [Route("GetMediaItemById/{id}")]
        public ActionResult<MediaItemModel> GetMediaItemById(int id)
        {
            // Attempts to find a media item by its ID
            var mediaItem = _data.GetMediaItemById(id);
            if (mediaItem != null)
                return Ok(mediaItem);
            return NotFound("Media item not found.");
        }

        // Endpoint to update a media item; responds to PUT requests
        [HttpPut]
        [Route("UpdateMediaItem")]
        public ActionResult UpdateMediaItem(MediaItemModel mediaUpdate)
        {
            // Attempts to update the media item, returns success or error message
            if (_data.UpdateMediaItem(mediaUpdate))
                return Ok("Media item updated successfully.");
            return BadRequest("Failed to update media item.");
        }

        // Endpoint to delete a media item; responds to DELETE requests
        [HttpDelete]
        [Route("DeleteMediaItem")]
        public ActionResult DeleteMediaItem(int mediaId)
        {
            // Attempts to delete the media item, returns success or error message
            if (_data.DeleteMediaItem(mediaId))
                return Ok("Media item deleted successfully.");
            return BadRequest("Failed to delete media item.");
        }

        // Endpoint to add a translation request for a media item; responds to POST requests
        [HttpPost]
        [Route("AddTranslationRequest")]
        public ActionResult AddTranslationRequest(TranslationRequestModel translationRequest)
        {
            // Attempts to add a translation request, returns success or error message
            if (_data.AddTranslationRequest(translationRequest))
                return Ok("Translation request added successfully.");
            return BadRequest("Failed to add translation request.");
        }

        // Endpoint to add a translation for a media item; responds to POST requests
        [HttpPost]
        [Route("AddTranslation")]
        public IActionResult AddTranslation([FromBody] TranslationModel translation)
        {
            // Attempts to add a translation, returns success or error message
            if (_data.AddTranslation(translation))
                return Ok("Translation added successfully.");
            return BadRequest("Failed to add translation.");
        }

        // Endpoint to retrieve user media with translations by user ID; responds to GET requests
        [HttpGet]
        [Route("GetUserMediaWithTranslations/{userId}")]
        public ActionResult<IEnumerable<MediaItemModel>> GetUserMediaWithTranslations(int userId)
        {
            // Returns media items with translations for a specific user ID
            return Ok(_data.GetUserMediaWithTranslations(userId));
        }

        // Endpoint to retrieve translated media by translator user ID; responds to GET requests
        [HttpGet]
        [Route("GetTranslatedMediaByUser/{translatorUserId}")]
        public ActionResult<IEnumerable<MediaItemModel>> GetTranslatedMediaByUser(int translatorUserId)
        {
            // Returns media items that have been translated by a specific translator user ID
            return Ok(_data.GetTranslatedMediaByUser(translatorUserId));
        }

        // Endpoint to retrieve translation requests by user ID; handles exceptions and responds to GET requests
        [HttpGet]
        [Route("GetTranslationRequestsByUserId/{userId}")]
        public ActionResult<IEnumerable<TranslationRequestDTO>> GetTranslationRequestsByUserId(int userId)
        {
            try
            {
                // Attempts to retrieve translation requests by user ID
                var translationRequest = _data.GetTranslationRequestsByUserId(userId);
                return Ok(translationRequest);
            }
            catch (Exception ex)
            {
                // Returns a server error response with the exception message
                return StatusCode(500, ex.Message);
            }
        }

        // Endpoint to retrieve translation requests by media ID; handles exceptions and responds to GET requests
        [HttpGet]
        [Route("GetTranslationRequestsByMediaId/{mediaId}")]
        public ActionResult<IEnumerable<TranslationRequestDTO>> GetTranslationRequestsByMediaId(int mediaId)
        {
            try
            {
                // Attempts to retrieve translation requests by media ID
                var translationRequest = _data.GetTranslationRequestsByMediaId(mediaId);
                return Ok(translationRequest);
            }
            catch (Exception ex)
            {
                // Returns a server error response with the exception message
                return StatusCode(500, ex.Message);
            }
        }

        // Endpoint to retrieve translations by request ID; handles not found scenarios and responds to GET requests
        [HttpGet]
        [Route("GetTranslationsByRequestId/{requestId}")]
        public ActionResult<IEnumerable<TranslationDTO>> GetTranslationsByRequestId(int requestId)
        {
            // Retrieves translations by request ID
            var translations = _data.GetTranslationsByRequestId(requestId);
            if (translations.Any())
                return Ok(translations);
            else
                return NotFound("No translations found for this request ID.");
        }

        // Endpoint to retrieve translations by translator user ID; handles not found scenarios and responds to GET requests
        [HttpGet]
        [Route("GetTranslationsByTranslatorUserId/{translatorUserId}")]
        public ActionResult<IEnumerable<TranslationDTO>> GetTranslationsByTranslatorUserId(int translatorUserId)
        {
            // Retrieves translations by translator user ID
            var translations = _data.GetTranslationsByTranslatorUserId(translatorUserId);
            if (translations.Any())
                return Ok(translations);
            else
                return NotFound("No translations found for this translator user ID.");
        }
        
        // Endpoint to retrieve translations by media ID; handles exceptions and not found scenarios; responds to GET requests
        [HttpGet]
        [Route("GetTranslationsByMediaId/{mediaId}")]
        public ActionResult<IEnumerable<TranslationDTO>> GetTranslationsByMediaId(int mediaId)
        {
            try
            {
                // Attempts to retrieve translations by media ID
                var translations = _data.GetTranslationsByMediaId(mediaId);
                if (translations.Any())
                    return Ok(translations);
                else
                    return NotFound("No translations found for this media ID.");
            }
            catch (Exception ex)
            {
                // Returns a server error response with the exception message
                return StatusCode(500, ex.Message);
            }
        }
    }
}
