using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.Constants;

namespace Utilities.Results
{
    public interface IDataResult<TData> : IResult
    {
        TData Data { get; }
    }

    public class DataResult<TData>(TData data, bool success, string message) : Result(success, message), IDataResult<TData>
    {
        // Mesaj parametresi opsiyonel hale getirildi. Bu yapıcı metot kendini (this) çağırarak mesaj parametresi olmadan da kullanılabilir. Burada this ile primary constructor'a yönlendirme yapılıyor.
        public DataResult(TData data, bool success) : this(data, success, string.Empty) { }

        public TData Data { get; } = data;
    }

    public class SuccessDataResult<TData> : DataResult<TData>
    {
        public SuccessDataResult(TData data, string message) : base(data, true, message) { }
        public SuccessDataResult(TData data) : base(data, true) { }
    }

    public class ErrorDataResult<TData> : DataResult<TData>
    {
        public ErrorDataResult(string message) : base(default, false, message) { }
        public ErrorDataResult(TData data, string message) : base(data, false, message) { }
    }
}
