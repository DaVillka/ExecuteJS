using Newtonsoft.Json;
using System;

namespace HardLife.Utils
{
    internal static class Storage
    {
        public static void Write<T>(string key, T data)
        {
            ExecuteJS.Execute<string>($"mp.storage.data.{key} = '{JsonConvert.SerializeObject(data)}';");
            ExecuteJS.Execute("mp.storage.flush();");
        }
        public static void Read<T>(string key, Action<T> result)
        {
            ExecuteJS.Execute<string>($"mp.storage.data.{key}", (r) => { result?.Invoke(JsonConvert.DeserializeObject<T>(r)); });
        }
    }
}
