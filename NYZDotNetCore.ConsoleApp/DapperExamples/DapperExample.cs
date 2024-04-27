using Dapper;
using NYZDotNetCore.ConsoleApp.Dtos;
using NYZDotNetCore.ConsoleApp.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NYZDotNetCore.ConsoleApp.DapperExamples
{
    internal class DapperExample
    {
        public void Run()
        {
            //Read();
            //Edit(1);
            //Edit(11);

            //Create("title", "author", "content");
            //Update(2002, "title 2", "author 2", "content 2");
            Delete(2002);
        }
        private void Read()
        {
            using IDbConnection dbConnection = new SqlConnection(ConnectionStrings.sqlConnectionStringBuilder.ConnectionString);
            List<BlogDto> list = dbConnection.Query<BlogDto>("SELECT * FROM Tbl_Blog").ToList();

            foreach (BlogDto item in list)
            {
                Console.WriteLine(item.BlogId);
                Console.WriteLine(item.BlogTitle);
                Console.WriteLine(item.BlogAuthor);
                Console.WriteLine(item.BlogContent);
                Console.WriteLine("-------------------------");
            }
        }
        private void Edit(int id)
        {
            using IDbConnection dbConnection = new SqlConnection(ConnectionStrings.sqlConnectionStringBuilder.ConnectionString);
            var item = dbConnection.Query<BlogDto>("SELECT * FROM Tbl_Blog where blogId = @BlogId", new BlogDto { BlogId = id }).FirstOrDefault();
            if (item is null)
            {
                Console.WriteLine("No data found!");
                return;
            }

            Console.WriteLine(item.BlogId);
            Console.WriteLine(item.BlogTitle);
            Console.WriteLine(item.BlogAuthor);
            Console.WriteLine(item.BlogContent);
            Console.WriteLine("-------------------------");
        }

        private void Create(string title, string author, string content)
        {
            var item = new BlogDto
            {
                BlogTitle = title,
                BlogAuthor = author,
                BlogContent = content,
            };

            string query = @"INSERT INTO [dbo].[Tbl_Blog]
           ([BlogTitle]
           ,[BlogAuthor]
           ,[BlogContent])
     VALUES
           (@BlogTitle
		   ,@BlogAuthor
		   ,@BlogContent)";

            using IDbConnection dbConnection = new SqlConnection(ConnectionStrings.sqlConnectionStringBuilder.ConnectionString);
            int result = dbConnection.Execute(query, item);

            string message = result > 0 ? "Saved Successfully!" : "Saving Failed!";
            Console.WriteLine(message);
        }

        private void Update(int id, string title, string author, string content)
        {
            var item = new BlogDto
            {
                BlogId = id,
                BlogTitle = title,
                BlogAuthor = author,
                BlogContent = content,
            };

            string query = @"UPDATE [dbo].[Tbl_Blog]
   SET [BlogTitle] = @BlogTitle
      ,[BlogAuthor] = @BlogAuthor
      ,[BlogContent] = @BlogContent
 WHERE BlogId = @BlogId";

            using IDbConnection dbConnection = new SqlConnection(ConnectionStrings.sqlConnectionStringBuilder.ConnectionString);
            int result = dbConnection.Execute(query, item);

            string message = result > 0 ? "Updated Successfully!" : "Updating Failed!";
            Console.WriteLine(message);
        }

        private void Delete(int id)
        {
            var item = new BlogDto
            {
                BlogId = id,
            };

            string query = @"DELETE FROM [dbo].[Tbl_Blog] WHERE BlogId = @BlogId;";

            using IDbConnection dbConnection = new SqlConnection(ConnectionStrings.sqlConnectionStringBuilder.ConnectionString);
            int result = dbConnection.Execute(query, item);

            string message = result > 0 ? "Deleted Successfully!" : "Deleting Failed!";
            Console.WriteLine(message);
        }
    }
}
