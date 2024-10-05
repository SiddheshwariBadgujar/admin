using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CrudOperations.Models
{
    public class Category
    {
        [Key]
        public int CId { get; set; }


        public string CName { get; set; }
    }
}