using System;
using System.Text;

public class StateResult
{
    public IJsonState NextState;
    public string Key;
    public string Value;
    public string ArrayValue;
    public bool newObject;
    public bool newArray;
    public bool endObject;
    public bool endArray;

    public StateResult(IJsonState nextState)
    {
        NextState = nextState;
    }

    public StateResult(IJsonState nextState, string key, string value, string arrayValue, bool newObject, bool newArray, bool endObject, bool endArray)
    {
        NextState = nextState;
        Key = key;
        Value = value;
        ArrayValue = arrayValue;
        this.newObject = newObject;
        this.newArray = newArray;
        this.endObject = endObject;
        this.endArray = endArray;
    }
}

public interface IJsonState
{
    StateResult HandleChar(char c);
}

public class BeginJsonState : IJsonState
{
    public StateResult HandleChar(char c)
    {
        if (c == '{')
        {
            return new StateResult(new ObjectJsonState(), null, null, null, true, false, false, false);
        }
        else if (c == '[')
        {
            return new StateResult(new ArrayJsonState(), null, null, null, false, true, false, false);
        }
        throw new InvalidOperationException("expected { at begining");
    }
}

public class ArrayJsonState : IJsonState
{
    private bool first = true;
    private string t = "true";
    private string f = "false";
    private StringBuilder sb = new StringBuilder();

    public StateResult HandleChar(char c)
    {
        if (first)
        {
            first = false;
            if (c >= '0' && c <= '9' || c == 't' || c == 'f')
            {
                sb.Append(c);
                return null;
            }
            else if (c == '\"')
            {
                return new StateResult(new OpenedArrayValueJsonState(), null, null, null, false, false, false, false);
            }
            else if (c == '{')
            {
                return new StateResult(new ObjectJsonState(), null, null, null, false, false, true, false);
            }
            else if (c == '[')
            {
                return new StateResult(new ArrayJsonState(), null, null, null, false, true, false, false);
            }
            else
            {
                throw new InvalidOperationException("expected numeric value or \"");
            }
        }
        else
        {
            if ((c >= '0' && c <= '9') || c == '.' || t.Contains(c.ToString()) || f.Contains(c.ToString()))
            {
                sb.Append(c);
                if (!t.StartsWith(sb.ToString()) && f.StartsWith(sb.ToString()))
                {
                    throw new InvalidOperationException("Expected true or false, but was: " + sb.ToString());
                }

                return null;
            }
            else if (c == ',')
            {
                return new StateResult(new ArrayJsonState(), null, null, sb.ToString(), false, false, false, false);
            }
            else if (c == ']')
            {
                return new StateResult(new EndObjectOrArrayState(), null, null, sb.ToString(), false, false, false, true);
            }
            else
            {
                throw new InvalidOperationException("expected numeric value or , or }");
            }
        }
    }
}

public class ObjectJsonState : IJsonState
{
    public StateResult HandleChar(char c)
    {
        if (c != '\"')
        {
            throw new InvalidOperationException("expected \"");
        }
        return new StateResult(new OpenedKeyJsonState());
    }
}

public class OpenedKeyJsonState : IJsonState
{
    private StringBuilder sb = new StringBuilder();
    public StateResult HandleChar(char c)
    {
        if (c == '\"')
        {
            return new StateResult(new ClosedKeyJsonState(), sb.ToString(), null, null, false, false, false, false);
        }
        sb.Append(c);
        return null;
    }
}

public class ClosedKeyJsonState : IJsonState
{
    public StateResult HandleChar(char c)
    {
        if (c != ':')
        {
            throw new InvalidOperationException("expected :");
        }
        return new StateResult(new ValueJsonState());
    }
}

public class ValueJsonState : IJsonState
{
    private bool first = true;
    private string t = "true";
    private string f = "false";
    private StringBuilder sb = new StringBuilder();

    public StateResult HandleChar(char c)
    {
        if (first)
        {
            first = false;
            if (c >= '0' && c <= '9' || c == 't' || c == 'f')
            {
                sb.Append(c);
                return null;
            }
            else if (c == '\"')
            {
                return new StateResult(new OpenedValueJsonState());
            }
            else if (c == '{')
            {
                return new StateResult(new ObjectJsonState(), null, null, null, true, false, false, false);
            }
            else
            {
                throw new InvalidOperationException("expected numeric value or \"");
            }
        }
        else
        {
            if ((c >= '0' && c <= '9') || c == '.' || t.Contains(c.ToString()) || f.Contains(c.ToString()))
            {
                sb.Append(c);
                if (!t.StartsWith(sb.ToString()) && f.StartsWith(sb.ToString()))
                {
                    throw new InvalidOperationException("Expected true or false, but was: " + sb.ToString());
                }

                return null;
            }
            else if (c == ',')
            {
                return new StateResult(new ObjectJsonState(), null, sb.ToString(), null, false, false, false, false);
            }
            else if (c == '}')
            {
                return new StateResult(new EndObjectOrArrayState(), null, sb.ToString(), null, false, false, true, false);
            }
            else
            {
                throw new InvalidOperationException("expected numeric value or , or }");
            }
        }
    }
}

public class OpenedValueJsonState : IJsonState
{
    private StringBuilder sb = new StringBuilder();
    private bool escape = false;
    public StateResult HandleChar(char c)
    {
        if (c == '\"' && !escape)
        {
            return new StateResult(new ClosedValueJsonState(), null, sb.ToString(), null, false, false, false, false);
        }

        if (c == '\\' && !escape)
        {
            escape = true;
        }
        else
        {
            escape = false;
            sb.Append(c);
        }
        return null;
    }
}

public class OpenedArrayValueJsonState : IJsonState
{
    private StringBuilder sb = new StringBuilder();
    private bool escape = false;
    public StateResult HandleChar(char c)
    {
        if (c == '\"' && !escape)
        {
            return new StateResult(new ClosedArrayValueJsonState(), null, null, sb.ToString(), false, false, false, false);
        }

        if (c == '\\' && !escape)
        {
            escape = true;
        }
        else
        {
            escape = false;
            sb.Append(c);
        }
        return null;
    }
}

public class ClosedArrayValueJsonState : IJsonState
{
    public StateResult HandleChar(char c)
    {
        if (c == ',')
        {
            return new StateResult(new ArrayJsonState());
        }
        else if (c == ']')
        {
            return new StateResult(new EndObjectOrArrayState(), null, null, null, false, false, false, true);
        }
        else
        {
            throw new InvalidOperationException("expected , or ]");
        }
    }
}

public class ClosedValueJsonState : IJsonState
{
    public StateResult HandleChar(char c)
    {
        if (c == ',')
        {
            return new StateResult(new ObjectJsonState());
        }
        else if (c == '}')
        {
            return new StateResult(new EndObjectOrArrayState(), null, null, null, false, false, true, false);
        }
        else
        {
            throw new InvalidOperationException("expected , or }");
        }
    }
}

public class EndObjectOrArrayState : IJsonState
{
    public StateResult HandleChar(char c)
    {
        if (c == ',')
        {
            return new StateResult(new ObjectJsonState());
        }
        else if (c == '}')
        {
            return new StateResult(new EndObjectOrArrayState(), null, null, null, false, false, true, false);
        }
        throw new InvalidOperationException("expected , or }");
    }
}
