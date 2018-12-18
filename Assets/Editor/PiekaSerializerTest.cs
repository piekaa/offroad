using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.U2D;

[TestFixture]
public class PiekaSerializerTest
{
    [Test]
    public void TestToJson()
    {
        TestClass t = new TestClass(3.14f, true, 123, "hel\\\"lo");
        var json = PiekaSerializer.ToJson(t);
        Assert.AreEqual("{\"a\":123,\"b\":\"hel\\\\\\\"lo\",\"d\":3.14,\"state\":true}", json);
    }

    [Test]
    public void TestNestedToJson()
    {
        TestMainClass t = new TestMainClass(new TestNestedClass());
        var json = PiekaSerializer.ToJson(t);
        Assert.AreEqual("{\"nested\":{\"test\":100.01}}", json);
    }

    [Test]
    public void TestGenericCollectionToJson()
    {
        List<int> list = new List<int> { 1, 2, 3, 4, 5 };
        var json = PiekaSerializer.ToJson(list);
        Assert.AreEqual("[1,2,3,4,5]", json);
    }

    [Test]
    public void TestNestedGenericCollectionToJson()
    {
        var t = new TestNestedCollection();
        var json = PiekaSerializer.ToJson(t);
        Assert.AreEqual("{\"names\":[\"Arnold\",\"Marian\",\"Fedrynand\"]}", json);
    }

    [Test]
    public void TestDictionaryToJson()
    {
        var t = new Dictionary<string, int>() {
            {"a", 1},
            {"b", 2},
            {"c", 3},
            {"d", 4},
        };
        var json = PiekaSerializer.ToJson(t);
        Assert.AreEqual("{\"a\":1,\"b\":2,\"c\":3,\"d\":4}", json);
    }


    [Test]
    public void TestFromJson()
    {
        var json = "{\"a\":123,\"b\":\"hel\\\\\\\"lo\",\"d\":3.14,\"state\":true}";
        var t = PiekaSerializer.FromJson<TestClass>(json);
        Assert.AreEqual(new TestClass(3.14f, true, 123, "hel\\\"lo"), t);
    }

    [Test]
    public void TestFromNestedJson()
    {
        var json = "{\"nested\":{\"test\":100.01}}";
        var t = PiekaSerializer.FromJson<TestMainClass>(json);
        Assert.AreEqual(new TestMainClass(new TestNestedClass()), t);
    }

    [Test]
    public void TestFromIntListJson()
    {
        var json = "[1,2,3,4,5,6]";
        var t = PiekaSerializer.FromJson<List<int>>(json);
        Assert.AreEqual(new List<int> { 1, 2, 3, 4, 5, 6 }, t);
    }

    [Test]
    public void TestFromStringListJson()
    {
        var json = "[\"a\",\"b\",\"c\",\"d\"]";
        var t = PiekaSerializer.FromJson<List<string>>(json);
        Assert.AreEqual(new List<string> { "a", "b", "c", "d" }, t);
    }
}

public class TestClass
{
    private float _d;
    private bool state;
    public int a;
    public string b;

    public TestClass() { }

    public TestClass(float d, bool state, int a, string b)
    {
        _d = d;
        this.state = state;
        this.a = a;
        this.b = b;
    }

    public float d { get { return _d; } set { _d = value; } }
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

    public TestMainClass() { }

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
    public List<string> names = new List<string> { "Arnold", "Marian", "Fedrynand" };
}