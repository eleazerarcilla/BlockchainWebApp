using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BlockChain.Framework.Entities
{
    [Table("tblProductList")]
    public class ProductList
    {
        [Key]
        public long ID_PRODUCT { get; set; }
        public string TX_JAN { get; set; }
        public string TX_SKU { get; set; }
        public string TX_CATEGORY { get; set; }
        public string TX_SUBCATEGORY { get; set; }
        public string TX_INGREDIENTS { get; set; }
    }
}
