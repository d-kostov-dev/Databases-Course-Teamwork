namespace SexStore.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Shop
    {
        private ICollection<Product> products;

        public Shop()
        {
            this.products = new HashSet<Product>();
        }

        [Key]
        public int ID { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public virtual ICollection<Product> Products { get; set; }

        public City City { get; set; }
    }
}
