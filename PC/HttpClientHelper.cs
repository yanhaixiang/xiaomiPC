using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Http;
using System.Net.Http.Headers;

namespace PC
{
    public class HttpClientHelper
    {
        static string staffid = "#@9793932i82`/";
        /// <summary>
        /// 发送httpclient请求
        /// </summary>
        /// <param name="method">get delete post put</param>
        /// <param name="apiMethod">/api/admin/checklogin</param>
        /// <param name="jsonStr">返回的成功或者不成功</param>
        /// <returns></returns>
        public static string Send(string method, string apiMethod, string jsonStr)
        {
            Uri uri = new Uri("http://localhost:52721/");
            HttpClient client = new HttpClient();
            client.BaseAddress = uri;
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = null;
            switch (method)
            {
                case "get"://查询
                    response = client.GetAsync(apiMethod).Result;
                    break;
                case "post"://添加
                    HttpContent content = new StringContent(jsonStr);
                    content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    response = client.PostAsync(apiMethod, content).Result;
                    break;
                case "delete"://删除
                    response = client.DeleteAsync(apiMethod).Result;
                    break;
                case "put"://修改
                    HttpContent content1 = new StringContent(jsonStr);
                    content1.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    response = client.PutAsync(apiMethod, content1).Result;
                    break;
                default:
                    break;
            }
            //返回读取结果
            if (response.IsSuccessStatusCode)
            {
                return response.Content.ReadAsStringAsync().Result;
            }
            else
            {
                return "未知原因，失败";
            }

        }
    }
}