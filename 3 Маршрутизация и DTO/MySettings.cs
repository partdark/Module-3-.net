namespace _3_Маршрутизация_и_DTO
{
    public class MySettings
    {

        public string ApplicationName { get; set; }
        public int MaxBooksPerPage { get; set; }
        public ApiSettings ApiSettings { get; set; } = new ApiSettings();
    }

    public class ApiSettings
    {
        public int TomeoutInSeconds { get; set; }
        public int RetryCount { get; set; }
    }
}
