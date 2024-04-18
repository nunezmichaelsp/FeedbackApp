namespace FeedbackApp.Data.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using FeedbackApp.Application.Interfaces;
    using FeedbackApp.Domain.Entities;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;

    public class FeedbackRepository : IFeedbackRepository
    {
        private readonly ApplicationDbContext context;
        private readonly ILogger<FeedbackRepository> logger;

        public FeedbackRepository(ApplicationDbContext context, ILogger<FeedbackRepository> logger)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task AddAsync(Feedback feedback)
        {
            this.context.Feedbacks.Add(feedback);
            await this.context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var feedback = await this.context.Feedbacks.FindAsync(id);
            if (feedback != null)
            {
                this.context.Feedbacks.Remove(feedback);
                await this.context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Feedback>> GetAllAsync()
        {
            return await this.context.Feedbacks.ToListAsync();
        }

        public async Task<Feedback> GetByIdAsync(Guid id)
        {
            var feedback = await this.context.Feedbacks.FindAsync(id);
            if (feedback == null)
            {
                this.logger.LogError("The feedback Id {FeedbackId} is invalid", id);
                throw new InvalidOperationException($"Feedback not found with Id: {id}.");
            }

            return feedback;
        }

        public async Task UpdateAsync(Feedback feedback)
        {
            this.context.Entry(feedback).State = EntityState.Modified;
            await this.context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Feedback>> GetByCategoryAsync(string category)
        {
            return await this.context.Feedbacks
                .Where(f => f.Category == category)
                .ToListAsync();
        }
    }
}
