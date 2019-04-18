using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using PC.Models;


namespace PC.Controllers
{
    public class OrderController : Controller
    {
        // GET: Order
        /// <summary>
        /// 显示页面
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();

        }

        /// <summary>
        /// 修改反填订单信息使用
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public string Edit(int id)
        {
            var s = GetOrders().Where(m => m.OrderId == id).SingleOrDefault();
            return JsonConvert.SerializeObject(s);
        }


        /// <summary>
        /// 修改订单使用
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        public string Edit(Order order)
        {
            string str = JsonConvert.SerializeObject(order);
            //使用HttpClientHelper获取所有数据
            string jsonStr = HttpClientHelper.Send("put", "api/OrderAPI/Upt", str);
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
        /// 修改发货
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string Upt(int id)
        {
            var s = GetOrders().Where(m => m.OrderId == id).SingleOrDefault();
            s.OrderState = 1;
            return Edit(s);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public string Create(User user)
        {
            string str = JsonConvert.SerializeObject(user);
            //使用HttpClientHelper获取所有数据
            string jsonStr = HttpClientHelper.Send("post", "api/OrderAPI/Create", str);
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
        public List<Order> GetOrders()
        {
            //使用HttpClientHelper获取所有数据
            string jsonStr = HttpClientHelper.Send("get", "api/OrderAPI/GetOrders", null);
            //将json数据转化为list集合 并返回
            return JsonConvert.DeserializeObject<List<Order>>(jsonStr); ;
        }


    }
}