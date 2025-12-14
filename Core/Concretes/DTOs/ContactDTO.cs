namespace Core.Concretes.DTOs
{
    public class ContactListDTO
    {
        public int Id { get; set; }
        public string FullName { get; set; } = null!;
        public string CompanyName { get; set; } = null!;
        public string? Title { get; set; }
        public string Email { get; set; } = null!;
        public string? Phone { get; set; }
        public bool IsPrimary { get; set; }
    }
}