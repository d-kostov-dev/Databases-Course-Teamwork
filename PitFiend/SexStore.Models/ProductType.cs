namespace SexStore.Models
{
    using System;
    using System.Collections.Generic;

    public class ProductType
    {
        private ICollection<Product> products;

        public ProductType()
        {
            this.products = new HashSet<Product>();
        }

        public int ID { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
