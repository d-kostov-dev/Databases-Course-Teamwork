namespace SexStore.Models
{
    using System;
    using System.Collections.Generic;

    public class Sale
    {
        public int ID { get; set; }

        public Shop Shop { get; set; }

        public Product Product { get; set; }

        public int Quantity { get; set; }

        public DateTime SaleDate { get; set; }
    }
}
