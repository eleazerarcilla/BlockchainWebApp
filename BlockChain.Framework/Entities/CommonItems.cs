using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BlockChain.Framework.Entities
{
    [Table("tblCommonItems")]
    public class CommonItems
    {
        [Key]
        public long ID_COMMON_ITEM { get; set; }
        public string TX_COMMON_ITEM { get; set; }
    }
}
