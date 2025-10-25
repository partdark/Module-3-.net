namespace _3_Маршрутизация_и_DTO
{
    public class Book
    {

        public Book(int year, string author, string title)
        {
            Year = year;
            Author = author;
            Title = title;
        }
        public Guid Id { get; init; } = Guid.NewGuid();
        public int Year { get; set; }
        public string Author { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;

        
      
    }
}
