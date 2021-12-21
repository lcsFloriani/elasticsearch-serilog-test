namespace ElasticSearchSample
{
    public class LoggerRequest
    {
        public string? ApplicationName { get; set; }
        public string? Company { get; set; }
        public string? Message { get; set; }
        public string? InnerMessage { get; set; }
        public string? StackTrace { get; set; }
    }
}
