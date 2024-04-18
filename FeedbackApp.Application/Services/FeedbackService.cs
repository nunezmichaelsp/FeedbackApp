namespace FeedbackApp.Application.Services
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using AutoMapper;
    using FeedbackApp.Application.DTOs;
    using FeedbackApp.Application.Interfaces;
    using FeedbackApp.Domain.Entities;

    public class FeedbackService : IFeedbackService
    {
        private readonly IFeedbackRepository feedbackRepository;
        private readonly IMapper mapper;

        public FeedbackService(IFeedbackRepository feedbackRepository, IMapper mapper)
        {
            this.feedbackRepository = feedbackRepository;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<FeedbackDto>> GetAllFeedbackAsync()
        {
            var feedbacks = (await this.feedbackRepository.GetAllAsync())
                .OrderByDescending(f => f.SubmissionDate);
            return this.mapper.Map<IEnumerable<FeedbackDto>>(feedbacks);
        }

        public async Task<FeedbackDto> GetByIdAsync(Guid id)
        {
            var feedback = await this.feedbackRepository.GetByIdAsync(id);
            if (feedback == null)
            {
                throw new KeyNotFoundException($"Feedback with ID {id} not found.");
            }

            return this.mapper.Map<FeedbackDto>(feedback);
        }

        public async Task<FeedbackDto> AddFeedbackAsync(FeedbackDto feedbackDto)
        {
            var feedback = this.mapper.Map<Feedback>(feedbackDto);
            feedback.Id = Guid.NewGuid();
            feedback.SubmissionDate = DateTime.UtcNow;
            await this.feedbackRepository.AddAsync(feedback);
            return this.mapper.Map<FeedbackDto>(feedback);
        }

        public async Task UpdateFeedbackAsync(FeedbackDto feedbackDto)
        {
            var feedback = await this.feedbackRepository.GetByIdAsync(feedbackDto.Id);
            if (feedback == null)
            {
                throw new InvalidOperationException($"Feedback not found with Id: {feedbackDto.Id}.");
            }

            this.mapper.Map(feedbackDto, feedback);
            await this.feedbackRepository.UpdateAsync(feedback);
        }

        public async Task DeleteFeedbackAsync(Guid feedbackId)
        {
            var feedback = await this.feedbackRepository.GetByIdAsync(feedbackId);
            if (feedback == null)
            {
                throw new InvalidOperationException($"Feedback not found with Id: {feedbackId}.");
            }

            await this.feedbackRepository.DeleteAsync(feedbackId);
        }

        public async Task<List<string?>> GetCustomerNamesAsync()
        {
            var feedbacks = await this.feedbackRepository.GetAllAsync();
            var customerNames = feedbacks.Select(f => f.CustomerName).Distinct().Order().ToList();
            return customerNames;
        }

        public async Task<List<string?>> GetCategoriesAsync()
        {
            var feedbacks = await this.feedbackRepository.GetAllAsync();
            var categories = feedbacks.Select(f => f.Category).Distinct().Order().ToList();
            return categories;
        }

        public async Task<IEnumerable<FeedbackDto>> SearchFeedbacksAsync(string customerName, string category, DateTime? startDate, DateTime? endDate)
        {
            var feedbacks = (await this.feedbackRepository.GetAllAsync())
                .OrderByDescending(f => f.SubmissionDate);
            var feedbackQuery = feedbacks.AsQueryable();

            if (!string.IsNullOrWhiteSpace(customerName))
            {
                feedbackQuery = feedbackQuery.Where(f => f.CustomerName == customerName);
            }

            if (!string.IsNullOrWhiteSpace(category))
            {
                feedbackQuery = feedbackQuery.Where(f => f.Category == category);
            }

            if (startDate.HasValue && endDate.HasValue)
            {
                feedbackQuery = feedbackQuery.Where(f => f.SubmissionDate >= startDate.Value && f.SubmissionDate <= endDate.Value);
            }

            var feedbacksResult = feedbackQuery.OrderByDescending(f => f.SubmissionDate).ToList();

            return this.mapper.Map<IEnumerable<FeedbackDto>>(feedbacksResult);
        }
    }
}
