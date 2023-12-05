using khiemnguyen_FrontEnd.Models;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Reflection;
using static System.Net.Mime.MediaTypeNames;

namespace khiemnguyen_FrontEnd.Controllers
{
    public class CatererController : Controller
    {

        APIControl Control=new APIControl();
        public IActionResult Index()
        {
			
			ViewBag.Cid = APIControl.UserID;
		

			IEnumerable<Menu> Mnu = Control.GetEndPoint<Menu>("/GetMenubyCater/"+APIControl.UserID);
                return View(Mnu);
        }


		public IActionResult ViewItems()
		{
			IEnumerable<Category> Cate = Control.GetEndPoint<Category>("/GetCategoryList/" );
			ViewBag.cate = Cate;
			IEnumerable<Foods> Food = Control.GetEndPoint<Foods>("/GetFoodsList/");
			ViewBag.food = Food;
			Foods fd = new Foods();
			return View(fd);
		}

	
		public IActionResult DeleteFood(int id)
		{
			
			IEnumerable<Foods> food = Control.GetEndPoint<Foods>("/DeleteItems/"+id);

			IEnumerable<Category> Cate = Control.GetEndPoint<Category>("/GetCategoryList/");
			ViewBag.cate = Cate;
			IEnumerable<Foods> Food = Control.GetEndPoint<Foods>("/GetFoodsList/");
			ViewBag.food = Food;
		
			return RedirectToAction("Index");
		}

		[HttpPost]
		public IActionResult AddItemPOST(Foods model, IFormFile postedFile)
		{

			if (Request.Form.Files.Count > 0)
			{
				IFormFile image = Request.Form.Files[0];

				var fileName = Path.GetFileName(image.FileName);



				using (var target = new MemoryStream())
				{
					image.CopyTo(target);
					model.Image = target.ToArray();

				}

			}

			model.Caterid = APIControl.UserID;
			model.Category = model.Name;
			IEnumerable<Foods> ss = Control.PostEndPoint<Foods>("/Addfood/", model);

			return RedirectToAction("ViewItems");
		}
		public IActionResult CreateMenu(int Cid)
		{

		
			IEnumerable<Foods> Items = Control.GetEndPoint<Foods>("/GetFoodsList/");

			ViewBag.Foods = Items;
			var Mnu = new Menu { CaterID=Cid};

			
			return View(Mnu);
		}


		[HttpPost]
		public IActionResult CreateMenuPost(Menu model)
		{
			if (Request.Form.Files.Count > 0)
			{
				IFormFile image = Request.Form.Files[0];

				var fileName = Path.GetFileName(image.FileName);



				using (var target = new MemoryStream())
				{
					image.CopyTo(target);
					model.Image = target.ToArray();

				}
			}

			IEnumerable<Menu> ss = Control.PostEndPoint<Menu>("/Addmenu/", model);


			return RedirectToAction("Index");
		}


		[HttpPost]
		public IActionResult EditMenuPOST(Menu model, IFormFile postedFile)
		{
            if (Request.Form.Files.Count > 0)
            {
                IFormFile image = Request.Form.Files[0];

                var fileName = Path.GetFileName(image.FileName);



                using (var target = new MemoryStream())
                {
                    image.CopyTo(target);
                    model.Image = target.ToArray();

                }
            }
            

           IEnumerable<Menu>  ss= Control.PostEndPoint<Menu>("/Updatemenu/", model);
           

            return RedirectToAction("Editmenu", new {id=model.id });
		}
		public IActionResult EditMenu(int id)
		{

			IEnumerable<Menu> Mnu = Control.GetEndPoint<Menu>("/GetMenubyID/" + id);

			IEnumerable<Foods> Items = Control.GetEndPoint<Foods>("/GetFoodsList/");
			IEnumerable<Food_in_Menu> MnuItems = Control.GetEndPoint<Food_in_Menu>("/GetMenuFoodsbyMenuID/" + id);
            ViewBag.MenuFoods = MnuItems;
            ViewBag.Foods = Items;
			return View(Mnu.FirstOrDefault());
		}

        public IActionResult additemstoMenu()
        {
            var Menuid = Request.Form["menuid"].ToString();
			var itemid = Request.Form["id"].ToString();
            
            var MenuItems = new Food_in_Menu { 
               
            Menuid=int.Parse(Menuid),
            Foodid=int.Parse(itemid)
            };
			
            IEnumerable<Food_in_Menu> ss = Control.PostEndPoint<Food_in_Menu>("/AddItemstoMenu/", MenuItems);

			return RedirectToAction("EditMenu", new { id = Menuid });
        }
		public IActionResult DeleteMenuItem(int id,int mid)
		{
			Food_in_Menu FiM = new Food_in_Menu { id = id };

			IEnumerable<Food_in_Menu> ss = Control.PostEndPoint<Food_in_Menu>("/DeleteMenuItems/", FiM);

			return RedirectToAction("EditMenu", new { id = mid });
		}
		
	}
}
