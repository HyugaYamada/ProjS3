using JWTAuth.WebApi.Models;
using khiemnguyen.WebApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace khiemnguyen.WebApi.Controllers
{
    [ApiController]
    public class AdminController : Controller
    {
        private DatabaseContext _db;

        public AdminController(DatabaseContext db)
        {
            _db = db;
        }

        [HttpPost("AddUser")]
        public async Task<ActionResult<IEnumerable<UserInfo>>> AddUser(UserInfo UserData)
        {
            UserData.CreatedDate = DateTime.Now;
            try
            {
                _db.UserInfos.Add(UserData);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                return NotFound(ex.InnerException);
            }

            return Ok(UserData);
        }


        [HttpPost("AddFood")]
        public async Task<ActionResult<IEnumerable<Food>>> AddFood(Food FoodData)
        {

            try
            {
                _db.Add(FoodData);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                return NotFound(ex.InnerException);
            }

            return Ok(FoodData);
        }

        [HttpPost("AddCategory")]
        public async Task<ActionResult<IEnumerable<Category>>> AddCategory(Category CategoryData)
        {

            try
            {
                _db.Add(CategoryData);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                return NotFound(ex.InnerException);
            }

            return Ok(CategoryData);
        }

        [HttpPost("AddMenuTags")]
        public async Task<ActionResult<IEnumerable<Menu_Tag>>> AddMenuTags(Menu_Tag MenuTagsData)
        {

            try
            {
                _db.Add(MenuTagsData);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                return NotFound(ex.InnerException);
            }

            return Ok(MenuTagsData);
        }

        [HttpPost("AddMessage")]
        public async Task<ActionResult<IEnumerable<Message>>> AddMessage(Message MessageData)
        {

            try
            {
                _db.Add(MessageData);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                return NotFound(ex.InnerException);
            }

            return Ok(MessageData);
        }




    }
}
