using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace KingMarket.Utility
{
    public class Utilities
    {
        public static string GetJson<T>(T obj)
        {
            var type = obj.GetType();
            var properties = type.GetProperties();
            var postData = "{";
            var i = 1;
            foreach (var property in properties)
            {
                postData += string.Format("\"{0}\":", property.Name);
                var value = property.GetValue(obj);
                if (value == null)
                    postData += "null";
                else
                    postData += (property.PropertyType == typeof(String))
                        ? String.Format("\"{0}\"", property.GetValue(obj))
                        : property.GetValue(obj);
                postData += i.Equals(properties.Length) ? "" : ",";
                i++;
            }
            postData += "}";
            return postData;
        }

        public static T GetEntity<T>(string uri, string id)
        {
            var reqGet = (HttpWebRequest)WebRequest.Create(uri + id);
            reqGet.Method = "GET";
            var resGet = (HttpWebResponse)reqGet.GetResponse();
            var readerGet = new StreamReader(resGet.GetResponseStream());
            var entityJson = readerGet.ReadToEnd();
            var js = new JavaScriptSerializer();
            var entity = js.Deserialize<T>(entityJson);
            return entity;
        }

        public static List<T> GetEntities<T>(string uri)
        {
            var reqGet = (HttpWebRequest)WebRequest.Create(uri);
            reqGet.Method = "GET";
            var resGet = (HttpWebResponse)reqGet.GetResponse();
            var readerGet = new StreamReader(resGet.GetResponseStream());
            var entityJson = readerGet.ReadToEnd();
            var js = new JavaScriptSerializer();
            var entity = js.Deserialize<List<T>>(entityJson);
            return entity;
        }

        public static T Deserialize<T>(WebException ex)
        {
            var readerGet = new StreamReader(ex.Response.GetResponseStream());
            var entityJson = readerGet.ReadToEnd();
            var js = new JavaScriptSerializer();
            var entity = js.Deserialize<T>(entityJson);
            return entity;
        }

        public static void CreateOrEditEntity<T>(T obj, string uri, string method = "POST")
        {
            var postData = GetJson(obj);
            var data = Encoding.UTF8.GetBytes(postData);
            var reqCreate = (HttpWebRequest)WebRequest.Create(uri);
            reqCreate.Method = method;
            reqCreate.ContentLength = data.Length;
            reqCreate.ContentType = "application/json";
            var requestStream = reqCreate.GetRequestStream();
            requestStream.Write(data, 0, data.Length);
            reqCreate.GetResponse();
        }

        public static void DeleteEntity(string uri, string id)
        {
            var reqCreate = (HttpWebRequest)WebRequest.Create(uri + id);
            reqCreate.Method = "DELETE";
            reqCreate.GetResponse();
        }

        public static void CreateEntity<T>(T obj, string uri)
        {
            CreateOrEditEntity(obj, uri);
        }

        public static void EditEntity<T>(T obj, string uri)
        {
            CreateOrEditEntity(obj, uri, "PUT");
        }
    }
}