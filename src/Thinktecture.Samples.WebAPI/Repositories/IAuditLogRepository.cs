using System;
using System.Threading.Tasks;

namespace Thinktecture.Samples.WebAPI.Repositories
{
    public interface IAuditLogRepository
    {
        public Task AuditCreatedAsync(String message);
        public Task AuditUpdatedAsync(String message);
        public Task AuditDeletedAsync(String message);
    }
}
