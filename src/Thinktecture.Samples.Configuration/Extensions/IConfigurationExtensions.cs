using System;
using Microsoft.Extensions.Configuration;

namespace Thinktecture.Samples.Configuration.Extensions
{
    // ReSharper disable once InconsistentNaming
    public static class IConfigurationExtensions
    {

        public static APIConfig GetAPIConfig(this IConfiguration configuration)
        {
            var rootSection = configuration.GetSection(APIConfig.RootSectionName);
            if (rootSection == null)
                throw new ApplicationException(
                    $"Root Section ({APIConfig.RootSectionName}) not found in configuration");

            var section = rootSection.GetSection(APIConfig.SectionName);
            if (section == null)
                throw new ApplicationException(
                    $"Section ({APIConfig.SectionName}) not found in root section ({APIConfig.RootSectionName})");

            var config = new APIConfig();
            section.Bind(config);
            return config;

        }
        
    }
}
