using HardLife.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace HardLife.Utils
{
    internal class ExecuteJSDate
    {
        public Type Type { get; private set; }
        public Action<object> Action { get; private set; }
        public ExecuteJSDate(Type type, Action<object> action)
        {
            Type = type;
            Action = action;
        }
    }
}
internal static class ExecuteJS
{
    private static readonly Dictionary<int, ExecuteJSDate> _callback = new Dictionary<int, ExecuteJSDate>();
    private static int _id = 0;
    static ExecuteJS()
    {
        RAGE.Events.Add("executeJSCallback", ExecuteJSCallback);
    }
    private static void ExecuteJSCallback(object[] args)
    {
        //RAGE.Ui.Console.Log(RAGE.Ui.ConsoleVerbosity.Info, $"ExecuteJSCallback: {args[1]}");
        if (_callback.TryGetValue((int)args[0], out ExecuteJSDate data) && data.Action != null)
        {
            object result = Convert.ChangeType(JsonConvert.DeserializeObject((string)args[1]), data.Type);
            data.Action.Invoke(result);
        }
        if (_callback.ContainsKey((int)args[0])) _callback.Remove((int)args[0]);
    }
    public static void Execute<T>(string code, Action<T> result = null)
    {
        void action(object o) => result?.Invoke((T)o);
        _callback.Add(_id, new ExecuteJSDate(typeof(T), action));
        RAGE.Events.CallLocal("executeJS", code, _id);
        _id++;
    }
    public static void Execute(string code)
    {
        RAGE.Events.CallLocal("executeJS", code);
    }
}