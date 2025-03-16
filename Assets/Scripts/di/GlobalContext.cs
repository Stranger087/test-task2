using System.Collections.Generic;

namespace di
{
    //плейсхолдер для DI
    public class GlobalContext
    {
        private static readonly Dictionary<string, object> Instances = new();

        public static void BindInstance<T>(T instance)
        {
            Instances[typeof(T).Name] = instance;
        }

        public static T Resolve<T>()
        {
            return (T)Instances[typeof(T).Name];
        }
    }
}