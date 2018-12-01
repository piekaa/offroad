using System;
using UnityEngine;

public class PMEventArgs
{
    public int Number;
    public string Text;
    public float Floating;
    public Vector3 Vector3;
    public Vector2 Vector2;
    public GameObject GameObject;
    public object Custom;
    public object Sender;
    public ScriptablePieka Pieka;
    public char Character;

    public PMEventArgs()
    {
    }


    public PMEventArgs(string text)
    {
        this.Text = text;
    }


    public PMEventArgs(int number)
    {
        this.Number = number;
    }

    public PMEventArgs(float floating)
    {
        this.Floating = floating;
    }

    public PMEventArgs(Vector3 vector3)
    {
        this.Vector3 = vector3;
    }

    public PMEventArgs(Vector2 vector2)
    {
        this.Vector2 = vector2;
    }

    public PMEventArgs(GameObject gameObject)
    {
        this.GameObject = gameObject;
    }

    public PMEventArgs(ScriptablePieka pieka)
    {
        this.Pieka = pieka;
    }

    public PMEventArgs(object custom)
    {
        Custom = custom;
    }
}

