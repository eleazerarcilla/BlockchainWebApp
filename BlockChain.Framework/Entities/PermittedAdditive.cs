using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BlockChain.Framework.Entities
{
    [Table("tblPermittedAdditive")]
    public class PermittedAdditive
    {
        [Key]
        public long ID_ADDITIVE { get; set; }
        public string TX_ADDITIVE_NAME { get; set; }
        public string TX_ADDITIVE_NAME_FR { get; set; }
        public string TX_REMARKS { get; set; }
    }
}
