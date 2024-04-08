namespace Application.Wrappers
{
    public class Filter
    {
        public string Column { get; set; } = string.Empty;
        public string Conditional { get; set; } = string.Empty;
        public string Value { get; set; } = string.Empty;
        public List<string>? Values { get; set; }
    }
}
