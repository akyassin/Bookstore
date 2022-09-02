using Bookstore.ViewModels;
using DataAccess.Entities;
using System.Collections.Generic;

namespace Bookstore.Mapper
{
    public static class Mapper
    {
        public static List<AuthorViewModel> MapTo(this List<Author> authors)
        {
            List<AuthorViewModel> authorsList = new();
            foreach (var author in authors)
            {
                authorsList.Add(new AuthorViewModel()
                {
                    Id = author.AuthorId,
                    FullName = author.FullName
                });
            }
            return authorsList;
        }
        public static List<Author> MapFrom(this List<AuthorViewModel> models)
        {
            List<Author> authorsList = new();
            foreach (var model in models)
            {
                authorsList.Add(new Author()
                {
                    AuthorId = (int)model.Id,
                    FullName = model.FullName
                });
            }
            return authorsList;
        }

        public static AuthorViewModel MapTo(this Author author)
        {
            return new AuthorViewModel()
            {
                Id = author.AuthorId,
                FullName = author.FullName
            };
        }

        public static Author MapFrom(this AuthorViewModel model)
        {
            return new Author()
            {
                AuthorId = (int)model.Id,
                FullName = model.FullName
            };
        }

        public static List<BookViewModel> MapTo(this List<Book> books)
        {
            List<BookViewModel> booksList = new();
            foreach (var book in books)
            {
                booksList.Add(new BookViewModel()
                {
                    Id = book.BookId,
                    Title = book.Title,
                    Description = book.Description,
                    AuthorId = book.Author.AuthorId,
                    ImageUrl = book.ImageUrl,
                    ImageName = book.ImageName,
                });
            }
            return booksList;
        }

        public static List<Book> MapFrom(this List<BookViewModel> models)
        {
            List<Book> booksList = new();
            foreach (var model in models)
            {
                booksList.Add(new Book()
                {
                    BookId = model.Id,
                    Title = model.Title,
                    ImageUrl = model.ImageUrl,
                    ImageName = model.ImageName,
                    Description = model.Description,
                });
            }
            return booksList;
        }

        public static BookViewModel MapTo(this Book book)
        {
            return new BookViewModel()
            {
                Id = book.BookId,
                Title = book.Title,
                Description = book.Description,
                AuthorId = book.Author.AuthorId,
                ImageUrl = book.ImageUrl,
                ImageName = book.ImageName,
            };
        }

        public static Book MapFrom(this BookViewModel model)
        {
            return new Book()
            {
                BookId = model.Id,
                Title = model.Title,
                ImageUrl = model.ImageUrl,
                ImageName = model.ImageName,
                Description = model.Description,
            };
        }
    }
}
