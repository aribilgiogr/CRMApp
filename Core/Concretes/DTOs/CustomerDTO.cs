namespace Core.Concretes.DTOs
{
    public class CustomerDTO
    {
        public int Id { get; set; }
        public string? CompanyName { get; set; }

        // null!: Bu ifade, C# 8.0 ve sonrasında tanıtılan "nullable reference types" özelliği ile ilgilidir. Bu property'nin null olamayacağını belirtir. Null olmayacağına dair garanti verir.
        public string Email { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string Status { get; set; } = null!;
        public int TotalOpportunities { get; set; }
        public decimal TotalValue { get; set; }

    }
}
