using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using Restaurant.Models;

namespace Restaurant.DAL
{
    public class RestaurantInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<RestaurantContext>
    {
        protected override void Seed(RestaurantContext context)
        {
            base.Seed(context);
            var Tables = new List<Table>
            {
            new Table { ChairCount = 2, Available = true},
            new Table { ChairCount = 2, Available = true},
            new Table { ChairCount = 4, Available = true},
            new Table { ChairCount = 4, Available = true},
            new Table { ChairCount = 4, Available = true},
            new Table { ChairCount = 4, Available = true},
            new Table { ChairCount = 6, Available = true},
            new Table { ChairCount = 6, Available = true}
            };
            Tables.ForEach(t => context.Tables.Add(t));
            context.SaveChanges();
        }
    }
}
