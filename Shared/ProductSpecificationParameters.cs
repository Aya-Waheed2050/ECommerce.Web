namespace Shared
{
    public class ProductSpecificationParameters
    {
        private const int DefaultPageSize = 5;
        private const int MaxPageSize = 10;
        public int? BrandId {get; set; }
        public int? TypeId {get; set; }
        public ProductSortingOptions Sort { get; set; }
        public string? Search {get; set; }

        // Pagination
        public int PageIndex { get; set; } = 1;

     
        private int _pageSize = DefaultPageSize;
        public int PageSize 
        {
            get => _pageSize;
            set
            {
                _pageSize = value > MaxPageSize? MaxPageSize : value;
            } 
        }

    }
}
