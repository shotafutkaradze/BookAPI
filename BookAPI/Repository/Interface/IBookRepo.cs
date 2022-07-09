using BookAPI.Hepler;
using DB.Model;

namespace BookAPI.Services.Interface
{
    public interface IBookRepo
    {
        Task<PagedList<Book>> GetBook(BookParameters bookParameters);
        Task<Book> GetBookById(int id);
        Task<int> Add(Book model);
        Task<bool> Update(Book model);
        Task<bool> Delete(int id);
    }
}
