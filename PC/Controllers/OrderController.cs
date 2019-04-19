using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
            var nonce = DataTransfer.GetNonce();
            var timestamp = DataTransfer.GetTimeStamp();
            var staffid = "#9793932i82`/";
            string str = JsonConvert.SerializeObject(order);
            var singture = timestamp + nonce + staffid;
            Type type = typeof(Order);
            PropertyInfo[] pros = type.GetProperties();
            SortedDictionary<string,string> pairs = new SortedDictionary<string, string>();
            foreach (var item in pros)
            {
                pairs.Add(item.Name.ToString(), item.GetValue(order, null).ToString());
            }
            foreach (var item in pairs.OrderBy(o => o.Key))
            {
                
                singture += item.Key + item.Value.ToString();
            }
            //使用HttpClientHelper获取所有数据
            string jsonStr = HttpClientHelper2.SendRequest("api/OrderAPI/Upt", "put", timestamp, nonce.ToString(),singture , str);
            //string jsonStr = HttpClientHelper.Send("put", "api/OrderAPI/Upt", str);
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
            var nonce = DataTransfer.GetNonce();
            var timestamp = DataTransfer.GetTimeStamp();
            var staffid = "#9793932i82`/";
            //使用HttpClientHelper获取所有数据
            string jsonStr = HttpClientHelper2.SendRequest("api/OrderAPI/GetOrders", "get", timestamp, nonce.ToString(), timestamp + nonce + staffid);
            //string jsonStr = HttpClientHelper.Send("get", "api/OrderAPI/GetOrders", null);
            //将json数据转化为list集合 并返回
            if (jsonStr != "未知原因，失败")
            {
                return JsonConvert.DeserializeObject<List<Order>>(jsonStr); ;
            }
            else
            {
                return null;
            }
            
        }


    }
}