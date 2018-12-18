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
        Stack<JSONArray> arrays = new Stack<JSONArray>();


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
                var stateResult = state.HandleChar(c);
                if (stateResult != null)
                {
                    if (stateResult.Key != null)
                    {
                        keys.Push(stateResult.Key);
                    }
                    if (stateResult.Value != null && !(state is ArrayJsonState) && !(state is OpenedArrayValueJsonState))
                    {
                        if (keys.Count == 0)
                        {
                            throw new NullReferenceException("key should not be null when value is not null");
                        }
                        setValue(keys.Pop(), stateResult.Value, values.Peek(), t);
                    }
                    if (stateResult.NextState != null)
                    {
                        if (stateResult.NextState is ObjectJsonState)
                        {
                            if (keys.Count == 0)
                            {
                                values.Push(obj);
                            }
                            else
                            {
                                var newObject = createValueObjectAndSet(values.Peek().GetType(), values.Peek(), keys.Peek());
                                values.Push(newObject);
                            }
                        }
                        else if (stateResult.NextState is ArrayJsonState)
                        {
                            if (!(state is ArrayJsonState) && !(state is OpenedArrayValueJsonState) && !(state is ClosedArrayValueJsonState))
                            {
                                if (arrays.Count == 0)
                                {
                                    arrays.Push(new JSONArray(obj));
                                }
                                else
                                {
                                    var newArray = createValueObjectAndSet(values.Peek().GetType(), values.Peek(), keys.Peek());
                                    arrays.Push(new JSONArray(newArray));
                                }
                            }
                            else if (stateResult.Value != null)
                            {
                                arrays.Peek().Add(getValueByType(stateResult.Value, arrays.Peek().collectionItemType));
                            }
                        }
                        else if (stateResult.NextState is ClosedArrayValueJsonState)
                        {
                            arrays.Peek().Add(getValueByType(stateResult.Value, arrays.Peek().collectionItemType));
                        }
                        else if (stateResult.NextState is EndObjectOrArrayState)
                        {

                            if (state is ArrayJsonState)
                            {
                                arrays.Peek().Add(getValueByType(stateResult.Value, arrays.Peek().collectionItemType));
                            }
                            else if (!(state is ClosedArrayValueJsonState))
                            {
                                values.Pop();
                            }
                        }
                        state = stateResult.NextState;
                    }
                }
            }
            catch (InvalidOperationException e)
            {
                throw new InvalidOperationException(json.Substring(0, i) + "   (" + json[i] + ")  , state = " + state.GetType().Name, e);
            }
        }

        if (!(state is EndObjectOrArrayState))
        {
            throw new InvalidOperationException("Incorrect state: " + state.GetType().Name);
        }
        return obj;
    }

    private static void setValue(string key, string value, object obj, Type t)
    {
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