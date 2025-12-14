namespace Core.Concretes.DTOs
{
    public class LeadListDTO
    {
        public int Id { get; set; }
        public string FullName { get; set; } = null!;
        public string? CompanyName { get; set; }
        public string Email { get; set; } = null!;
        public string? Phone { get; set; }
        public string Status { get; set; } = null!;
        public string Source { get; set; } = null!;
        public int Score { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
