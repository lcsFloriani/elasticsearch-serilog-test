using Serilog.Context;

namespace ElasticSearchSample
{
    public class LoggerService : ILoggerService
    {
        private readonly ILogger<LoggerService> _logger;

        public LoggerService(ILogger<LoggerService> logger) => _logger = logger;

        public async Task LogError(LoggerRequest request)
        {
            DateTime date = DateTime.UtcNow;
            using (LogContext.PushProperty("ApplicationName", request.ApplicationName))
            using (LogContext.PushProperty("Company", request.Company))
            using (LogContext.PushProperty("LogMessage", request.Message))
            using (LogContext.PushProperty("Date", date))
            {
                await Task.Run(() => _logger.LogError($"{request.ApplicationName} - Date: {date} - Error: {request.Message} Stacktrace: {request.StackTrace}"));
            }
        }

        public async Task LogInformation(LoggerRequest request)
        {
            DateTime date = DateTime.UtcNow;
            using (LogContext.PushProperty("ApplicationName", request.ApplicationName))
            using (LogContext.PushProperty("Company", request.Company))
            using (LogContext.PushProperty("LogMessage", request.Message))
            using (LogContext.PushProperty("Date", date))
            {
                await Task.Run(() => _logger.LogInformation($"{request.ApplicationName} - Date: {date} - Information: {request.Message}"));
            }
        }

        public async Task LogWarning(LoggerRequest request)
        {
            DateTime date = DateTime.UtcNow;
            using (LogContext.PushProperty("ApplicationName", request.ApplicationName))
            using (LogContext.PushProperty("Company", request.Company))
            using (LogContext.PushProperty("LogMessage", request.Message))
            using (LogContext.PushProperty("Date", date))
            {
                await Task.Run(() => _logger.LogWarning($"{request.ApplicationName} - Date: {date} - Warning: {request.Message}"));
            }
        }
    }
}
