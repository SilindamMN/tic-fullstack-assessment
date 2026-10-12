using AutoMapper;
using TlcBackend.DTOs;
using TlcBackend.Entities;

namespace TlcBackend.MappingProfile
{
    /// <summary>
    /// Represents the mapping profile for AutoMapper.
    /// </summary>
    public class MappingProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MappingProfile"/> class.
        /// </summary>
        public MappingProfile()
        {
            CreateMap<Participant, ParticipantDto>();
            CreateMap<Reward, RewardDto>();
        }
    }
}