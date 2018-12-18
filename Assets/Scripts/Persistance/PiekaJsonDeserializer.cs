using System.Text;
using System.Reflection;
using System;
using System.Collections.Generic;
using System.Collections;
class PiekaJsonDeserializer
{
    public static T FromJson<T>(string json)
    {
        var t = typeof(T);

        var obj = Activator.CreateInstance<T>();
        Stack<string> keys = new Stack<string>();
        Stack<object> values = new Stack<object>();
        IJsonState state = new BeginJsonState();

        for (int i = 0; i < json.Length; i++)
        {
            char c = json[i];
            if (char.IsWhiteSpace(c))
            {
                continue;
            }
            try
            {
                var result = state.HandleChar(c);

                if (result != null)
                {
                    if (result.newObject)
                    {
                        if (values.Count == 0)
                        {
                            values.Push(obj);
                        }
                        else
                        {
                            values.Push(createValueObjectAndSet(values.Peek().GetType(), values.Peek(), keys.Peek()));
                        }
                    }
                    if (result.newArray)
                    {
                        if (values.Count == 0)
                        {
                            values.Push(new JSONArray(obj));
                        }
                        else
                        {
                            values.Push(new JSONArray(createValueObjectAndSet(values.Peek().GetType(), values.Peek(), keys.Peek())));
                        }
                    }
                    if (result.Key != null)
                    {
                        keys.Push(result.Key);
                    }
                    if (result.Value != null)
                    {
                        setValue(keys.Pop(), result.Value, values.Peek());
                    }
                    if (result.ArrayValue != null)
                    {
                        var jsonArray = (JSONArray)values.Peek();
                        jsonArray.Add(getValueByType(result.ArrayValue, jsonArray.collectionItemType));
                    }
                    if (result.endArray)
                    {
                        values.Pop();
                    }
                    if (result.endObject)
                    {
                        values.Pop();
                    }
                    if (result.NextState != null)
                    {
                        state = result.NextState;
                    }
                }

            }
            //todo DeserializationException
            catch (Exception e)
            {
                throw new Exception(json.Substring(0, i) + "   (" + json[i] + ")  , state = " + state.GetType().Name + "\nstack trace: " + e.StackTrace, e);
            }
        }
        return obj;
    }

    private static void setValue(string key, string value, object obj)
    {
        Type t = obj.GetType();
        var fieldInfo = t.GetField(key);
        var propertyInfo = t.GetProperty(key);
        var methodInfo = t.GetMethod("Set" + StringUtils.ToFirstUpper(key));
        if (fieldInfo != null)
        {
            var fieldType = fieldInfo.FieldType;
            fieldInfo.SetValue(obj, getValueByType(value, fieldType));
        }
        else if (propertyInfo != null)
        {
            var propertyType = propertyInfo.PropertyType;
            if (propertyInfo.GetSetMethod() != null)
            {
                propertyInfo.SetValue(obj, getValueByType(value, propertyType), null);
            }
        }
        else if (methodInfo != null)
        {
            var parameterType = methodInfo.GetParameters()[0].ParameterType;
            methodInfo.Invoke(obj, new object[] { getValueByType(value, parameterType) });
        }
    }

    private static object getValueByType(string value, Type t)
    {
        if (t.IsAssignableFrom(typeof(int)))
        {
            return int.Parse(value);
        }
        else if (t.IsAssignableFrom(typeof(float)))
        {
            return float.Parse(value);
        }
        else if (t.IsAssignableFrom(typeof(double)))
        {
            return double.Parse(value);
        }
        else if (t.IsAssignableFrom(typeof(string)))
        {
            return value;
        }
        else if (t.IsAssignableFrom(typeof(bool)))
        {
            return bool.Parse(value);
        }
        return null;
    }

    private static object createValueObjectAndSet(Type objectType, object currentObject, string fieldName)
    {
        var currentObjType = objectType;
        var fieldInfo = currentObjType.GetField(fieldName);
        var propertyInfo = currentObjType.GetProperty(fieldName);
        var methodInfo = currentObjType.GetMethod("Set" + StringUtils.ToFirstUpper(fieldName));

        var type = fieldInfo != null ? fieldInfo.FieldType : null;
        type = propertyInfo != null ? propertyInfo.PropertyType : type;
        type = methodInfo != null ? methodInfo.GetParameters()[0].ParameterType : type;
        var newObject = Activator.CreateInstance(type);

        if (fieldInfo != null)
        {
            var fieldType = fieldInfo.FieldType;
            fieldInfo.SetValue(currentObject, newObject);
        }
        else if (propertyInfo != null)
        {
            var propertyType = propertyInfo.PropertyType;
            if (propertyInfo.GetSetMethod() != null)
            {
                propertyInfo.SetValue(currentObject, newObject, null);
            }
        }
        else if (methodInfo != null)
        {
            var parameterType = methodInfo.GetParameters()[0].ParameterType;
            methodInfo.Invoke(currentObject, new object[] { newObject });
        }
        return newObject;
    }
}


class JSONArray
{
    private IList list;

    public Type collectionItemType;

    public JSONArray(object collection)
    {
        if (typeof(IList).IsAssignableFrom(collection.GetType()))
        {
            var collectionType = collection.GetType();
            if (!collectionType.IsGenericType)
            {
                throw new InvalidCastException(collection.GetType().Name + " is not generic");
            }
            collectionItemType = collectionType.GetGenericArguments()[0];
            list = collection as IList;
        }
        else
        {
            throw new InvalidCastException(collection.GetType().Name + " is not assingable from IList");
        }
    }

    public void Add(object value)
    {
        list.Add(value);
    }
}