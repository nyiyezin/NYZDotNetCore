using Dapper;
using NYZDotNetCore.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NYZDotNetCore.RestApi;
using NYZDotNetCore.RestApi.Models;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Reflection.Metadata;

namespace NYZDotNetCore.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogDapper2Controller : ControllerBase
    {
        private readonly DapperService _dapperService = new DapperService(ConnectionStrings.sqlConnectionStringBuilder.ConnectionString);

        [HttpGet]
        public IActionResult GetBlogs()
        {
            string query = "SELECT * FROM Tbl_Blog";
            var list2 = _dapperService.Query<BlogModel>(query);
            return Ok(list2);
        }

        [HttpGet("{id}")]
        public IActionResult GetBlog(int id)
        {
            var item = FindById(id);
            if (item is null)
            {
                return NotFound("No data found!");
            }
            return Ok(item);
        }

        [HttpPost]
        public IActionResult CreateBlog(BlogModel blog)
        {
            string query = @"INSERT INTO [dbo].[Tbl_Blog]
           ([BlogTitle]
           ,[BlogAuthor]
           ,[BlogContent])
     VALUES
           (@BlogTitle
		   ,@BlogAuthor
		   ,@BlogContent)";

            int result = _dapperService.Execute(query, blog);

            string message = result > 0 ? "Saved Successfully!" : "Saving Failed!";
            return Ok(message);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBlog(int id, BlogModel blog)
        {
            var item = FindById(id);
            if(item is null)
            {
                return NotFound();
            }
            blog.BlogId = id;
            string query = @"UPDATE [dbo].[Tbl_Blog]
   SET [BlogTitle] = @BlogTitle
      ,[BlogAuthor] = @BlogAuthor
      ,[BlogContent] = @BlogContent
 WHERE BlogId = @BlogId";

            int result = _dapperService.Execute(query, blog);

            string message = result > 0 ? "Updated Successfully!" : "Updating Failed!";
            return Ok(message);
        }

        [HttpPatch("{id}")]
        public IActionResult PatchBlos(int id, BlogModel blog)
        {
            var item = FindById(id);
            if (item is null)
            {
                return NotFound();
            }
            string conditions = string.Empty;
            if(!string.IsNullOrEmpty(blog.BlogTitle))
            {
                conditions += "[BlogTitle] = @BlogTitle,  ";
            }
            if (!string.IsNullOrEmpty(blog.BlogAuthor))
            {
                conditions += "[BlogAuthor] = @BlogAuthor,  ";
            }
            if (!string.IsNullOrEmpty(blog.BlogContent))
            { 
                conditions += "[BlogContent] = @BlogContent, ";
            }
            if(conditions.Length == 0)
            {
                return NotFound("No data to update!");
            }


            conditions = conditions.Substring(0, conditions.Length - 2);
            blog.BlogId = id;

            string query = $@"UPDATE [dbo].[Tbl_Blog]
   SET {conditions}
 WHERE BlogId = @BlogId";


            int result = _dapperService.Execute(query, blog);

            string message = result > 0 ? "Updated Successfully!" : "Updating Failed!";
            return Ok(message);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBlog(int id)
        {
            var item = FindById(id);
            if(item is null)
            {
                return NotFound();
            }
            string query = @"DELETE FROM [dbo].[Tbl_Blog] WHERE BlogId = @BlogId;";

            int result = _dapperService.Execute(query, new BlogModel { BlogId = id});

            string message = result > 0 ? "Deleted Successfully!" : "Deleting Failed!";
            return Ok(message);
        } 
        
        private BlogModel FindById(int id)
        {
            string query = "SELECT * FROM Tbl_Blog where blogId = @BlogId";
            var item = _dapperService.QueryFirstOrDefault<BlogModel>(query, new BlogModel { BlogId = id });
            return item;
        }
    }
}
