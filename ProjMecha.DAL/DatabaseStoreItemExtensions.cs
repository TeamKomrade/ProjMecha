using Microsoft.EntityFrameworkCore;
using ProjMecha.DL.Store;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjMecha.DAL
{
    public static class DatabaseStoreItemExtensions
    {
        public static IQueryable<StoreItem> GetOnlyProducts(this IQueryable<StoreItem> items)
            => items.Where(c => c.StoreItemType == StoreItemType.Product);

        public static IQueryable<StoreItem> GetOnlyServices(this IQueryable<StoreItem> items)
            => items.Where(c => c.StoreItemType == StoreItemType.Service);

        public static StoreItem GetByID(this IQueryable<StoreItem> items, int id)
            => items.Where(c => c.StoreItemID == id).Include(c => c.AttributeProductPairs).ThenInclude(ap => ap.ProductAttribute).FirstOrDefault();

        public static ProductAttribute GetAttributeByName(this IQueryable<ProductAttribute> items, string name)
            => items.Where(c => c.Name == name).FirstOrDefault();

        public static IQueryable<StoreItem> GetStoreItemByName(this IQueryable<StoreItem> items, string name)
            => items.Where(c => c.Title.Contains(name));
    }
}
