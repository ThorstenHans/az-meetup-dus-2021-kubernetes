using AutoMapper;
using Thinktecture.Samples.Entities;
using Thinktecture.Samples.WebAPI.Models;

namespace Thinktecture.Samples.WebAPI.Configuration
{
    public class AudienceProfile: Profile
    {
        public AudienceProfile()
        {
            // outbound

            CreateMap<Audience, AudienceListModel>();
            CreateMap<Audience, AudienceDetailsModel>();
            
            // inbound

            CreateMap<AudienceCreateModel, Audience>();
            CreateMap<AudienceUpdateModel, Audience>();
        }
    }
}
