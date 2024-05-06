namespace Application.Wrappers
{
    public class Filter
    {
        private static readonly string[] TextFilters = { "notequals", "beginswith", "notbeginswith", "endswith", "notendswith", "contains", "notcontains" };
        private static readonly string[] NumericFilters = { "greaterthan", "lessthan", "greaterthanorequalto", "lessthanorequalto" };
        private static readonly string[] DateFilters = { "before", "after", "beforeorequal", "afterorequal" };
        private string _column = string.Empty;
        public string Column { set { _column = value.ToLower(); } get { return _column; } }
        public string Conditional { get; set; } = string.Empty;
        public string Value { get; set; } = string.Empty;
        public List<string>? Values { get; set; }
        public bool IsParameter { get; set; }

        public static bool IsValidFilter(string conditionalName, string value)
        {
            bool validFilter = false;

            if (string.IsNullOrEmpty(conditionalName))
            {
                return false;
            }

            if (TextFilters.Contains(conditionalName))
            {
                validFilter = true;
            }
            else if (NumericFilters.Contains(conditionalName))
            {
                return int.TryParse(value, out _);
            }
            else if (DateFilters.Contains(conditionalName))
            {
                return DateTime.TryParse(value, out _);
            }

            return validFilter;
        }
    }
}
