
using Backend_localinezationBackend.Services;
using localinezationBackend.Models;
using Microsoft.AspNetCore.Mvc;


namespace Backend_localinezationBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MediaController : ControllerBase
    {
        // private readonly ILogger<MediaController> _logger;
        private readonly MediaService _data;

        public MediaController(MediaService data)
        {
            _data = data;
        }

        [HttpPost]
        [Route("AddMediaItem")]
            public bool AddMediaItem(MediaItemModel newMediaItem){
            return _data.AddMediaItem(newMediaItem);
        }


        [HttpGet]
        [Route("GetAllMediaItems")]
        public IEnumerable<MediaItemModel> GetAllMediaItems(){
            return _data.GetAllMediaItems();
        }

        [HttpGet]
        [Route("GetItemsByUserId/{userId}")]
        public IEnumerable<MediaItemModel> GetItemsByUserId(int userId){
            return _data.GetItemsByUserId(userId);
        }

        [HttpGet]
        [Route("GetItemsByOriginalLanguage/{originallanguage}")]
        public IEnumerable<MediaItemModel> GetItemsByOriginalLanguage(string originallanguage)
        {
            return _data.GetItemsByOriginalLanguage(originallanguage);
        }

        [HttpGet]
        [Route("GetPublishedItems")]
        public IEnumerable<MediaItemModel> GetPublishedItems(){
            return _data.GetPublishedItems();
        }

        [HttpGet]
        [Route("GetAllItemsByTags/{tag}")]
        public List<MediaItemModel> GetAllItemsByTags(string tag){
            return _data.GetAllItemsByTags(tag);
        }

        [HttpGet]
        [Route("GetMediaItemById/{id}")]
        public MediaItemModel GetMediaItemById(int id){
            return _data.GetMediaItemById(id);
        }

        [HttpPut]
        [Route("UpdateMediaItem")]
        public bool UpdateMediaItem(MediaItemModel mediaUpdate){
            return _data.UpdateMediaItem(mediaUpdate);
        }

        [HttpDelete]
        [Route("DeleteMediaItem")]
        public bool DeleteMediaItem(MediaItemModel mediaToDelete){
            return _data.DeleteMediaItem(mediaToDelete);
        }




        // POST endpoint to add a translation request to a media item
        [HttpPost]
        [Route("AddTranslationRequest")]
        public ActionResult AddTranslationRequest(TranslationRequestModel translationRequest)
        {
            if (_data.AddTranslationRequest(translationRequest))
                return Ok("Translation request added successfully.");
            return BadRequest("Failed to add translation request.");
        }

        // POST endpoint to add a translation
        [HttpPost]
        [Route("AddTranslation")]
        public ActionResult AddTranslation(TranslationModel translation)
        {
            if (_data.AddTranslation(translation))
                return Ok("Translation added successfully.");
            return BadRequest("Failed to add translation.");
        }

        // GET endpoint to fetch all media and translations for a given user
        [HttpGet]
        [Route("GetUserMediaWithTranslations/{userId}")]
        public ActionResult<IEnumerable<MediaItemModel>> GetUserMediaWithTranslations(int userId)
        {
            return Ok(_data.GetUserMediaWithTranslations(userId));
        }

        // GET endpoint to fetch all translated media by a specific user
        [HttpGet]
        [Route("GetTranslatedMediaByUser/{translatorUserId}")]
        public ActionResult<IEnumerable<MediaItemModel>> GetTranslatedMediaByUser(string translatorUserId)
        {
            return Ok(_data.GetTranslatedMediaByUser(translatorUserId));
        }


    }
}
