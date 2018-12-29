using System;
using System.Collections.Generic;
using NUnit.Framework;
using TestNamespace;
using UnityEngine;

[TestFixture]
public class PieksonTest
{
    [Test]
    public void TestToJson()
    {
        TestClass t = new TestClass(3.14f, true, 123, "hel\\\"lo");
        var json = Piekson.ToJson(t);
        Assert.AreEqual("{\"a\":123,\"b\":\"hel\\\\\\\"lo\",\"d\":3.14,\"state\":true}", json);
    }

    [Test]
    public void TestNestedToJson()
    {
        TestMainClass t = new TestMainClass(new TestNestedClass());
        var json = Piekson.ToJson(t);
        Assert.AreEqual("{\"nested\":{\"test\":100.01}}", json);
    }

    [Test]
    public void TestGenericCollectionToJson()
    {
        List<int> list = new List<int> {1, 2, 3, 4, 5};
        var json = Piekson.ToJson(list);
        Assert.AreEqual("[1,2,3,4,5]", json);
    }

    [Test]
    public void TestNestedGenericCollectionToJson()
    {
        var t = new TestNestedCollection(new List<string> {"Arnold", "Marian", "Fedrynand"});
        var json = Piekson.ToJson(t);
        Assert.AreEqual("{\"names\":[\"Arnold\",\"Marian\",\"Fedrynand\"]}", json);
    }

    [Test]
    public void TestDictionaryToJson()
    {
        var t = new Dictionary<string, int>()
        {
            {"a", 1},
            {"b", 2},
            {"c", 3},
            {"d", 4},
        };
        var json = Piekson.ToJson(t);
        Assert.AreEqual("{\"a\":1,\"b\":2,\"c\":3,\"d\":4}", json);
    }

    [Test]
    public void TestLoopToJson()
    {
        var t = new TestLoop(10);
        var json = Piekson.ToJson(t);
        Assert.AreEqual("{\"a\":10}", json);
    }

    [Test]
    public void TestNullValue()
    {
        var t = new TestMainClass();
        var json = Piekson.ToJson(t);
        Assert.AreEqual("{}", json);
    }

    [Test]
    public void TestEnum()
    {
        var t = new TestClassWithEnum(TestDirection.DOWN);
        var json = Piekson.ToJson(t);
        Assert.AreEqual("{\"TestDirection\":\"DOWN\"}", json);
    }

    [Test]
    public void TestType()
    {
        var t = new TestClassWithType(typeof(TestClassWithType));
        var json = Piekson.ToJson(t);
        Assert.AreEqual("{\"type\":\"TestNamespace.TestClassWithType, Assembly-CSharp-Editor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\"}", json);
    }

    [Test]
    public void TestFromJson()
    {
        var json = "{\"a\":123,\"b\":\"hel\\\\\\\"lo\",\"d\":3.14,\"state\":true}";
        var t = Piekson.FromJson<TestClass>(json);
        Assert.AreEqual(new TestClass(3.14f, true, 123, "hel\\\"lo"), t);
    }

    [Test]
    public void TestNegativeValueFromJson()
    {
        var json = "{\"a\":-123,\"b\":\"hel\\\\\\\"lo\",\"d\":-3.14,\"state\":true}";
        var t = Piekson.FromJson<TestClass>(json);
        Assert.AreEqual(new TestClass(-3.14f, true, -123, "hel\\\"lo"), t);
    }

    [Test]
    public void TestFromNestedJson()
    {
        var json = "{\"nested\":{\"test\":100.01}}";
        var t = Piekson.FromJson<TestMainClass>(json);
        Assert.AreEqual(new TestMainClass(new TestNestedClass()), t);
    }

    [Test]
    public void TestFromIntListJson()
    {
        var json = "[1,2,3,4,5,6]";
        var t = Piekson.FromJson<List<int>>(json);
        Assert.AreEqual(new List<int> {1, 2, 3, 4, 5, 6}, t);
    }

    [Test]
    public void TestFromStringListJson()
    {
        var json = "[\"a\",\"b\",\"c\",\"d\"]";
        var t = Piekson.FromJson<List<string>>(json);
        Assert.AreEqual(new List<string> {"a", "b", "c", "d"}, t);
    }

    [Test]
    public void TestFromObjectInListJson()
    {
        var json = "[{\"test\":1},{\"test\":2}]";
        var t = Piekson.FromJson<List<TestSimpleClass>>(json);
        Assert.AreEqual(new List<TestSimpleClass> {new TestSimpleClass(1), new TestSimpleClass(2)}, t);
    }

    [Test]
    public void TestFromListInObjectJson()
    {
        var json = "{\"names\":[\"Arnold\",\"Marian\",\"Fedrynand\"]}";
        var t = Piekson.FromJson<TestNestedCollection>(json);
        Assert.AreEqual(new TestNestedCollection(new List<string> {"Arnold", "Marian", "Fedrynand"}), t);
    }

    [Test]
    public void TestFromDictionaryJson()
    {
        var json = "{\"a\":1,\"b\":2,\"c\":3,\"d\":4}";
        var t = Piekson.FromJson<Dictionary<string, int>>(json);
        CollectionAssert.AreEquivalent(new Dictionary<string, int>() {{"a", 1}, {"b", 2}, {"c", 3}, {"d", 4},}, t);
    }

    [Test]
    public void TestFromDictionaryInObjectJson()
    {
        var json = "{\"test\":{\"a\":1, \"b\":3}}";
        var t = Piekson.FromJson<TestDictionaryInObject>(json);
        CollectionAssert.AreEquivalent(new Dictionary<string, int>() {{"a", 1}, {"b", 3}}, t.test);
    }

    [Test]
    public void TestFromObjectInDictionaryJson()
    {
        var json = "{\"testObj1\":{\"test\":1}, \"testObj2\":{\"test\":2}}";
        var t = Piekson.FromJson<Dictionary<string, TestSimpleClass>>(json);
        CollectionAssert.AreEquivalent(
            new Dictionary<string, TestSimpleClass>()
                {{"testObj1", new TestSimpleClass(1)}, {"testObj2", new TestSimpleClass(2)}}, t);
    }

    [Test]
    public void TestFromEnum()
    {
        var json = "{\"TestDirection\":\"DOWN\"}";
        var t = Piekson.FromJson<TestClassWithEnum>(json);
        Assert.AreEqual(new TestClassWithEnum(TestDirection.DOWN).TestDirection, t.TestDirection);
    }

    [Test]
    public void TestFromType()
    {
        var json = "{\"type\":\"TestNamespace.TestClassWithType, Assembly-CSharp-Editor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\"}";
        var t = Piekson.FromJson<TestClassWithType>(json);
        Assert.AreEqual(typeof(TestClassWithType), t.type);
    }
}


namespace TestNamespace
{
    public class TestClassWithType
    {
        public Type type;

        public TestClassWithType()
        {
        }

        public TestClassWithType(Type type)
        {
            this.type = type;
        }
    }
}

public class TestClassWithEnum
{
    public TestDirection TestDirection;

    public TestClassWithEnum()
    {
    }

    public TestClassWithEnum(TestDirection testDirection)
    {
        TestDirection = testDirection;
    }
}

public enum TestDirection
{
    UP,
    DOWN,
    LEFT,
    RIGHT
}

public class TestLoop
{
    public int a;
    public TestLoop testLoop;

    public TestLoop(int i)
    {
        a = i;
        testLoop = this;
    }
}

public class TestDictionaryInObject
{
    public Dictionary<string, int> test;

    public TestDictionaryInObject()
    {
    }

    public TestDictionaryInObject(Dictionary<string, int> test)
    {
        this.test = test;
    }
}

public class TestSimpleClass
{
    public int test;

    public TestSimpleClass()
    {
    }

    public TestSimpleClass(int test)
    {
        this.test = test;
    }

    public override bool Equals(object obj)
    {
        var @class = obj as TestSimpleClass;
        return @class != null &&
               test == @class.test;
    }

    public override int GetHashCode()
    {
        return -935573843 + test.GetHashCode();
    }
}

public class TestClass
{
    private float _d;
    private bool state;
    public int a;
    public string b;

    public TestClass()
    {
    }

    public TestClass(float d, bool state, int a, string b)
    {
        _d = d;
        this.state = state;
        this.a = a;
        this.b = b;
    }

    public float d
    {
        get { return _d; }
        set { _d = value; }
    }

    public bool GetState()
    {
        return state;
    }

    public void SetState(bool s)
    {
        state = s;
    }

    public override bool Equals(object obj)
    {
        var @class = obj as TestClass;
        return @class != null &&
               _d == @class._d &&
               state == @class.state &&
               a == @class.a &&
               b == @class.b;
    }

    public override int GetHashCode()
    {
        var hashCode = 630885269;
        hashCode = hashCode * -1521134295 + _d.GetHashCode();
        hashCode = hashCode * -1521134295 + state.GetHashCode();
        hashCode = hashCode * -1521134295 + a.GetHashCode();
        hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(b);
        return hashCode;
    }

    public override string ToString()
    {
        return "" + a + " " + b + " " + _d + " " + state;
    }
}

public class TestMainClass
{
    public TestNestedClass nested;

    public TestMainClass()
    {
    }

    public TestMainClass(TestNestedClass nested)
    {
        this.nested = nested;
    }

    public override bool Equals(object obj)
    {
        var @class = obj as TestMainClass;
        return @class != null &&
               EqualityComparer<TestNestedClass>.Default.Equals(nested, @class.nested);
    }

    public override int GetHashCode()
    {
        return 238069722 + EqualityComparer<TestNestedClass>.Default.GetHashCode(nested);
    }
}

public class TestNestedClass
{
    public double test = 100.01;

    public override bool Equals(object obj)
    {
        var @class = obj as TestNestedClass;
        return @class != null &&
               test == @class.test;
    }

    public override int GetHashCode()
    {
        return -935573843 + test.GetHashCode();
    }
}

public class TestNestedCollection
{
    public List<string> names;

    public TestNestedCollection()
    {
    }

    public TestNestedCollection(List<string> names)
    {
        this.names = names;
    }

    public override bool Equals(object obj)
    {
        var collection = obj as TestNestedCollection;
        return new HashSet<string>(names).SetEquals(new HashSet<string>(collection.names));
    }

    public override int GetHashCode()
    {
        return 1;
    }

    public override string ToString()
    {
        string result = "[";
        foreach (var name in names)
        {
            result += ", " + name;
        }

        return result + "]";
    }
}