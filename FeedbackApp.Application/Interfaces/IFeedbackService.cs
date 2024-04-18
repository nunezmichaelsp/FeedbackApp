namespace FeedbackApp.Application.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using FeedbackApp.Application.DTOs;

    public interface IFeedbackService
    {
        Task<IEnumerable<FeedbackDto>> GetAllFeedbackAsync();

        Task<FeedbackDto> AddFeedbackAsync(FeedbackDto feedbackDto);

        Task UpdateFeedbackAsync(FeedbackDto feedbackDto);

        Task DeleteFeedbackAsync(Guid feedbackId);
    }
}
