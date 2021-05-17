using System.Threading.Tasks;
using Thinktecture.Samples.Entities;

namespace Thinktecture.Samples.WebAPI.Repositories
{
    public class AuditLogRepository : IAuditLogRepository
    {
        protected DemoContext Context { get; }

        public AuditLogRepository(DemoContext context)
        {
            Context = context;
        }
        public async Task AuditCreatedAsync(string message)
        {
            var audit = CreateAuditLog(message, AuditLevel.Created);
            Context.AuditLogs.Add(audit);
            await Context.SaveChangesAsync();
        }

        public async Task AuditUpdatedAsync(string message)
        {
            var audit = CreateAuditLog(message, AuditLevel.Updated);
            Context.AuditLogs.Add(audit);
            await Context.SaveChangesAsync();
        }

        public async Task AuditDeletedAsync(string message)
        {
            var audit = CreateAuditLog(message, AuditLevel.Deleted);
            Context.AuditLogs.Add(audit);
            await Context.SaveChangesAsync();
        }

        private AuditLog CreateAuditLog(string message, AuditLevel level)
        {
            return new()
            {
                Message = message,
                Level = level
            };
        }
    }
}
