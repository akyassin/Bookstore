using Bookstore.ViewModels;
using System.Collections.Generic;

namespace Bookstore.Models.Mapper
{
    public class Mapper
    {
        public static List<AuthorViewModel> MapTo(List<Author> authors)
        {
            List<AuthorViewModel> authorsList = new();
            foreach (var author in authors)
            {
                authorsList.Add(new AuthorViewModel()
                {
                    Id = author.Id,
                    FullName = author.FullName
                });
            }
            return authorsList;
        }
        public static List<Author> MapFrom(List<AuthorViewModel> models)
        {
            List<Author> authorsList = new();
            foreach (var model in models)
            {
                authorsList.Add(new Author()
                {
                    Id = (int)model.Id,
                    FullName = model.FullName
                });
            }
            return authorsList;
        }

        public static AuthorViewModel MapTo(Author author)
        {
            return new AuthorViewModel()
            {
                Id = author.Id,
                FullName = author.FullName
            };
        }

        public static Author MapFrom(AuthorViewModel model)
        {
            return new Author()
            {
                Id = (int)model.Id,
                FullName = model.FullName
            };
        }

        public static List<BookAuthorViewModel> MapTo(List<Book> books)
        {
            List<BookAuthorViewModel> booksList = new();
            foreach (var book in books)
            {
                booksList.Add(new BookAuthorViewModel()
                {
                    Id = book.Id,
                    Title = book.Title,
                    Description = book.Description,
                    AuthorId = book.Author.Id,
                    ImageName = book.ImageName,
                });
            }
            return booksList;
        }

        public static List<Book> MapFrom(List<BookAuthorViewModel> models)
        {
            List<Book> booksList = new();
            foreach (var model in models)
            {
                booksList.Add(new Book()
                {
                    Id = (int)model.Id,
                    Title = model.Title,
                    ImageName = model.ImageName,
                    Description = model.Description,
                });
            }
            return booksList;
        }

        public static BookAuthorViewModel MapTo(Book book)
        {
            return new BookAuthorViewModel()
            {
                Id = book.Id,
                Title = book.Title,
                Description = book.Description,
                AuthorId = book.Author.Id,
                ImageName = book.ImageName,
            };
        }

        public static Book MapFrom(BookAuthorViewModel model)
        {
            return new Book()
            {
                Id = (int)model.Id,
                Title = model.Title,
                ImageName = model.ImageName,
                Description = model.Description,
            };
        }
    }
}
