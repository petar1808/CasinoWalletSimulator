namespace CasinoWallet.Models
{
    public class Result
    {
        public Result(bool isSuccess, string message)
        {
            IsSuccess = isSuccess;
            Message = message;
        }

        public bool IsSuccess { get; }

        public string Message { get; }
    }

    public class Result<TData> : Result
    {
        private readonly TData _data;

        public Result(bool isSuccess, string message, TData data)
         : base(isSuccess, message)
         => this._data = data;

        public TData Data => this._data;
    }
}
