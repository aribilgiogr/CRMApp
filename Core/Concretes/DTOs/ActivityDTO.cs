using Core.Concretes.Enums;
using System.ComponentModel.DataAnnotations;

namespace Core.Concretes.DTOs
{
    public class ActivityListDTO
    {
        public int Id { get; set; }
        public string Type { get; set; } = null!;
        public string Subject { get; set; } = null!;
        public string? Description { get; set; }
        public DateTime? DueDate { get; set; }
        public DateTime? CompletedDate { get; set; }
        public bool IsCompleted { get; set; } = false;
        public string? AssignedSalesPersonId { get; set; }
        public string? AssignedSalesPersonName { get; set; }
    }

    public class ActivityCreateDTO
    {
        [Required]
        public ActivityType Type { get; set; }
        [Required]
        public string Subject { get; set; } = null!;
        [DataType(DataType.MultilineText)]
        public string? Description { get; set; }
        public DateTime? DueDate { get; set; }
        [Required]
        public int RelatedId { get; set; }
        [Required]
        public string AssignedSalesPersonId { get; set; } = null!;
    }
}