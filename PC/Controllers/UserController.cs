using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using PC.Models;

namespace PC.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        /// <summary>
        /// 显示页面
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View(GetUsers());
        }

        /// <summary>
        /// 登陆页面使用
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        /// <summary>
        /// 登陆提交使用
        /// </summary>
        /// <param name="UserName">用户名</param>
        /// <param name="UserPwd">密码</param>
        /// <returns></returns>
        [HttpPost]
        public User Login(string UserName,string UserPwd)
        {
            User user = GetUsers().Where(m => m.UserName.Equals(UserName) && m.UserPwd.Equals(UserPwd)).SingleOrDefault();
            Session["UserId"] = user.UserId;
            Session["UserIntegral"] = user.UserIntegral;
            Session["UserMember"] = user.UserMember;
            Session["UserName"] = user.UserName;
            Session["UserPhone"] = user.UserPhone;
            Session["UserPwd"] = user.UserPwd;
            return user;
        }

        /// <summary>
        /// 修改用户信息使用
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var s = GetUsers().Where(m => m.UserId == id).SingleOrDefault();
            return View(s);
        }

        [HttpPost]
        public string Edit(User user)
        {
            string str = JsonConvert.SerializeObject(user);
            //使用HttpClientHelper获取所有数据
            string jsonStr = HttpClientHelper.Send("put", "api/UserAPI/Upt",str);
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
        public string Create(User user)
        {
            string str = JsonConvert.SerializeObject(user);
            //使用HttpClientHelper获取所有数据
            string jsonStr = HttpClientHelper.Send("post", "api/UserAPI/Create", str);
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
        /// 获取到所有的USer用户
        /// </summary>
        /// <returns></returns>
        public List<User> GetUsers()
        {
            //使用HttpClientHelper获取所有数据
            string jsonStr = HttpClientHelper.Send("get", "api/UserAPI/GetUsers", null);
            //将json数据转化为list集合 并返回
            return JsonConvert.DeserializeObject<List<User>>(jsonStr); ;
        }
    }
}