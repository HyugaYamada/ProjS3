using JWTAuth.WebApi.Interface;
using JWTAuth.WebApi.Models;
using khiemnguyen.WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Security.Cryptography.Xml;

namespace khiemnguyen.WebApi.Controllers
{

    //[Route("api/caterer")]
    [ApiController]
    public class CatererController : Controller
    {
        private DatabaseContext _db;
        public CatererController(DatabaseContext db)
        {
            _db = db;   
        }

   

        [HttpGet("GetCatererDetailsbyID/{id}")]
        public async Task<ActionResult<IEnumerable<UserInfo>>> GetCatererDetailsbyID(int id)
        {
            return await Task.FromResult(_db.UserInfos.Where(x=>x.UserId==id).ToList());
        }

        [HttpGet("GetCatererDetailsbyEmail/{Email}")]
        public async Task<ActionResult<IEnumerable<UserInfo>>> GetCatererDetailsbyEmail(string Email)
        {
            return await Task.FromResult(_db.UserInfos.Where(x => x.Email == Email).ToList());
        }

        [HttpGet("GetMenubyID/{id}")]
        public async Task<ActionResult<IEnumerable<Menu>>> GetMenubyID(int id)
        {
            return await Task.FromResult(_db.Menues.Where(x => x.id == id).ToList());
        }

        [HttpGet("GetMenubyCater/{id}")]
        public async Task<ActionResult<IEnumerable<Menu>>> GetMenubyUser(int id)
        {
            return await Task.FromResult(_db.Menues.Where(x => x.CaterID == id).ToList());
        }

		[HttpGet("GetMenuFoodsbyMenuID/{id}")]
		public async Task<ActionResult<IEnumerable<Food_in_Menu>>> GetMenuFoodsbyMenuID(int id)
		{
			var data = (from items in _db.Food_In_Menus
						join Food in _db.Foods on items.Foodid equals Food.id where items.Menuid==id
						select new Food_in_Menu { Menuid = items.Menuid, Name=Food.Name,Foodid=Food.id,id=items.id }).ToList();
	

			return await Task.FromResult(data);
		}

		[HttpGet("GetFoodsList")]
		public async Task<ActionResult<IEnumerable<Food>>> GetFoodsList()
		{
			return await Task.FromResult(_db.Foods.ToList());
		}

		[HttpGet("GetCategoryList")]
		public async Task<ActionResult<IEnumerable<Category>>> GetCategoryList()
		{
			return await Task.FromResult(_db.Categories.ToList());
		}



		[HttpGet("GetUserbyID/{id}")]
		public async Task<ActionResult<IEnumerable<UserInfo>>> GetUserbyID(int id)
		{
			return await Task.FromResult(_db.UserInfos.Where(x=>x.UserId==id).ToList());
		}

		[HttpGet("GetFeedBackbyID/{id}")]
		public async Task<ActionResult<IEnumerable<FeedBack>>> GetFeedBackbyID(int id)
		{
			return await Task.FromResult(_db.FeedBacks.Where(x=>x.Menuid==id).ToList());
		}



		[HttpPost("UpdateMenu")]
		public async Task<ActionResult<IEnumerable<Menu>>> UpdateMenu(Menu MenuData)
		{
          
            
            if (MenuData.Image == null)
            {
				var _db2 = _db.Menues.Where(x => x.id == MenuData.id).FirstOrDefault();
				MenuData.Image = _db2.Image;
            }

			try
			{
				_db.Menues.Update(MenuData);
				_db.SaveChanges();
			}
			catch (Exception ex)
			{
				return NotFound(ex.InnerException);
			}

			return Ok(MenuData);
		}


		[HttpPost("AddMenu")]
        public async Task<ActionResult<IEnumerable<Menu>>> AddMenu(Menu MenuData)
        {
          
            try
            {
                _db.Menues.Add(MenuData);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                return NotFound(ex.InnerException);
            }

            return Ok(MenuData);
        }

		[HttpPost("AddItem")]
		public async Task<ActionResult<IEnumerable<Menu>>> AddItem(Food MenuData)
		{

			try
			{
				_db.Foods.Add(MenuData);
				_db.SaveChanges();
			}
			catch (Exception ex)
			{
				return NotFound(ex.InnerException);
			}

			return Ok(MenuData);
		}


		[HttpGet("DeleteItems/{id}")]
		public async Task<ActionResult<IEnumerable<Food>>> DeleteItems(int id)
		{

            var Data = new Food { id = id };
			try
			{
				_db.Foods.Remove(Data);
				_db.SaveChanges();
			}
			catch (Exception ex)
			{
				return NotFound(ex.InnerException);
			}

			return Ok(_db.Foods.ToList());
		}

		[HttpPost("DeleteMenuItems")]
		public async Task<ActionResult<IEnumerable<Menu>>> DeleteMenuItems(Food_in_Menu FiM)
		{
           
            

			try
			{
                _db.Food_In_Menus.Remove(FiM);
				_db.SaveChanges();
			}
			catch (Exception ex)
			{
				return NotFound(ex.InnerException);
			}

			return Ok();
		}


		[HttpPost("AddItemstoMenu")]
        public async Task<ActionResult<IEnumerable<Food_in_Menu>>> AddItemstoMenu(Food_in_Menu MenuItemsData)
        {

            try
            {
                _db.Add(MenuItemsData);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                return NotFound(ex.InnerException);
            }

            return Ok(MenuItemsData);
        }

        [HttpPost("UpdateUserInfo")]
        public async Task<ActionResult<IEnumerable<UserInfo>>> UpdateUserInfo(UserInfo UserData)
        {

            try
            {
                _db.Update(UserData);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.ToString());
            }

            return Ok(UserData);
        }


        [HttpPost("ResetPassowrd")]
        public async Task<ActionResult<IEnumerable<ResetPassowrdDTO>>> ResetPassowrd(ResetPassowrdDTO UserData)
        {

           var data=_db.UserInfos.Where(x=>x.UserId==UserData.Userid).FirstOrDefault();

            if (data == null)
            {
                return BadRequest("Invalid user ID");
            }else if (data.Password!=UserData.OldPassword)
            {
                return BadRequest("Old Password Mismatch");
            }else if (UserData.NewPassword.Trim()=="")
            {
                return BadRequest("New Password Error!");
            }
            else
            {
                data.Password= UserData.NewPassword;
            }

            try
            {

                _db.Update(data);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.ToString());
            }

            return Ok(UserData);
        }




        [HttpPost("AddOrder")]
        public async Task<ActionResult<IEnumerable<Order>>> AddOrder(Order OrderData)
        {

            try
            {
                _db.Add(OrderData);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                return NotFound(ex.InnerException);
            }

            return Ok(OrderData);
        }

        [HttpPost("AddFeedBack")]
        public async Task<ActionResult<IEnumerable<FeedBack>>> AddFeedBack(FeedBack FeedBackData)
        {

            try
            {
                _db.Add(FeedBackData);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                return NotFound(ex.InnerException);
            }

            return Ok(FeedBackData);
        }


        [HttpPost("AddTagInclude")]
        public async Task<ActionResult<IEnumerable<Message>>> AddTagInclude(Tag_Include TagIncludeData)
        {

            try
            {
                _db.Add(TagIncludeData);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                return NotFound(ex.InnerException);
            }

            return Ok(TagIncludeData);
        }


    }
}
