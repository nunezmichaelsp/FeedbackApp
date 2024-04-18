namespace FeedbackApp.Web.Controllers
{
    using System;
    using System.Threading.Tasks;
    using FeedbackApp.Application.DTOs;
    using FeedbackApp.Application.Interfaces;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize]
    public class FeedbackController : Controller
    {
        private readonly IFeedbackService feedbackService;
        private readonly ILogger<FeedbackController> logger;

        public FeedbackController(IFeedbackService feedbackService, ILogger<FeedbackController> logger)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.feedbackService = feedbackService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return this.View();
        }

        [HttpGet]
        public async Task<JsonResult> GetFeedbacks()
        {
            var feedbacks = await this.feedbackService.GetAllFeedbackAsync();
            return this.Json(feedbacks);
        }

        [HttpPost]
        public async Task<JsonResult> Insert(FeedbackDto feedbackDto)
        {
            if (feedbackDto != null)
            {
                var createdFeedback = await this.feedbackService.AddFeedbackAsync(feedbackDto);
                this.logger.LogInformation("Feedback with Id: {Id} was created successfully.", createdFeedback.Id);
                return this.Json("Feedback creation succeed.");
            }

            return this.Json("Feedback creation failed.");
        }

        [HttpGet]
        public async Task<JsonResult> Edit([FromQuery] Guid? id)
        {
            if (id != null)
            {
                var feedbackDtoEdit = await this.feedbackService.GetByIdAsync(id.Value);

                if (feedbackDtoEdit != null)
                {
                    return new JsonResult(feedbackDtoEdit);
                }
            }

            return new JsonResult(new { message = "Not Found" }) { StatusCode = StatusCodes.Status404NotFound };
        }

        [HttpPost]
        public async Task<JsonResult> Update(FeedbackDto feedbackDto)
        {
            if (this.ModelState.IsValid)
            {
                await this.feedbackService.UpdateFeedbackAsync(feedbackDto);
                this.logger.LogInformation("Feedback with Id: {Id} was updated successfully.", feedbackDto.Id);
                return this.Json("Feedback update succeed.");
            }

            return this.Json("Feedback update failed.");
        }

        [HttpPost]
        public async Task<JsonResult> Delete([FromQuery] Guid? id)
        {
            if (id != null)
            {
                await this.feedbackService.DeleteFeedbackAsync(id.Value);
                this.logger.LogInformation("Feedback with Id: {Id} was deleted successfully.", id.Value);
                return this.Json("Feedback delete succeed.");
            }

            return this.Json("Feedback delete failed.");
        }

        [HttpGet]
        public async Task<JsonResult> GetCustomerNames()
        {
            var customerNames = await this.feedbackService.GetCustomerNamesAsync();
            return this.Json(customerNames);
        }

        [HttpGet]
        public async Task<JsonResult> GetCategories()
        {
            var categories = await this.feedbackService.GetCategoriesAsync();
            return this.Json(categories);
        }

        [HttpGet]
        public async Task<JsonResult> SearchFeedbacks(string customerName, string category, DateTime? startDate, DateTime? endDate)
        {
            var searchFeedbacks = await this.feedbackService.SearchFeedbacksAsync(customerName, category, startDate, endDate);

            return this.Json(searchFeedbacks);
        }
    }
}
