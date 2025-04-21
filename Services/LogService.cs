using System.Threading.Tasks;
using TaskManagerApi.Enums;
using TaskManagerApi.Models;
using TaskManagerApi.Data;
using TaskManagerApi.Repositories;


namespace TaskManagerApi.Services
{

    public class LogService : ILogService
    {
        private readonly ILogRepository _repository;

        public LogService(ILogRepository repository)
        {
            _repository = repository;
        }

        public async Task LogAsync(LogEntry action, string entity, int userId)
        {
            await _repository.AddLogAsync(action, entity, userId);
        }
    }
}
