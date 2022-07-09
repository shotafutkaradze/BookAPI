using BookAPI.Services.Interface;
using DB.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Annotations;


namespace BookAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [SwaggerTag("წიგნების მოდული")]
    public class BookController : ControllerBase
    {
        private readonly IBookRepo _bookrepo;
        public BookController(IBookRepo bookrepo)
        {
            _bookrepo = bookrepo;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetBook([FromQuery] BookParameters bookParameters)
        {
            var model = await _bookrepo.GetBook(bookParameters);
            if (model == null)
            {
                return BadRequest("წიგნების ლისტი არ მოიძებნა");
            }
            var metadata = new
            {
                model.TotalCount,
                model.PageSize,
                model.CurrentPage,
                model.TotalPages,
                model.HasNext,
                model.HasPrevious
            };
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
            return Ok(model);
        }
        [HttpGet("id")]
        public async Task<IActionResult> GetBookById(int id)
        {
            var model = await _bookrepo.GetBookById(id);
            if (model == null)
            {
                return BadRequest($"წიგნი ID {id} არ მოიძებნა");
            }
            return Ok(model);
        }
        [HttpPost]
        public async Task<IActionResult> Add(Book model)
        {
            var Model = await _bookrepo.Add(model);
            if (Model == null)
            {
                return BadRequest("მოხდა შეცდომა");
            }
            return Ok("ჩანაწერი წარმატებით დაემატა");
        }
        [HttpDelete]
        public async Task<bool> Delete(int id)
        {
            await _bookrepo.Delete(id);
            return true;
        }
        [HttpPut]
        public async Task<bool> Update(Book model)
        {
            await _bookrepo.Update(model);
            return true;
        }

    }
}
