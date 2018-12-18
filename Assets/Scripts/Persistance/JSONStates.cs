using System;
using System.Text;

public class StateResult
{
    public IJsonState NextState;
    public string Key;
    public string Value;

    public StateResult(IJsonState nextState)
    {
        NextState = nextState;
    }

    public StateResult(IJsonState nextState, string key) : this(nextState)
    {
        Key = key;
    }

    public StateResult(string value, IJsonState nextState) : this(nextState)
    {
        Value = value;
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
            return new StateResult(new ObjectJsonState());
        }
        else if (c == '[')
        {
            return new StateResult(new ArrayJsonState());
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
                return new StateResult(new OpenedArrayValueJsonState());
            }
            else if (c == '{')
            {
                return new StateResult(new ObjectJsonState());
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
                return new StateResult(sb.ToString(), new ArrayJsonState());
            }
            else if (c == ']')
            {
                return new StateResult(sb.ToString(), new EndObjectOrArrayState());
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
            return new StateResult(new ClosedKeyJsonState(), sb.ToString());
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
                return new StateResult(new ObjectJsonState());
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
                return new StateResult(sb.ToString(), new ObjectJsonState());
            }
            else if (c == '}')
            {
                return new StateResult(sb.ToString(), new EndObjectOrArrayState());
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
            return new StateResult(sb.ToString(), new ClosedValueJsonState());
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
            return new StateResult(sb.ToString(), new ClosedArrayValueJsonState());
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
            return new StateResult(new EndObjectOrArrayState());
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
            return new StateResult(new EndObjectOrArrayState());
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
            return new StateResult(new EndObjectOrArrayState());
        }
        throw new InvalidOperationException("expected , or }");
    }
}
