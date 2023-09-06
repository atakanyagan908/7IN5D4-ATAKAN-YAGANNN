using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Product : IEntity
    {
        public int Id { get; set; }
        
        [Required]
        public string ProductName { get; set; }

        [Required]
        public string ProductDescription { get; set; }

        [Required]
        public int UnitPrice { get; set; }

        [Required]
        public int UnitInStock { get; set; }

        public Product(string ProductName, string ProductDescription, int UnitPrice, int UnitInStock) { 
        
            this.ProductName = ProductName;
            this.ProductDescription = ProductDescription;
            this.UnitPrice = UnitPrice;
            this.UnitInStock = UnitInStock;

        }

        public Product() { 


        }



    }
}
