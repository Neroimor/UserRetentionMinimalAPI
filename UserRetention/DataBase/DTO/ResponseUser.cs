namespace UserRetention.DataBase.DTO
{
    public class ResponseUser<T>
    {
        public string Message { get; set; } = string.Empty;
        public bool Success { get; set; } = false;
        public int StatusCode { get; set; } = 200;

        public T? Data { get; set; } = default!;
    }
}
