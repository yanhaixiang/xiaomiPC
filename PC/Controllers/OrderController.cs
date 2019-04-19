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
        /// 修改订单使用 使用的Vue 修改完之后不用返回任何东西
        /// </summary>
        /// <param name="user"></param>
        [HttpPost]
        public void Edit(Order order)
        {
            //获取随机数
            var nonce = DataTransfer.GetNonce();
            //获取时间戳
            var timestamp = DataTransfer.GetTimeStamp();
            //秘钥
            var staffid = "#9793932i82`/";
            //data数据有时间戳+随机数+秘钥+传入数据
            var singture = timestamp + nonce + staffid;
            //反射获取传递的属性和值
            Type type = typeof(Order);
            //获取所有属性
            PropertyInfo[] pros = type.GetProperties();
            //定义一个有序字典
            SortedDictionary<string,string> pairs = new SortedDictionary<string, string>();
            //遍历属性
            foreach (var item in pros)
            {
                //将order属性和属性值放入字典
                pairs.Add(item.Name.ToString(), item.GetValue(order, null).ToString());
            }
            //进行有序排列
            foreach (var item in pairs.OrderBy(o => o.Key))
            {
                //data数据+到stigture
                singture += item.Key + item.Value.ToString();
            }
            //json序列化
            string str = JsonConvert.SerializeObject(order);
            //使用HttpClientHelper获取所有数据
            string jsonStr = HttpClientHelper2.SendRequest("api/OrderAPI/Upt", "put", timestamp, nonce.ToString(),singture , str);
            //string jsonStr = HttpClientHelper.Send("put", "api/OrderAPI/Upt", str);
        }

        /// <summary>
        /// 修改发货
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public void Upt(int id)
        {
            //先进行反填这一步
            var s = GetOrders().Where(m => m.OrderId == id).SingleOrDefault();
            //修改原有信息
            s.OrderState = 1;
            //在整个方法进行修改
            Edit(s);
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
            //singture参数是有时间戳+随机数+秘钥+数据
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