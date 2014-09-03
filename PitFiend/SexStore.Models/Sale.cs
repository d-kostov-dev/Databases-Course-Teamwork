namespace SexStore.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Sale
    {
        [Key]
        public int ID { get; set; }

        public virtual Shop Shop { get; set; }

        public virtual Product Product { get; set; }

        public int Quantity { get; set; }

        public virtual DateTime SaleDate { get; set; }

        public override string ToString()
        {
            return string.Format("Product ID: {0}, Shop ID: {1}, Quantity: {2}, Sale Date: {3}", this.Product.Name, this.Shop.Name, this.Quantity, this.SaleDate);
        }
    }
}
