using System;

namespace Thinktecture.Samples.Configuration
{
    public class APIConfig
    {
        public const String RootSectionName = "Thinktecture";
        public const String SectionName = "Api";
        
        public String DatabaseConnectionString { get; set; }
        public int AuditLogRetentionDays { get; set; }
    }
}
