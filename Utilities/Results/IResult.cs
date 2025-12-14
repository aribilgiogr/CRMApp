using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities.Results
{
    public interface IResult
    {
        bool Success { get; }
        string Message { get; }
    }

    public class Result(bool success, string message) : IResult
    {
        // Mesaj parametresi opsiyonel hale getirildi. Bu yapıcı metot kendini (this) çağırarak mesaj parametresi olmadan da kullanılabilir. Burada this ile primary constructor'a yönlendirme yapılıyor.
        public Result(bool success) : this(success, string.Empty) { }
        public bool Success { get; } = success;
        public string Message { get; } = message;
    }

    public class SuccessResult : Result
    {
        public SuccessResult(string message) : base(true, message) { }
        public SuccessResult() : base(true) { }
    }

    public class ErrorResult : Result
    {
        public ErrorResult(string message) : base(false, message) { }
        public ErrorResult() : base(false) { }
    }
}
