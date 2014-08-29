namespace SexStore.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class City
    {
        private ICollection<Shop> shops;

        public City()
        {
            this.shops = new HashSet<Shop>();
        }

        [Key]
        public int ID { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Shop> Shops { get; set; }
    }
}
