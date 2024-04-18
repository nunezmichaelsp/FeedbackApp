namespace FeedbackApp.Application.DTOs
{
    using AutoMapper;
    using FeedbackApp.Domain.Entities;

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            this.CreateMap<Feedback, FeedbackDto>();
            this.CreateMap<FeedbackDto, Feedback>();
        }
    }
}
