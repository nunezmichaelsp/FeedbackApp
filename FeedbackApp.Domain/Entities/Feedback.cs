namespace FeedbackApp.Domain.Entities
{
    using System;

    public class Feedback : Entity
    {
        public Feedback()
            : base()
        {
        }

        public string? CustomerName { get; set; }
        public string? Category { get; set; }
        public string? Description { get; set; }
        public DateTime SubmissionDate { get; set; }
    }
}
