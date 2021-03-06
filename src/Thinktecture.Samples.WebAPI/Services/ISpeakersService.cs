using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Thinktecture.Samples.WebAPI.Models;

namespace Thinktecture.Samples.WebAPI.Services
{
    public interface ISpeakersService
    {
        public Task<IEnumerable<SpeakerListModel>> GetAllAsync();
        public Task<SpeakerDetailsModel> GetByIdAsync(Guid id);
        public Task<SpeakerDetailsModel> CreateAsync(SpeakerCreateModel model);
        public Task<SpeakerDetailsModel> UpdateAsync(Guid id, SpeakerUpdateModel model);
        public Task<bool> DeleteByIdAsync(Guid id);
    }
}
