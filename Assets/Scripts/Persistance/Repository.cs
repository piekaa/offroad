using System;
using System.IO;
using UnityEngine;


namespace Pieka.Persistance
{
    //todo perform it async
    public class Repository
    {
        public static void Save<T>(T t)
        {
            var path = getPath<T>();
            File.WriteAllText(path, JsonUtility.ToJson(t));
        }

        public static T Load<T>()
        {
            var path = getPath<T>();
            if (!File.Exists(path))
            {
                return default(T);
            }
            var json = File.ReadAllText(path);
            return JsonUtility.FromJson<T>(json);
        }

        public static void Delete<T>()
        {
            var path = getPath<T>();
            File.Delete(path);
        }

        private static string getPath<T>()
        {
            return Application.persistentDataPath + "/" + typeof(T).FullName + ".json";
        }
    }
}