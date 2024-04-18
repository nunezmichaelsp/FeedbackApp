namespace FeedbackApp.Application.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using FeedbackApp.Domain.Entities;

    public interface IFeedbackRepository
    {
        Task<Feedback> GetByIdAsync(Guid id);
        Task<IEnumerable<Feedback>> GetAllAsync();
        Task<IEnumerable<Feedback>> GetByCategoryAsync(string category);
        Task AddAsync(Feedback feedback);
        Task UpdateAsync(Feedback feedback);
        Task DeleteAsync(Guid id);
    }
}
