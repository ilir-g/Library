using IG_CoreLibrary;
using IG_CoreLibrary.Models;
using IG_CoreLibrary.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using FromBodyAttribute = System.Web.Http.FromBodyAttribute;
using HttpGetAttribute = System.Web.Http.HttpGetAttribute;
using HttpPostAttribute = System.Web.Http.HttpPostAttribute;
using RouteAttribute = System.Web.Http.RouteAttribute;

namespace IG_WebApi.Controllers
{
    [RoutePrefix("api/[controller]")]
    public class PhoneBookController : ApiController
    {
        private static IBaseRepository Repository = Globals.BaseRepository;

        [Route("GetAll")]
        [HttpGet]
        public async Task<IHttpActionResult> ReadAllPhoneBooks()
        {
            var result = await Repository.ReadAllPhoneBooks();
            return Ok(result);
        }

        [Route("Create")]
        [HttpPost]
        public async Task<ResponseModel<PhoneBookBase>> CreatePhoneBook([FromBody] PhoneBookBase phoneBookBase)
        {
            return await Repository.CreatePhoneBook(phoneBookBase);
        }

        [Route("Update")]
        [HttpPost]
        public async Task<ResponseModel<PhoneBookBase>> UpdatePhoneBook([FromBody] PhoneBookBase phoneBookBase)
        {
            return await Repository.UpdatePhoneBook(phoneBookBase);
        }

        [Route("Delete")]
        [HttpPost]
        public async Task<ResponseModel<PhoneBookBase>> DeletePhoneBook([FromBody] PhoneBookBase phoneBookBase)
        {
            return await Repository.DeletePhoneBook(phoneBookBase);
        }

        [Route("OrderByFirtLastName")]
        [HttpGet]
        public async Task<IEnumerable<PhoneBookBase>> OrderByFirtLastNamePhoneBooks([FromUri] bool firstNameOrder=false,[FromUri] bool lastNameOrder=false)
        {
            return await Repository.OrderByFirtLastNamePhoneBooks(firstNameOrder,lastNameOrder);
        }
    }
}
