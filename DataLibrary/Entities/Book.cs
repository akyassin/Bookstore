namespace DataAccess.Entities
{
    public class Book
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public string ImageName { get; set; }
        public Author Author { get; set; }
    }
}
