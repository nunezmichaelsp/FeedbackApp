namespace FeedbackApp.Domain.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public abstract class Entity
    {
        protected Entity()
        {
            this.SysCreatedOn = DateTime.UtcNow;
        }

        [Key]
        public virtual Guid Id { get; set; }

        public virtual DateTime SysCreatedOn { get; protected set; }
    }
}
