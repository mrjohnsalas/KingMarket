using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using KingMarket.Model.Models;
using System.Messaging;

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

        public static void WriteQueue<T>(string pathQueue, T obj, string label)
        {
            if (!MessageQueue.Exists(pathQueue))
                MessageQueue.Create(pathQueue);
            var queue = new MessageQueue(pathQueue);
            var message = new Message()
            {
                Label = label,
                Body = obj
            };
            queue.Send(message);
            queue.Dispose();
        }

        public static List<T> ReadQueue<T>(string pathQueue, bool deleteMessages = false)
        {
            try
            {
                if (!MessageQueue.Exists(pathQueue))
                    return null;
                var entities = new List<T>();
                using (var queue = new MessageQueue(pathQueue))
                {
                    queue.Formatter = new XmlMessageFormatter(new[] { typeof(T) });
                    var messages = queue.GetAllMessages();
                    entities.AddRange(messages.Select(message => (T)message.Body));
                    if (deleteMessages)
                        queue.Purge();
                }
                return entities;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static FaultException<GeneralException> GetException(Exception ex, string action)
        {
            SqlException sqlException = null;
            var tmp = ex;
            while (sqlException == null && tmp != null)
            {
                sqlException = tmp.InnerException as SqlException;
                tmp = tmp.InnerException;
            }
            if (sqlException == null) return null;
            if (sqlException.Number.Equals(2601))
            {
                return new FaultException<GeneralException>(new GeneralException()
                {
                    Id = sqlException.Number.ToString(),
                    Description = string.Format("Cannot insert duplicate value. The duplicate key value is: {0}", sqlException.Message.Split('(', ')')[1])
                }, new FaultReason(string.Format("Error when trying to {0}", action)));
            }
            if (sqlException.Number.Equals(547))
            {
                return new FaultException<GeneralException>(new GeneralException()
                {
                    Id = sqlException.Number.ToString(),
                    Description = "Can not be deleted, there are records referenced."
                }, new FaultReason(string.Format("Error when trying to {0}", action)));
            }
            return new FaultException<GeneralException>(new GeneralException()
            {
                Id = sqlException.Number.ToString(),
                Description = sqlException.Message
            }, new FaultReason(string.Format("Error when trying to {0}", action)));
        }
    }
}