namespace FeedbackApp.Application.DTOs
{
    using System;

    public class FeedbackDto
    {
        public Guid Id { get; set; }
        public string? CustomerName { get; set; }
        public string? Category { get; set; }
        public string? Description { get; set; }
        public string? SubmissionDate { get; set; }
    }
}
