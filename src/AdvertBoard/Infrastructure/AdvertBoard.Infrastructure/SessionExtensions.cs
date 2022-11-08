using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvertBoard.Infrastructure
{
    public static class SessionExtensions
    {

        public static void SetJson(this ISession session, string key, object value)
        {
            session.SetJson(key, JsonConvert.SerializeObject(value));
        }

        public static T GetJsonMethod<T>(this ISession session, string key)
        {
            string sessionData = session.TryGetValue(key, out var data).ToString();
            return sessionData == null ? default(T) : JsonConvert.DeserializeObject<T>(sessionData);
        }
    }
}
