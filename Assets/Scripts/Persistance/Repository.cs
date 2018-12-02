using System;
using System.IO;
using UnityEngine;
using System.Threading;

public delegate void ResultDelegate<T>(T result);

public class Repository
{
    public static string DefaultLocation = Application.persistentDataPath + "/";

    public static Promise Save<T>(T t)
    {
        var thread = new System.Threading.Thread(new ThreadStart(() =>
        {
            File.WriteAllText(getPath<T>(), JsonUtility.ToJson(t));
            File.WriteAllText(getBackupPath<T>(), JsonUtility.ToJson(t));
        }));
        thread.Start();
        return new Promise(thread);
    }

    public static Promise Load<T>(ResultDelegate<T> resultCallback)
    {
        var thread = new System.Threading.Thread(new ThreadStart(() =>
                    {
                        try
                        {
                            var json = File.ReadAllText(getPath<T>());
                            resultCallback(JsonUtility.FromJson<T>(json));
                        }
                        catch (Exception)
                        {
                            try
                            {
                                var json = File.ReadAllText(getBackupPath<T>());
                                resultCallback(JsonUtility.FromJson<T>(json));
                            }
                            catch (Exception)
                            {
                                resultCallback(default(T));
                            }
                        }
                    }));
        thread.Start();
        return new Promise(thread);
    }

    public static void Delete<T>()
    {
        var path = getPath<T>();
        File.Delete(path);
    }

    private static string getPath<T>()
    {
        return DefaultLocation + typeof(T).FullName + ".json";
    }

    private static string getBackupPath<T>()
    {
        return DefaultLocation + typeof(T).FullName + "_backup.json";
    }
}