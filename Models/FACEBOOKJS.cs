using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace API_IA_GOOGLE.Models
{
    [Table("TBL_JSON")]
    public class FACEBOOKJS
    {
        [Key]
        public int Id { get; set; }
        public string Json { get; set; }
    }
}