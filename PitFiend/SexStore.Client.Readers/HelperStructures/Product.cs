namespace SexStore.Client.Readers.HelperStructures
{
    using System.Collections.Generic;

    public struct Product
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public int ProductCode { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public string Type { get; set; }

        public ICollection<int> CategoryIds { get; set; }

        public ICollection<string> Shops { get; set; }
    }
}
