namespace ElasticSearchSample
{
    public interface ILoggerService
    {
        Task LogInformation(LoggerRequest request);
        Task LogWarning(LoggerRequest request);
        Task LogError(LoggerRequest request);
    }
}
