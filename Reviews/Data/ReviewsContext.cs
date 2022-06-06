using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Reviews.Models;

namespace Reviews.Data
{
    public class ReviewsContext : DbContext
    {
        public ReviewsContext (DbContextOptions<ReviewsContext> options)
            : base(options)
        {
        }
        public ReviewsContext()
        {
        }

        public DbSet<Reviews.Models.Review> Review { get; set; }
    }
}
