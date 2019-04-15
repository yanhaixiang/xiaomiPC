using Newtonsoft.Json;
using PC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PC.Controllers
{
    public class AddressController : Controller
    {
        // GET: Address
        /// <summary>
        /// 显示页面
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View(GetAddresss());
        }

        /// <summary>
        /// 修改反填商品信息使用
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var s = GetAddresss().Where(m => m.AddressId == id).SingleOrDefault();
            return View(s);
        }


        /// <summary>
        /// 修改订单使用
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        public string Edit(Address address)
        {
            string str = JsonConvert.SerializeObject(address);
            //使用HttpClientHelper获取所有数据
            string jsonStr = HttpClientHelper.Send("put", "api/AddressAPI/Upt", str);
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
        public string Create(Address address)
        {
            //address.UserId = int.Parse(Session["UserId"].ToString());
            address.UserId = 1;
            string str = JsonConvert.SerializeObject(address);
            //使用HttpClientHelper获取所有数据
            string jsonStr = HttpClientHelper.Send("post", "api/AddressAPI/Create", str);
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
        public List<Address> GetAddresss()
        {
            //使用HttpClientHelper获取所有数据
            string jsonStr = HttpClientHelper.Send("get", "api/AddressAPI/GetAddresss", null);
            //将json数据转化为list集合 并返回
            return JsonConvert.DeserializeObject<List<Address>>(jsonStr); ;
        }
    }
}