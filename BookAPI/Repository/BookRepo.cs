using BookAPI.Services.Interface;
using DB;
using DB.Model;
using Microsoft.EntityFrameworkCore;
using BookAPI.Hepler;

namespace BookAPI.Services
{
    public class BookRepo : IBookRepo
    {
        readonly BookContext _bookContext;
        public BookRepo(BookContext bookContext)
        {
            _bookContext = bookContext;
        }

        public async Task<int> Add(Book model)
        {
            await _bookContext.Book.AddAsync(model);
            await _bookContext.SaveChangesAsync();
            return model.Id;

        }

        public async Task<bool> Delete(int id)
        {
            var data = await _bookContext.Book.FindAsync(id);
            _bookContext.Book.Remove(data);
            await _bookContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Update(Book model)
        {
            _bookContext.Book.Update(model);
            await _bookContext.SaveChangesAsync();
            return true;
        }
        public async Task<PagedList<Book>> GetBook(BookParameters bookParameters)
        {
            return PagedList<Book>.ToPagedList(FindAll().OrderBy(on => on.Id),
                        bookParameters.PageNumber,
                        bookParameters.PageSize);
        }
        public async Task<Book> GetBookById(int id)
        {
            return await _bookContext.Book.Where(x => x.Id == id).SingleAsync();
        }
        public IQueryable<Book> FindAll()
        {
            return this._bookContext.Set<Book>();
        }

    }
}
