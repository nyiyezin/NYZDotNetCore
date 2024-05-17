using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using NYZDotNetCore.RestApi;
using NYZDotNetCore.RestApi.Models;
using System.Data;

namespace NYZDotNetCore.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogAdoDotNetController : ControllerBase

    {
        [HttpGet]
        public IActionResult GetBlogs()
        {
            string query = "SELECT * FROM Tbl_Blog";

            SqlConnection sqlConnection = new SqlConnection(ConnectionStrings.sqlConnectionStringBuilder.ConnectionString);
            sqlConnection.Open();

            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);

            DataTable dataTable = new DataTable();
            sqlDataAdapter.Fill(dataTable);

            sqlConnection.Close();

            List<BlogModel> list = dataTable.AsEnumerable().Select(dataRow => new BlogModel
            {
                BlogId = Convert.ToInt32(dataRow["BlogId"]),
                BlogTitle = Convert.ToString(dataRow["BlogTitle"]),
                BlogAuthor = Convert.ToString(dataRow["BlogAuthor"]),
                BlogContent = Convert.ToString(dataRow["BlogContent"]),
            }).ToList();

            return Ok(list);
        }

        [HttpGet("{id}")] 
        public IActionResult GetBlog(int id)
        {
            string query = "SELECT * FROM Tbl_Blog WHERE blogId = @BlogId";

            SqlConnection sqlConnection = new SqlConnection(ConnectionStrings.sqlConnectionStringBuilder.ConnectionString);
            sqlConnection.Open();

            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            sqlCommand.Parameters.AddWithValue("@BlogId", id);
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);

            DataTable dataTable = new DataTable();
            sqlDataAdapter.Fill(dataTable);

            sqlConnection.Close();

            if(dataTable.Rows.Count == 0)
            {
                return NotFound("No data found!");
            }

            DataRow dataRow = dataTable.Rows[0];

            var item = new BlogModel
            {
                BlogId = Convert.ToInt32(dataRow["BlogId"]),
                BlogTitle = Convert.ToString(dataRow["BlogTitle"]),
                BlogAuthor = Convert.ToString(dataRow["BlogAuthor"]),
                BlogContent = Convert.ToString(dataRow["BlogContent"]),
            };

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

            SqlConnection sqlConnection = new SqlConnection(ConnectionStrings.sqlConnectionStringBuilder.ConnectionString);
            sqlConnection.Open();

            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            sqlCommand.Parameters.AddWithValue("@BlogTitle", blog.BlogTitle);
            sqlCommand.Parameters.AddWithValue("@BlogAuthor", blog.BlogAuthor);
            sqlCommand.Parameters.AddWithValue("@BlogContent", blog.BlogContent);
            int result = sqlCommand.ExecuteNonQuery();

            sqlConnection.Close();

            string message = result > 0 ? "Created Successfully!" : "Creating Failed!";

            return Ok(message);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBlog(int id, BlogModel blog)
        {
            string query = @"UPDATE [dbo].[Tbl_Blog]
   SET [BlogTitle] = @BlogTitle
      ,[BlogAuthor] = @BlogAuthor
      ,[BlogContent] = @BlogContent
 WHERE BlogId = @BlogId";

            SqlConnection sqlConnection = new SqlConnection(ConnectionStrings.sqlConnectionStringBuilder.ConnectionString);
            sqlConnection.Open();

            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            sqlCommand.Parameters.AddWithValue("@BlogId", id);
            sqlCommand.Parameters.AddWithValue("@BlogTitle", blog.BlogTitle);
            sqlCommand.Parameters.AddWithValue("@BlogAuthor", blog.BlogAuthor);
            sqlCommand.Parameters.AddWithValue("@BlogContent", blog.BlogContent);
            int result = sqlCommand.ExecuteNonQuery();

            sqlConnection.Close();

            string message = result > 0 ? "Updated Successfully!" : "Updating Failed!";
            return Ok(message);
        }

        [HttpPatch("{id}")]
        public IActionResult PatchBlog(int id, BlogModel blog)
        {
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

            SqlConnection sqlConnection = new SqlConnection(ConnectionStrings.sqlConnectionStringBuilder.ConnectionString);
            sqlConnection.Open();

            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            sqlCommand.Parameters.AddWithValue("@BlogId", id);
            sqlCommand.Parameters.AddWithValue("@BlogTitle", blog.BlogTitle);
            sqlCommand.Parameters.AddWithValue("@BlogAuthor", blog.BlogAuthor);
            sqlCommand.Parameters.AddWithValue("@BlogContent", blog.BlogContent);
            int result = sqlCommand.ExecuteNonQuery();

            sqlConnection.Close();

            string message = result > 0 ? "Updated Successfully!" : "Updating Failed!";
            return Ok(message);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBlog(int id)
        {
            string query = @"DELETE FROM [dbo].[Tbl_Blog]
      WHERE BlogId = @BlogId;";

            SqlConnection sqlConnection = new SqlConnection(ConnectionStrings.sqlConnectionStringBuilder.ConnectionString);
            sqlConnection.Open();

            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            sqlCommand.Parameters.AddWithValue("@BlogId", id);
            int result = sqlCommand.ExecuteNonQuery();

            sqlConnection.Close();

            string message = result > 0 ? "Deleted Successfully!" : "Deleting Failed!";
            return Ok(message);
        }
    }
}
