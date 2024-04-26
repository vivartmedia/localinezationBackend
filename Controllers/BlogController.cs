using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using localinezationBackend.Models;
using localinezationBackend.Services;
using Microsoft.AspNetCore.Mvc;

namespace localinezationBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BlogController : ControllerBase
    {

        private readonly BlogService _data;

        public BlogController(BlogService data){
            _data = data;
        }

        [HttpPost]
        [Route("AddBlogItem")]
        public bool AddBlogItem(BlogItemModel newBlogItem){
            return _data.AddBlogItem(newBlogItem);
        }


        [HttpGet]
        [Route("GetAllBlogItems")]
        public IEnumerable<BlogItemModel> GetAllBlogItems(){
            return _data.GetAllBlogItems();
        }

        [HttpGet]
        [Route("GetItemsByUserId/{userId}")]
        public IEnumerable<BlogItemModel> GetItemsByUserId(int userId){
            return _data.GetItemsByUserId(userId);
        }

        [HttpGet]
        [Route("GetItemsByCategory/{category}")]
        public IEnumerable<BlogItemModel> GetItemsByCategory(string category)
        {
            return _data.GetItemsByCategory(category);
        }

        [HttpGet]
        [Route("GetPublishedItems")]
        public IEnumerable<BlogItemModel> GetPublishedItems(){
            return _data.GetPublishedItems();
        }

        [HttpGet]
        [Route("Get AllItemsByTags/{tag}")]
        public List<BlogItemModel> GetAllItemsByTags(string tag){
            return _data.GetAllItemsByTags(tag);
        }

        [HttpGet]
        [Route("GetBlogItemById/{id}")]
        public BlogItemModel GetBlogItemById(int id){
            return _data.GetBlogItemById(id);
        }

        [HttpPut]
        [Route("UpdateBlogItem")]
        public bool UpdateBlogItem(BlogItemModel blogUpdate){
            return _data.UpdateBlogItem(blogUpdate);
        }

        [HttpDelete]
        [Route("DeleteBlogItem")]
        public bool DeleteBlogItem(BlogItemModel blogToDelete){
            return _data.DeleteBlogItem(blogToDelete);
        }

    }
}