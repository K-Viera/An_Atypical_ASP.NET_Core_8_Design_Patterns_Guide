namespace BookStore
{
    public class Book : IComponent
    {
        public Book(string title) {
            Title = title;
        }
        public string Title { get; }
        public string Type => "Book";
        public int Count { get; } = 1;
    }
}
