namespace LinkDev.Talabat.Core.Application.Abstraction.Models.Product
{
	public class ProductSpecParams
	{
        public string? Sort { get; set; }
        public int? BrandId { get; set; }
        public int? CategoryId { get; set; }


        public int PageIndex { get; set; } = 1; // Default Page 

        const int MaxPageSize = 10;
        private int pageSize=5; // Default PageSize
        public int PageSize 
        {
            get { return pageSize; }
            set 
            {
                pageSize = value> MaxPageSize? MaxPageSize:value;
            }
        }


    }
}
