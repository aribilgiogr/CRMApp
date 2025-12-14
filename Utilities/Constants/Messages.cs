namespace Utilities.Constants
{
    public static class Messages
    {
        // const: Derleme zamanında sabit olan ve değiştirilemeyen değerler için kullanılır. Her kullanımda bellekte yeni bir kopyası oluşturulur.
        // static: Sınıfa ait olup, sınıfın bir örneği oluşturulmadan erişilebilir. Çalışma zamanında bellekte tek bir kopyası bulunur.
        // readonly: Değeri yalnızca nesne oluşturulurken veya yapıcı metot içinde atanabilir ve sonrasında değiştirilemez.
        // static readonly: Sınıfa ait olup, değeri yalnızca sınıfın ilk kullanımı sırasında atanabilir ve sonrasında değiştirilemez.

        public static readonly string ErrorOccurred = "An error occurred.";
        public static readonly string SuccessfulOperation = "Operation completed successfully.";
        public static readonly string ValidationFailed = "Validation failed.";
        public static readonly string UnauthorizedAccess = "Unauthorized access.";
        public static readonly string EmailAlreadyExists = "Email already exists.";

        public static readonly string AddedSuffix = " added successfully.";
        public static readonly string UpdatedSuffix = " updated successfully.";
        public static readonly string DeletedSuffix = " deleted successfully.";
        public static readonly string RetrievedSuffix = " retrieved successfully.";
        public static readonly string ListedSuffix = " listed successfully.";
        public static readonly string NotFoundSuffix = " not found.";
    }
}
