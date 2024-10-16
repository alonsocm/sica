using Application.Wrappers;

namespace Application.DTOs.Common
{
    public class QueryHelperDTO
    {
        public List<Filter> Filters { get; set; } = new List<Filter>();
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string Selector { get; set; } = string.Empty;
    }
}
