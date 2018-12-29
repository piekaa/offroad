using System;
using System.Text;

public enum StateResultAction
{
    NEW_OBJECT,
    NEW_ARRAY,
    END_OBJECT,
    END_ARRAY,
    NEW_OBJECT_IN_ARRAY,
    NONE
}

public enum WhereAmI
{
    OBJECT,
    ARRAY,
    NOWHERE
}

public class StateResult
{
    public IJsonState NextState;
    public string Key;
    public string Value;
    public string ArrayValue;
    public StateResultAction stateResultAction = StateResultAction.NONE;

    public StateResult(IJsonState nextState)
    {
        NextState = nextState;
    }

    public StateResult(IJsonState nextState, string key, string value, string arrayValue,
        StateResultAction stateResultAction) : this(nextState)
    {
        Key = key;
        Value = value;
        ArrayValue = arrayValue;
        this.stateResultAction = stateResultAction;
    }
}

public abstract class IJsonState
{
    public abstract StateResult HandleChar(char c);

    public virtual void whereAmI(WhereAmI whereAmI)
    {
    }
}

public class BeginJsonState : IJsonState
{
    public override StateResult HandleChar(char c)
    {
        if (c == '{')
        {
            return new StateResult(new ObjectJsonState(), null, null, null, StateResultAction.NEW_OBJECT);
        }
        else if (c == '[')
        {
            return new StateResult(new ArrayJsonState(), null, null, null, StateResultAction.NEW_ARRAY);
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

    public override StateResult HandleChar(char c)
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
                return new StateResult(new OpenedArrayValueJsonState(), null, null, null, StateResultAction.NONE);
            }
            else if (c == '{')
            {
                return new StateResult(new ObjectJsonState(), null, null, null, StateResultAction.NEW_OBJECT_IN_ARRAY);
            }
            else if (c == '[')
            {
                return new StateResult(new ArrayJsonState(), null, null, null, StateResultAction.NEW_ARRAY);
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
                return new StateResult(new ArrayJsonState(), null, null, sb.ToString(), StateResultAction.NONE);
            }
            else if (c == ']')
            {
                return new StateResult(new EndObjectOrArrayState(), null, null, sb.ToString(),
                    StateResultAction.END_ARRAY);
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
    public override StateResult HandleChar(char c)
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

    public override StateResult HandleChar(char c)
    {
        if (c == '\"')
        {
            return new StateResult(new ClosedKeyJsonState(), sb.ToString(), null, null, StateResultAction.NONE);
        }

        sb.Append(c);
        return null;
    }
}

public class ClosedKeyJsonState : IJsonState
{
    public override StateResult HandleChar(char c)
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

    public override StateResult HandleChar(char c)
    {
        if (first)
        {
            first = false;
            if (c >= '0' && c <= '9' || c == '-' || c == 't' || c == 'f')
            {
                sb.Append(c);
                return null;
            }

            if (c == '\"')
            {
                return new StateResult(new OpenedValueJsonState());
            }

            if (c == '{')
            {
                return new StateResult(new ObjectJsonState(), null, null, null, StateResultAction.NEW_OBJECT);
            }

            if (c == '[')
            {
                return new StateResult(new ArrayJsonState(), null, null, null, StateResultAction.NEW_ARRAY);
            }

            {
                throw new InvalidOperationException("expected numeric value or \"");
            }
        }


        if (c >= '0' && c <= '9' || c == '.' || t.Contains(c.ToString()) || f.Contains(c.ToString()))
        {
            sb.Append(c);
            if (!t.StartsWith(sb.ToString()) && f.StartsWith(sb.ToString()))
            {
                throw new InvalidOperationException("Expected true or false, but was: " + sb.ToString());
            }

            return null;
        }

        if (c == ',')
        {
            return new StateResult(new ObjectJsonState(), null, sb.ToString(), null, StateResultAction.NONE);
        }

        if (c == '}')
        {
            return new StateResult(new EndObjectOrArrayState(), null, sb.ToString(), null,
                StateResultAction.END_OBJECT);
        }
        throw new InvalidOperationException("expected numeric value or , or }");
    }
}

public class OpenedValueJsonState : IJsonState
{
    private StringBuilder sb = new StringBuilder();
    private bool escape = false;

    public override StateResult HandleChar(char c)
    {
        if (c == '\"' && !escape)
        {
            return new StateResult(new ClosedValueJsonState(), null, sb.ToString(), null, StateResultAction.NONE);
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

    public override StateResult HandleChar(char c)
    {
        if (c == '\"' && !escape)
        {
            return new StateResult(new ClosedArrayValueJsonState(), null, null, sb.ToString(), StateResultAction.NONE);
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
    public override StateResult HandleChar(char c)
    {
        if (c == ',')
        {
            return new StateResult(new ArrayJsonState());
        }
        else if (c == ']')
        {
            return new StateResult(new EndObjectOrArrayState(), null, null, null, StateResultAction.END_ARRAY);
        }
        else
        {
            throw new InvalidOperationException("expected , or ]");
        }
    }
}

public class ClosedValueJsonState : IJsonState
{
    public override StateResult HandleChar(char c)
    {
        if (c == ',')
        {
            return new StateResult(new ObjectJsonState());
        }
        else if (c == '}')
        {
            return new StateResult(new EndObjectOrArrayState(), null, null, null, StateResultAction.END_OBJECT);
        }
        else
        {
            throw new InvalidOperationException("expected , or }");
        }
    }
}

public class EndObjectOrArrayState : IJsonState
{
    private WhereAmI where;

    public override void whereAmI(WhereAmI whereAmI)
    {
        where = whereAmI;
    }

    public override StateResult HandleChar(char c)
    {
        if (c == ',')
        {
            if (where == WhereAmI.OBJECT)
            {
                return new StateResult(new ObjectJsonState());
            }
            else
            {
                return new StateResult(new ArrayJsonState());
            }
        }
        else if (c == '}')
        {
            return new StateResult(new EndObjectOrArrayState(), null, null, null, StateResultAction.END_OBJECT);
        }
        else if (c == ']')
        {
            return new StateResult(new EndObjectOrArrayState(), null, null, null, StateResultAction.END_ARRAY);
        }

        throw new InvalidOperationException("expected , or }, or ]");
    }
}