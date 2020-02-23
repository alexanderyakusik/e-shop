using System.Collections.Generic;

namespace EShop.Models.App
{
    public class StorageGoodsModel
    {
        public IEnumerable<Good> StorageGoods { get; set; }

        public IEnumerable<Good> CatalogGoods { get; set; }
    }
}
