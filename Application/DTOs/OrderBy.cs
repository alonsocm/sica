namespace Application.DTOs
{
    public class OrderBy
    {
        private string _column = string.Empty;
        public string Column { set { _column = value.ToLower(); } get { return _column; } }
        public string? Type { get; set; }
    }
}
