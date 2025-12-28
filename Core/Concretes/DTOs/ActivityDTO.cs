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
}