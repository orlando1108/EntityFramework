using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeFirstNewDatabaseSample
{
    class Program
    {
        public class Blog
        {
            public int BlogId { get; set; }
            public String Name { get; set; }
            public string Url { get; set; }

            public virtual List<Post> Posts { get; set; }
        }

        public class Post
        {
            public int PostId { get; set; }
            public String Title { get; set; }
            public String Content { get; set; }

            public int BlogId { get; set; }
            public virtual Blog blog { get; set; }
            public virtual List<Tag> Tags { get; set; }
        }

        public class Tag
        {
            public int TagId { get; set; }
            public String Name { get; set; }

            public virtual List<Post> Posts { get; set; }
        }

        public class BloggingContext : DbContext
        {
            public DbSet<Blog> Blogs { get; set; }
            public DbSet<Post> Posts { get; set; }
            public DbSet<Tag> Tags { get; set; }

        }


        static void Main(string[] args)
        {
            using (var db = new BloggingContext())
            {
                //Create and save a new Blog
                Console.Write("Enter a name for a new blog: ");
                var blogName = Console.ReadLine();

                var blog = new Blog { Name = blogName };
                db.Blogs.Add(blog);
               

                //Create and save a new Post
                Console.Write("Enter a title for a new post: ");
                var postTitle = Console.ReadLine();
                var post = new Post { Title = postTitle };
                db.Posts.Add(post);
               

                //Create and save a new Tag
                Console.Write("Enter a name for a new tag: ");
                var tagName = Console.ReadLine();
                var tag = new Tag { Name = tagName };
                db.Tags.Add(tag);
                db.SaveChanges();

                //Display all blogs from the database
                var query = from b in db.Blogs
                            orderby b.Name
                            select b;

                Console.WriteLine("All blogs in the dataBase:");
                foreach (var item in query)
                {
                    Console.WriteLine(item.Name);
                }
                Console.WriteLine("Press any key to exit...");
                Console.ReadKey();


            }
        }
    }
}
