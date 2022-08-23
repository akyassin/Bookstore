using Bookstore.Models.Mapper;
using Bookstore.Models.Repositories;
using Bookstore.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore.Controllers
{
    public class BookController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IWebHostEnvironment host;

        public BookController(IUnitOfWork unitOfWork, IWebHostEnvironment host)
        {
            this.unitOfWork = unitOfWork;
            this.host = host;
        }


        // GET: BookController
        public async Task<ActionResult> Index()
        {
            var books = await unitOfWork.BookRepository.GetAll();
            return View(books);
        }

        // GET: BookController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var book = await unitOfWork.BookRepository.Get(id);
            return View(book);
        }

        // GET: BookController/Create
        public async Task<ActionResult> Create()
        {
            var model = new BookAuthorViewModel()
            {
                Authors = await GetAuthorsSelect()
            };

            return View(model);
        }

        // POST: BookController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(BookAuthorViewModel model)
        {
            try
            {
                var fileName = TryToUploadFile(model);

                var book = Mapper.MapFrom(model);
                book.Author = await unitOfWork.AuthorRepository.Get(model.AuthorId);
                book.ImageName = fileName;

                unitOfWork.BookRepository.Add(book);
                await unitOfWork.Complete();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: BookController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var book = await unitOfWork.BookRepository.Get(id);

            var model = Mapper.MapTo(book);
            model.Authors = await GetAuthorsSelect();

            return View(model);
        }

        // POST: BookController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(BookAuthorViewModel model)
        {
            try
            {
                var fileName = TryToUploadFile(model);

                var book = Mapper.MapFrom(model);
                book.Author = await unitOfWork.AuthorRepository.Get(model.AuthorId);
                book.ImageName = fileName;

                unitOfWork.BookRepository.Update(book);
                await unitOfWork.Complete();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: BookController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var book = await unitOfWork.BookRepository.Get(id);
            return View(book);
        }


        // POST: BookController/Delete/5
        [ActionName("Delete")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteBook(int id)
        {
            try
            {
                unitOfWork.BookRepository.Delete(id);
                await unitOfWork.Complete();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // POST: BookController/RemoveImage/5
        [HttpPost]
        public async Task<ActionResult> RemoveImage(int id)
        {
            try
            {
                var book = await unitOfWork.BookRepository.Get(id);
                var filenUrl = Path.Combine(host.WebRootPath, "uploads", book.ImageName);

                if (!await unitOfWork.BookRepository.IConnectedToBook(book.ImageName))
                    System.IO.File.Delete(filenUrl);

                book.ImageName = string.Empty;
                unitOfWork.BookRepository.Update(book);
                await unitOfWork.Complete();

                return RedirectToAction("Edit", new { id });
            }
            catch
            {
                return View();
            }
        }

        public async Task<ActionResult> Search(string term)
        {
            var result = await unitOfWork.BookRepository.Serach(term);
            return View("Index", result);
        }

        private async Task<ActionResult> GetBookAndReturnView(int id)
        {
            var book = await unitOfWork.BookRepository.Get(id);
            return View(book);
        }

        private async Task<List<AuthorViewModel>> GetAuthorsSelect()
        {
            var authors = await unitOfWork.AuthorRepository.GetAll();
            var mappedAuthors = Mapper.MapTo(authors.ToList());

            mappedAuthors.Insert(0, new AuthorViewModel() { Id = null, FullName = "---- Please Select an Author ----" });
            return mappedAuthors;
        }

        private string TryToUploadFile(BookAuthorViewModel model)
        {
            if (model.File != null)
            {
                var fileUrl = Path.Combine(host.WebRootPath, "uploads", model.File.FileName);

                using (FileStream stream = new FileStream(fileUrl, FileMode.Create))
                {
                    model.File.CopyTo(stream);
                    return model.File.FileName;
                }
            }
            else if (!String.IsNullOrEmpty(model.ImageName))
            {
                return model.ImageName;
            }
            return string.Empty;
        }
    }
}
