namespace UserRetention.DataBase.DTO
{
    public class ResponseUser<T>
    {
        string Message { get; set; } = string.Empty;
        bool Success { get; set; } = false;

        public T? Data { get; set; } = default!;
    }
}
