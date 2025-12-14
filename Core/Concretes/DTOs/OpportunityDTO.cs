namespace Core.Concretes.DTOs
{
    public class OpportunityListDTO
    {
        public int Id { get; set; }
        public string CustomerName { get; set; } = null!;
        public string Title { get; set; } = null!;
        public decimal Value { get; set; }
        public string Currency { get; set; } = null!;
        public int Probability { get; set; }
        public string Stage { get; set; } = null!;
        public string Status { get; set; } = null!;
        public DateTime? ExpectedCloseDate { get; set; }
    }
}