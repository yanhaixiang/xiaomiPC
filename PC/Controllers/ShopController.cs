using Newtonsoft.Json;
using PC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PC.Controllers
{
    public class ShopController : Controller
    {
        // GET: Shop
        /// <summary>
        /// 显示页面
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View(GetShops());
        }

        /// <summary>
        /// 修改反填商品信息使用
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var s = GetShops().Where(m => m.ShopId == id).SingleOrDefault();
            return View(s);
        }


        /// <summary>
        /// 修改订单使用
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        public string Edit(Shop shop)
        {
            string str = JsonConvert.SerializeObject(shop);
            //使用HttpClientHelper获取所有数据
            string jsonStr = HttpClientHelper.Send("put", "api/ShopAPI/Upt", str);
            if (jsonStr != "未知原因，失败")
            {
                return "成功";
            }
            else
            {
                return "失败";
            }
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public string Create(Shop shop)
        {
            string str = JsonConvert.SerializeObject(shop);
            //使用HttpClientHelper获取所有数据
            string jsonStr = HttpClientHelper.Send("post", "api/ShopAPI/Create", str);
            if (jsonStr != "未知原因，失败")
            {
                return "成功";
            }
            else
            {
                return "失败";
            }
        }

        /// <summary>
        /// 获取到所有的订单数据
        /// </summary>
        /// <returns></returns>
        public List<Shop> GetShops()
        {
            //使用HttpClientHelper获取所有数据
            string jsonStr = HttpClientHelper.Send("get", "api/ShopAPI/GetShops", null);
            //将json数据转化为list集合 并返回
            return JsonConvert.DeserializeObject<List<Shop>>(jsonStr); ;
        }
    }
}