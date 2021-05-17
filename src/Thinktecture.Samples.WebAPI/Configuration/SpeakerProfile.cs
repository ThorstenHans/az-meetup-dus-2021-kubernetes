using AutoMapper;
using Thinktecture.Samples.Entities;
using Thinktecture.Samples.WebAPI.Models;

namespace Thinktecture.Samples.WebAPI.Configuration
{
    public class SpeakerProfile : Profile
    {
        public SpeakerProfile()
        {
            // outbound
            CreateMap<Speaker, SpeakerListModel>()
                .ForMember(slm => slm.Name, action => action.MapFrom(speaker => $"{speaker.FirstName} {speaker.LastName}"));
            
            CreateMap<Speaker, SpeakerDetailsModel>();

            // inbound
            CreateMap<SpeakerCreateModel, Speaker>();
            CreateMap<SpeakerUpdateModel, Speaker>();
        }
    }
}
