using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NYZDotNetCore.WinFormsApp.Queries
{
    internal class BlogQuery
    {
        public static string BlogCreate { get; } = @"INSERT INTO [dbo].[Tbl_Blog]
           ([BlogTitle]
           ,[BlogAuthor]
           ,[BlogContent])
     VALUES
           (@BlogTitle
           ,@BlogAuthor       
           ,@BlogContent)";

        public static string BlogLists { get; } = @"SELECT TOP (1000) [BlogId]
              ,[BlogTitle]
              ,[BlogAuthor]
              ,[BlogContent]
          FROM [NYZDotNetCore].[dbo].[Tbl_Blog]";

    }
}
