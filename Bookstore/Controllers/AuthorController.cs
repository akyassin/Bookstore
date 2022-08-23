using Bookstore.Models;
using Bookstore.Models.Repositories;
using Bookstore.Models.Repositories.EntityRepositories;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Bookstore.Controllers
{
    public class AuthorController : Controller
    {
        private readonly IUnitOfWork unitOfWork;

        public AuthorController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        // GET: AuthorController
        public async Task<ActionResult> Index()
        {
            var authors = await unitOfWork.AuthorRepository.GetAll();
            return View(authors);
        }

        // GET: AuthorController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            return await GetAuthor(id);
        }

        // GET: AuthorController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AuthorController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Author author)
        {
            try
            {
                unitOfWork.AuthorRepository.Add(author);
                await unitOfWork.Complete();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AuthorController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            return await GetAuthor(id);
        }

        // POST: AuthorController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Author author)
        {
            try
            {
                unitOfWork.AuthorRepository.Update(author);
                await unitOfWork.Complete();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AuthorController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            return await GetAuthor(id);
        }

        // POST: AuthorController/Delete/5
        [ActionName("Delete")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteAuthor(int id)
        {
            try
            {
                unitOfWork.AuthorRepository.Delete(id);
                await unitOfWork.Complete();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public async Task<ActionResult> Search(string term)
        {
            var result = await unitOfWork.AuthorRepository.Serach(term);
            return View("Index", result);
        }

        private async Task<ActionResult> GetAuthor(int id)
        {
            var author = await unitOfWork.AuthorRepository.Get(id);
            return View(author);
        }
    }
}
