namespace Application.Wrappers
{
    public class PagedResponse<T> : Response<T>
    {
        public PagedResponse(T data, int pageNumber, int pageSize)
        {
            Page=pageNumber;
            PageSize=pageSize;
            Data = data;
            Message = null;
            Succeded = true;
            Errors = null;
        }

        public int Page { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public int TotalRecords { get; set; }
        public bool HasNextPage => Page * PageSize < TotalRecords;
        public bool HasPreviousPage => Page > 1;

        public static PagedResponse<IEnumerable<T>> CreatePagedReponse(IEnumerable<T> data, int page, int pageSize)
        {
            var items = data;

            if (page >= 1)
            {
                var totalRecords = data.Count();
                var totalPages = (double)totalRecords / pageSize;
                int roundedTotalPages = Convert.ToInt32(Math.Ceiling(totalPages));

                if (totalRecords > pageSize)
                {
                    items = data.Skip((page - 1) * pageSize).Take(pageSize).ToList();
                }

                return new PagedResponse<IEnumerable<T>>(items, page, pageSize)
                {
                    TotalPages = roundedTotalPages,
                    TotalRecords = totalRecords
                };
            }
            else
            {
                return new PagedResponse<IEnumerable<T>>(items, page, pageSize);
            }
        }
    }
}
