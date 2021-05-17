using AutoMapper;
using Thinktecture.Samples.Entities;
using Thinktecture.Samples.WebAPI.Models;

namespace Thinktecture.Samples.WebAPI.Configuration
{
    public class SessionProfile : Profile
    {
        public SessionProfile()
        {
            // outbound
            CreateMap<Session, SessionListModel>();
            CreateMap<Session, SessionDetailsModel>();
            
            // inbound
            CreateMap<SessionCreateModel, Session>();
            CreateMap<SessionUpdateModel, Session>();

        }
    }
}
