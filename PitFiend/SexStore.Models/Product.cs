namespace SexStore.Models
{
    using System.Collections.Generic;

    public class Product
    {
        private ICollection<Category> categories;

        public Product()
        {
            this.categories = new HashSet<Category>();
        }

        public int ID { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public double Price { get; set; }

        public ProductType Type { get; set; }

        public int QuantityInStock { get; set; }

        public virtual ICollection<Category> Categories { get; set; }
    }
}
