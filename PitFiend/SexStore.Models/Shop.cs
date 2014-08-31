namespace SexStore.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Shop
    {
        private ICollection<Product> products;
        private ICollection<Sale> sales;

        public Shop()
        {
            this.products = new HashSet<Product>();
            this.sales = new HashSet<Sale>();
        }

        [Key]
        public int ID { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public virtual ICollection<Product> Products { get; set; }

        public virtual ICollection<Sale> Sales { get; set; }

        public virtual City City { get; set; }
    }
}
