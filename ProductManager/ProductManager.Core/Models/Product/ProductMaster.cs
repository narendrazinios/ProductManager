namespace ProductManager.Core.Models.Product
{
    public  class ProductMaster : GuidObject
    {
        public string Name { get; set; }
        public string PID { get; set; }
        public string Description { get; set; }
        public long Count { get; set; }
        public string DetailsJson { get; set; }
        public string[] Images { get; set; }
    }
}
