# ExecuteJS  
Custom ExecuteJS to RAGE MP

# Usage
```csharp
/* JS Code
function test(){
    return 228;
}*/
//test() - called func, int - callback result type
ExecuteJS.Execute<int>("test()", (r) => { RAGE.Ui.Console.Log(RAGE.Ui.ConsoleVerbosity.Info, $"Result: {r}"); });
ExecuteJS.Execute("test()");
```

# Example Storage
```csharp
internal static class Storage
    {
        public static void Write<T>(string key, T data)
        {
            //Convert object to json string and call storage date
            ExecuteJS.Execute($"mp.storage.data.{key} = '{JsonConvert.SerializeObject(data)}';");
            //call save storage
            ExecuteJS.Execute("mp.storage.flush();");
        }
        //Result - convert to call type and invoke result
        public static void Read<T>(string key, Action<T> result)
        {
            ExecuteJS.Execute<string>($"mp.storage.data.{key}", (r) => { result?.Invoke(JsonConvert.DeserializeObject<T>(r)); });
        }
    }
```
