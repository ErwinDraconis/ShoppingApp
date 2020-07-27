
namespace ShoppingAPI.Core.Interfaces
{
    public class ProductFilterParams
    {
        private const int MaxPageSize = 50;
        public int pageIndex { get; set; } = 1;

        private int _pagesize=6;
        public int PageSize 
        {
            get => _pagesize;
            set => _pagesize = ((value > MaxPageSize)) 
                ? _pagesize = 50 
                : _pagesize = value; 
        }

        public int? brandId { get; set; }
        public int? typeId { get; set; }
        public string sort { get; set; }

        private string _search;
        public string Search
        {
            get => _search;
            set => _search = value.ToLower();
        }

    }
}
