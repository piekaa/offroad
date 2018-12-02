using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.U2D;

[TestFixture]
public class SpriteShapeCalculatorTest
{

    [UnityTest]
    public IEnumerator Test0DegShape()
    {
        SpriteShapeController spriteShapeController = newSpriteShapeController(3);
        spriteShapeController.spline.SetPosition(0, new Vector3(0, 0));
        spriteShapeController.spline.SetPosition(1, new Vector3(1, 0));
        spriteShapeController.spline.SetPosition(2, new Vector3(2, 0));

        yield return null;

        Assert.AreEqual(0, SpriteShapeCalculator.Angle(spriteShapeController, 0));
        Assert.AreEqual(0, SpriteShapeCalculator.Angle(spriteShapeController, 1));
        Assert.AreEqual(0, SpriteShapeCalculator.Angle(spriteShapeController, 2));
    }

    [UnityTest]
    public IEnumerator Test45DegShape()
    {
        SpriteShapeController spriteShapeController = newSpriteShapeController(3);
        spriteShapeController.spline.SetPosition(0, new Vector3(0, 0));
        spriteShapeController.spline.SetPosition(1, new Vector3(1, 0));
        spriteShapeController.spline.SetPosition(2, new Vector3(1, 1));

        yield return null;

        Assert.AreEqual(0, SpriteShapeCalculator.Angle(spriteShapeController, 0));
        Assert.AreEqual(45, SpriteShapeCalculator.Angle(spriteShapeController, 1));
        Assert.AreEqual(90, SpriteShapeCalculator.Angle(spriteShapeController, 2));
    }

    [UnityTest]
    public IEnumerator Test90DegShape()
    {
        SpriteShapeController spriteShapeController = newSpriteShapeController(3);
        spriteShapeController.spline.SetPosition(0, new Vector3(0, 0));
        spriteShapeController.spline.SetPosition(1, new Vector3(0, 1));
        spriteShapeController.spline.SetPosition(2, new Vector3(0, 2));

        yield return null;

        Assert.AreEqual(90, SpriteShapeCalculator.Angle(spriteShapeController, 0));
        Assert.AreEqual(90, SpriteShapeCalculator.Angle(spriteShapeController, 1));
        Assert.AreEqual(90, SpriteShapeCalculator.Angle(spriteShapeController, 2));
    }

    [UnityTest]
    public IEnumerator Test135DegShape()
    {
        SpriteShapeController spriteShapeController = newSpriteShapeController(3);
        spriteShapeController.spline.SetPosition(0, new Vector3(0, 0));
        spriteShapeController.spline.SetPosition(1, new Vector3(0, 1));
        spriteShapeController.spline.SetPosition(2, new Vector3(-1, 1));

        yield return null;

        Assert.AreEqual(90, SpriteShapeCalculator.Angle(spriteShapeController, 0));
        Assert.AreEqual(135, SpriteShapeCalculator.Angle(spriteShapeController, 1));
        Assert.AreEqual(180, SpriteShapeCalculator.Angle(spriteShapeController, 2));
    }

    [UnityTest]
    public IEnumerator Test180DegShape()
    {
        SpriteShapeController spriteShapeController = newSpriteShapeController(3);
        spriteShapeController.spline.SetPosition(0, new Vector3(0, 0));
        spriteShapeController.spline.SetPosition(1, new Vector3(-1, 0));
        spriteShapeController.spline.SetPosition(2, new Vector3(-2, 0));

        yield return null;

        Assert.AreEqual(180, SpriteShapeCalculator.Angle(spriteShapeController, 0));
        Assert.AreEqual(180, SpriteShapeCalculator.Angle(spriteShapeController, 1));
        Assert.AreEqual(180, SpriteShapeCalculator.Angle(spriteShapeController, 2));
    }

    [UnityTest]
    public IEnumerator Test225DegShape()
    {
        SpriteShapeController spriteShapeController = newSpriteShapeController(3);
        spriteShapeController.spline.SetPosition(0, new Vector3(0, 0));
        spriteShapeController.spline.SetPosition(1, new Vector3(-1, 0));
        spriteShapeController.spline.SetPosition(2, new Vector3(-1, -1));

        yield return null;

        Assert.AreEqual(180, SpriteShapeCalculator.Angle(spriteShapeController, 0));
        Assert.AreEqual(225, SpriteShapeCalculator.Angle(spriteShapeController, 1));
        Assert.AreEqual(270, SpriteShapeCalculator.Angle(spriteShapeController, 2));
    }

    [UnityTest]
    public IEnumerator Test270DegShape()
    {
        SpriteShapeController spriteShapeController = newSpriteShapeController(3);
        spriteShapeController.spline.SetPosition(0, new Vector3(0, 0));
        spriteShapeController.spline.SetPosition(1, new Vector3(0, -1));
        spriteShapeController.spline.SetPosition(2, new Vector3(0, -2));

        yield return null;

        Assert.AreEqual(270, SpriteShapeCalculator.Angle(spriteShapeController, 0));
        Assert.AreEqual(270, SpriteShapeCalculator.Angle(spriteShapeController, 1));
        Assert.AreEqual(270, SpriteShapeCalculator.Angle(spriteShapeController, 2));
    }

    [UnityTest]
    public IEnumerator Test315DegShape()
    {
        SpriteShapeController spriteShapeController = newSpriteShapeController(3);
        spriteShapeController.spline.SetPosition(0, new Vector3(0, 0));
        spriteShapeController.spline.SetPosition(1, new Vector3(0, -1));
        spriteShapeController.spline.SetPosition(2, new Vector3(1, -1));

        yield return null;

        Assert.AreEqual(270, SpriteShapeCalculator.Angle(spriteShapeController, 0));
        Assert.AreEqual(315, SpriteShapeCalculator.Angle(spriteShapeController, 1));
        Assert.AreEqual(0, SpriteShapeCalculator.Angle(spriteShapeController, 2));
    }

    [UnityTest]
    public IEnumerator TestPassing0Shape()
    {
        SpriteShapeController spriteShapeController = newSpriteShapeController(3);
        spriteShapeController.spline.SetPosition(0, new Vector3(-1, 0));
        spriteShapeController.spline.SetPosition(1, new Vector3(0, -1));
        spriteShapeController.spline.SetPosition(2, new Vector3(1, 0));

        yield return null;

        Assert.AreEqual(315, SpriteShapeCalculator.Angle(spriteShapeController, 0));
        Assert.AreEqual(0, SpriteShapeCalculator.Angle(spriteShapeController, 1));
        Assert.AreEqual(45, SpriteShapeCalculator.Angle(spriteShapeController, 2));
    }


    private SpriteShapeController newSpriteShapeController(int size)
    {
        SpriteShapeController spriteShapeController = new GameObject().AddComponent<SpriteShapeController>();
        while (spriteShapeController.spline.GetPointCount() > 3)
        {
            spriteShapeController.spline.RemovePointAt(0);
        }
        for (int i = 0; i < 3; i++)
        {
            spriteShapeController.spline.SetPosition(i, new Vector3(-999999 + i, -999999 - i));
        }
        return spriteShapeController;
    }

    [UnityTest]
    public IEnumerator Test0FindStartingPoint()
    {
        SpriteShapeController spriteShapeController = newSpriteShapeController(3);
        spriteShapeController.spline.SetPosition(0, new Vector3(0, 0));
        spriteShapeController.spline.SetPosition(1, new Vector3(1, 0));
        spriteShapeController.spline.SetPosition(2, new Vector3(2, 0));

        yield return null;

        Assert.AreEqual(0, SpriteShapeCalculator.FindStartingPointIndex(spriteShapeController, 1, new Vector2(0.75f, 0)));
        Assert.AreEqual(1, SpriteShapeCalculator.FindStartingPointIndex(spriteShapeController, 1, new Vector2(1.25f, 0)));
    }

    [UnityTest]
    public IEnumerator Test1FindStartingPoint()
    {
        SpriteShapeController spriteShapeController = newSpriteShapeController(3);
        spriteShapeController.spline.SetPosition(0, new Vector3(0, 0));
        spriteShapeController.spline.SetPosition(1, new Vector3(1, 0));
        spriteShapeController.spline.SetPosition(2, new Vector3(2, 1));

        yield return null;

        Assert.AreEqual(0, SpriteShapeCalculator.FindStartingPointIndex(spriteShapeController, 1, new Vector2(0.75f, 0)));
        Assert.AreEqual(1, SpriteShapeCalculator.FindStartingPointIndex(spriteShapeController, 1, new Vector2(1.5f, 0.5f)));
    }

    [UnityTest]
    public IEnumerator Test2FindStartingPoint()
    {
        SpriteShapeController spriteShapeController = newSpriteShapeController(3);
        spriteShapeController.spline.SetPosition(0, new Vector3(0, 0));
        spriteShapeController.spline.SetPosition(1, new Vector3(1, 1));
        spriteShapeController.spline.SetPosition(2, new Vector3(1, 2));

        yield return null;

        Assert.AreEqual(0, SpriteShapeCalculator.FindStartingPointIndex(spriteShapeController, 1, new Vector2(0.5f, 0.5f)));
        Assert.AreEqual(1, SpriteShapeCalculator.FindStartingPointIndex(spriteShapeController, 1, new Vector2(1f, 1.5f)));
    }

    [UnityTest]
    public IEnumerator Test3FindStartingPoint()
    {
        SpriteShapeController spriteShapeController = newSpriteShapeController(3);
        spriteShapeController.spline.SetPosition(0, new Vector3(0, 0));
        spriteShapeController.spline.SetPosition(1, new Vector3(0, 1));
        spriteShapeController.spline.SetPosition(2, new Vector3(-1, 1));

        yield return null;

        Assert.AreEqual(0, SpriteShapeCalculator.FindStartingPointIndex(spriteShapeController, 1, new Vector2(0, 0.5f)));
        Assert.AreEqual(1, SpriteShapeCalculator.FindStartingPointIndex(spriteShapeController, 1, new Vector2(-0.5f, 1.5f)));
    }

    [UnityTest]
    public IEnumerator Test4FindStartingPoint()
    {
        SpriteShapeController spriteShapeController = newSpriteShapeController(3);
        spriteShapeController.spline.SetPosition(0, new Vector3(0, 0));
        spriteShapeController.spline.SetPosition(1, new Vector3(-1, 1));
        spriteShapeController.spline.SetPosition(2, new Vector3(-2, 1));

        yield return null;

        Assert.AreEqual(0, SpriteShapeCalculator.FindStartingPointIndex(spriteShapeController, 1, new Vector2(-0.5f, 0.5f)));
        Assert.AreEqual(1, SpriteShapeCalculator.FindStartingPointIndex(spriteShapeController, 1, new Vector2(-1.5f, 1f)));
    }

    [UnityTest]
    public IEnumerator Test5FindStartingPoint()
    {
        SpriteShapeController spriteShapeController = newSpriteShapeController(3);
        spriteShapeController.spline.SetPosition(0, new Vector3(0, 0));
        spriteShapeController.spline.SetPosition(1, new Vector3(-1, 0));
        spriteShapeController.spline.SetPosition(2, new Vector3(-2, -1));

        yield return null;

        Assert.AreEqual(0, SpriteShapeCalculator.FindStartingPointIndex(spriteShapeController, 1, new Vector2(-0.5f, 0f)));
        Assert.AreEqual(1, SpriteShapeCalculator.FindStartingPointIndex(spriteShapeController, 1, new Vector2(-1.5f, -0.5f)));
    }

    [UnityTest]
    public IEnumerator Test6FindStartingPoint()
    {
        SpriteShapeController spriteShapeController = newSpriteShapeController(3);
        spriteShapeController.spline.SetPosition(0, new Vector3(0, 0));
        spriteShapeController.spline.SetPosition(1, new Vector3(-1, -1));
        spriteShapeController.spline.SetPosition(2, new Vector3(-1, -2));

        yield return null;

        Assert.AreEqual(0, SpriteShapeCalculator.FindStartingPointIndex(spriteShapeController, 1, new Vector2(-0.5f, -0.5f)));
        Assert.AreEqual(1, SpriteShapeCalculator.FindStartingPointIndex(spriteShapeController, 1, new Vector2(-1f, -1.5f)));
    }

    [UnityTest]
    public IEnumerator Test7FindStartingPoint()
    {
        SpriteShapeController spriteShapeController = newSpriteShapeController(3);
        spriteShapeController.spline.SetPosition(0, new Vector3(0, 0));
        spriteShapeController.spline.SetPosition(1, new Vector3(0, -1));
        spriteShapeController.spline.SetPosition(2, new Vector3(1, -2));

        yield return null;

        Assert.AreEqual(0, SpriteShapeCalculator.FindStartingPointIndex(spriteShapeController, 1, new Vector2(0f, -0.5f)));
        Assert.AreEqual(1, SpriteShapeCalculator.FindStartingPointIndex(spriteShapeController, 1, new Vector2(0.5f, -1.5f)));
    }

    [UnityTest]
    public IEnumerator Test8FindStartingPoint()
    {
        SpriteShapeController spriteShapeController = newSpriteShapeController(3);
        spriteShapeController.spline.SetPosition(0, new Vector3(0, 0));
        spriteShapeController.spline.SetPosition(1, new Vector3(1, -1));
        spriteShapeController.spline.SetPosition(2, new Vector3(2, -1));

        yield return null;

        Assert.AreEqual(0, SpriteShapeCalculator.FindStartingPointIndex(spriteShapeController, 1, new Vector2(0.5f, -0.5f)));
        Assert.AreEqual(1, SpriteShapeCalculator.FindStartingPointIndex(spriteShapeController, 1, new Vector2(1.5f, -1f)));
    }
}