namespace HotelListing.API.Models
{
    public class QueryParameters
    {
        private int pageSize = 15;
        public int StartIndex { get; set; }
        public int PageNumber { get; set; }

        public int PageSize
        {
            get
            {
                return pageSize;
            }
            set
            {
                pageSize = value;
            }
        }
    }
}
