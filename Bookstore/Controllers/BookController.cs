using Bookstore.Mapper;
using Bookstore.Repositories.Interfaces;
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
        private readonly static string containerName = "bookstoreblob";


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
            var model = new BookViewModel()
            {
                Authors = await GetAuthorsSelect()
            };

            return View(model);
        }

        // POST: BookController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(BookViewModel model)
        {
            try
            {
                var image = await TryToUploadFile(model);
                model.ImageUrl = image.ImageUrl;
                model.ImageName = image.ImageName;

                var book = model.MapFrom();
                book.Author = await unitOfWork.AuthorRepository.Get(model.AuthorId);

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

            var model = book.MapTo();
            model.Authors = await GetAuthorsSelect();

            return View(model);
        }

        // POST: BookController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(BookViewModel model)
        {
            try
            {
                var image = await TryToUploadFile(model);
                model.ImageUrl = image.ImageUrl;
                model.ImageName = image.ImageName;

                var book = model.MapFrom();
                book.Author = await unitOfWork.AuthorRepository.Get(model.AuthorId);

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
                var bookName = (await unitOfWork.BookRepository.Get(id)).ImageName;
                unitOfWork.BookRepository.Delete(id);
                if (await unitOfWork.Complete())
                    RemoveBlobImage(bookName);

                return RedirectToAction("Index");
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
            var book = await unitOfWork.BookRepository.Get(id);
            var isDeleted = await unitOfWork.BlobRepository.DeleteBlob(book.ImageName, containerName);
            if (isDeleted)
            {
                book.ImageUrl = string.Empty;
                book.ImageName = string.Empty;
                unitOfWork.BookRepository.Update(book);
                await unitOfWork.Complete();
                return RedirectToAction("Edit", new { id });
            }
            throw new Exception(message: "Image couln't delete for some reasons..");
        }

        public async void RemoveBlobImage(string imageName)
        {
            try
            {
                await unitOfWork.BlobRepository.DeleteBlob(imageName, containerName);
            }
            catch (Exception)
            {
                throw new Exception(message: "Image couln't delete for some reasons..");
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
            var mappedAuthors = authors.ToList().MapTo();

            mappedAuthors.Insert(0, new AuthorViewModel() { Id = null, FullName = "---- Please Select an Author ----" });
            return mappedAuthors;
        }

        private async Task<ImageViewModel> TryToUploadFile(BookViewModel model)
        {
            if (model.File != null)
            {
                var fileName = GetRandomBlobName(model.File.FileName);
                var isUploaded = await unitOfWork.BlobRepository.UploadBlob(fileName, model.File, containerName);

                if (isUploaded)
                {
                    return new ImageViewModel()
                    {
                        ImageName = fileName,
                        ImageUrl = unitOfWork.BlobRepository.GetBlob(fileName, containerName)
                    };
                }
                else
                {
                    throw new Exception(message: "Image could not upload..");
                }
            }
            return new ImageViewModel()
            {
                ImageName = model.ImageName,
                ImageUrl = model.ImageUrl
            };
        }

        private string GetRandomBlobName(string fileName)
        {
            var ext = Path.GetExtension(fileName);
            return string.Format("{0:10}_{1}_{2}{3}", DateTime.Now.Ticks, Guid.NewGuid(), fileName[..4], ext);
        }
    }
}
