using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.U2D;

[TestFixture]
public class CalculatorTest
{
    [Test]
    public void TestCalculateVectorAngle()
    {
        Assert.AreEqual(0, CalculateUtils.Vector2ToAngle(new Vector2(1, 0)), 1);
        Assert.AreEqual(45, CalculateUtils.Vector2ToAngle(new Vector2(1, 1)), 1);
        Assert.AreEqual(90, CalculateUtils.Vector2ToAngle(new Vector2(0, 1)), 1);
        Assert.AreEqual(135, CalculateUtils.Vector2ToAngle(new Vector2(-1, 1)), 1);
        Assert.AreEqual(180, CalculateUtils.Vector2ToAngle(new Vector2(-1, 0)), 1);
        Assert.AreEqual(225, CalculateUtils.Vector2ToAngle(new Vector2(-1, -1)), 1);
        Assert.AreEqual(270, CalculateUtils.Vector2ToAngle(new Vector2(0, -1)), 1);
        Assert.AreEqual(315, CalculateUtils.Vector2ToAngle(new Vector2(1, -1)), 1);
    }
}