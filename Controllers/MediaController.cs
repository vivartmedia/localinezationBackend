using Backend_localinezationBackend.Services;
using localinezationBackend.Models;
using Microsoft.AspNetCore.Mvc;

namespace Backend_localinezationBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MediaController : ControllerBase
    {
        private readonly MediaService _data;

        public MediaController(MediaService data)
        {
            _data = data;
        }

        [HttpPost]
        [Route("AddMediaItem")]
        public ActionResult AddMediaItem(MediaItemModel newMediaItem)
        {
            if (_data.AddMediaItem(newMediaItem))
                return Ok("Media item added successfully.");
            return BadRequest("Failed to add media item.");
        }

        [HttpGet]
        [Route("GetAllMediaItems")]
        public ActionResult<IEnumerable<MediaItemModel>> GetAllMediaItems()
        {
            return Ok(_data.GetAllMediaItems());
        }

        [HttpGet]
        [Route("GetItemsByUserId/{userId}")]
        public ActionResult<IEnumerable<MediaItemModel>> GetItemsByUserId(int userId)
        {
            return Ok(_data.GetItemsByUserId(userId));
        }

        [HttpGet]
        [Route("GetItemsByOriginalLanguage/{originallanguage}")]
        public ActionResult<IEnumerable<MediaItemModel>> GetItemsByOriginalLanguage(string originallanguage)
        {
            return Ok(_data.GetItemsByOriginalLanguage(originallanguage));
        }

        [HttpGet]
        [Route("GetPublishedItems")]
        public ActionResult<IEnumerable<MediaItemModel>> GetPublishedItems()
        {
            return Ok(_data.GetPublishedItems());
        }

        [HttpGet]
        [Route("GetAllItemsByTags/{tag}")]
        public ActionResult<List<MediaItemModel>> GetAllItemsByTags(string tag)
        {
            return Ok(_data.GetAllItemsByTags(tag));
        }

        [HttpGet]
        [Route("GetMediaItemById/{id}")]
        public ActionResult<MediaItemModel> GetMediaItemById(int id)
        {
            var mediaItem = _data.GetMediaItemById(id);
            if (mediaItem != null)
                return Ok(mediaItem);
            return NotFound("Media item not found.");
        }

        [HttpPut]
        [Route("UpdateMediaItem")]
        public ActionResult UpdateMediaItem(MediaItemModel mediaUpdate)
        {
            if (_data.UpdateMediaItem(mediaUpdate))
                return Ok("Media item updated successfully.");
            return BadRequest("Failed to update media item.");
        }

        [HttpDelete]
        [Route("DeleteMediaItem")]
        public ActionResult DeleteMediaItem(int mediaId)
        {
            if (_data.DeleteMediaItem(mediaId))
                return Ok("Media item deleted successfully.");
            return BadRequest("Failed to delete media item.");
        }

        [HttpPost]
        [Route("AddTranslationRequest")]
        public ActionResult AddTranslationRequest(TranslationRequestModel translationRequest)
        {
            if (_data.AddTranslationRequest(translationRequest))
                return Ok("Translation request added successfully.");
            return BadRequest("Failed to add translation request.");
        }

        [HttpPost]
        [Route("AddTranslation")]
        public IActionResult AddTranslation([FromBody] TranslationModel translation)
        {
            if (_data.AddTranslation(translation))
                return Ok("Translation added successfully.");
            return BadRequest("Failed to add translation.");
        }

        [HttpGet]
        [Route("GetUserMediaWithTranslations/{userId}")]
        public ActionResult<IEnumerable<MediaItemModel>> GetUserMediaWithTranslations(int userId)
        {
            return Ok(_data.GetUserMediaWithTranslations(userId));
        }

        [HttpGet]
        [Route("GetTranslatedMediaByUser/{translatorUserId}")]
        public ActionResult<IEnumerable<MediaItemModel>> GetTranslatedMediaByUser(int translatorUserId)
        {
            return Ok(_data.GetTranslatedMediaByUser(translatorUserId));
        }

    }
}
