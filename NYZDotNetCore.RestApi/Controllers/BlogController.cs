using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NYZDotNetCore.RestApi.Db;
using NYZDotNetCore.RestApi.Models;

namespace NYZDotNetCore.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        //private readonly AppDbContext _appDbContext;

        //public BlogController(AppDbContext context)
        //{
        //    _appDbContext = context;
        //}

        private readonly AppDbContext _appDbContext;

        public BlogController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        [HttpGet]
        public IActionResult Read()
        {
            var list = _appDbContext.Blogs.ToList();
            return Ok(list);
        }

        [HttpGet("{id}")]
        public IActionResult Edit(int id)
        {
            var item = _appDbContext.Blogs.FirstOrDefault(x => x.BlogId == id);
            if (item is null)
            {
                return NotFound("No data found!");
            }
            return Ok(item);
        }

        [HttpPost]
        public IActionResult Create(BlogModel blog)
        {
            _appDbContext.Blogs.Add(blog);
            var result = _appDbContext.SaveChanges();

            string message = result > 0 ? "Saved Successfully" : "Saving Failed";
            return Ok(message);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, BlogModel blog)
        {
            var item = _appDbContext.Blogs.FirstOrDefault(x => x.BlogId == id);
            if (item is null)
            {
                return NotFound("No data found!");
            }

            item.BlogTitle = blog.BlogTitle;
            item.BlogAuthor = blog.BlogAuthor;
            item.BlogContent = blog.BlogContent;
            var result = _appDbContext.SaveChanges();

            string message = result > 0 ? "Updated Successfully" : "Updating Failed";
            return Ok(message);
        }

        [HttpPatch("{id}")]
        public IActionResult Patch(int id, BlogModel blog)
        {
            var item = _appDbContext.Blogs.FirstOrDefault(x => x.BlogId == id);
            if (item is null)
            {
                return NotFound("No data found!");
            }

            if (!string.IsNullOrEmpty(blog.BlogTitle))
            {
                item.BlogTitle = blog.BlogTitle;
            }
            if (!string.IsNullOrEmpty(blog.BlogAuthor))
            {
                item.BlogAuthor = blog.BlogAuthor;
            }
            if (!string.IsNullOrEmpty(blog.BlogContent))
            {
                item.BlogContent = blog.BlogContent;
            }
            var result = _appDbContext.SaveChanges();


            string message = result > 0 ? "Updated Successfully" : "Updating Failed";
            return Ok(message);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var item = _appDbContext.Blogs.FirstOrDefault(x => x.BlogId == id);
            if (item is null)
            {
                return NotFound("No data found!");
            }

            _appDbContext.Blogs.Remove(item);
            var result = _appDbContext.SaveChanges();

            string message = result > 0 ? "Deleted Successfully" : "Deleting Failed";
            return Ok(message);
        }
    }
}
