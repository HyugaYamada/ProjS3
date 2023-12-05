using khiemnguyen_FrontEnd.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace khiemnguyen_FrontEnd.Controllers
{
	public class CustomerController : Controller
	{

		APIControl Control = new APIControl();
		public IActionResult Index()
		{
			ViewBag.Cid = APIControl.UserID;
			IEnumerable<Menu> Mnu = Control.GetEndPoint<Menu>("/GetMenubyCater/" + APIControl.UserID);
			return View(Mnu);
			
		}
		public IActionResult ViewCart()
		{
					return View();

		}
		[HttpPost]
		public IActionResult AddtoCart()
		{
			string menuid = Request.Form["id"].ToString();
			string qty = Request.Form["quantity"].ToString();
			string price = Request.Form["Price"].ToString();

			var cartdata = new Cart { menuid=int.Parse(menuid),Quantity=int.Parse(qty),Price=int.Parse(price) };

			CartControl.MyCart.Add(cartdata);
			return RedirectToAction("Index");

		}



		public IActionResult SingleShop(int id)
		{

			IEnumerable<Menu> Mnu = Control.GetEndPoint<Menu>("/GetMenubyID/" + id);
			IEnumerable<UserInfo> chefData =  Control.GetEndPoint<UserInfo>("/GetUserbyID/" + id);

			ViewBag.Chef = chefData.FirstOrDefault().FullName;

			IEnumerable<FeedBack> feed = Control.GetEndPoint<FeedBack>("/GetFeedBackbyID/" + id);

			if (feed.Count() > 0)
			{
				ViewBag.Rate = feed.Sum(x => x.rate) / feed.Count();
			}
			else
			{
				ViewBag.Rate = 0;
			}
	


			IEnumerable<Food_in_Menu> MnuItems = Control.GetEndPoint<Food_in_Menu>("/GetMenuFoodsbyMenuID/" + id);
			ViewBag.MenuFoods = MnuItems;
		
			return View(Mnu.FirstOrDefault());
			
		}

		public IActionResult Checkout()
		{
			return View();
		}

		public IActionResult AboutUs()
		{
			return View();
		}

		public IActionResult ContactUs()
		{
			return View();
		}
	}
}
