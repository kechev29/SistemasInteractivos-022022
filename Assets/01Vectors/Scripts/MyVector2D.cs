using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct MyVector2D
{
    public float x;
    public float y;

    public float magnitude
    {
        get 
        { 
            return Mathf.Sqrt(x * x + y * y); 
        }
    }

    public MyVector2D normalized
    {
        get
        {
            float m = magnitude;

            if(m <= 0.0001f)
            {
                return new MyVector2D(0f, 0f);
            }

            return new MyVector2D(x / m, y / m);
        }
    }

    // - CONSTRUCTORES -
    //Crea al vector con un componente 'x' y un componente 'y'
    public MyVector2D(float x, float y)
    {
        this.x = x;
        this.y = y;
    }

    // - OPERACIONES -
    //Suma del vector que llama al metodo + vector a
    public MyVector2D Sum(MyVector2D a)
    {
        MyVector2D d = new MyVector2D(
            this.x + a.x,
            this.y + a.y
            );

        return d;
    }

    //Resta del vector que llama al metodo - vector a
    public MyVector2D Sub(MyVector2D a)
    {
        MyVector2D d = new MyVector2D(
            this.x - a.x,
            this.y - a.y
            );

        return d;
    }

    //Multiplicacion del vector que llama al metodo por un escalar a
    public MyVector2D Scale(float a)
    {
        MyVector2D d = new MyVector2D(
            this.x * a,
            this.y * a
            );

        return d;
    }

    //Muta al vector para que quede normalizado
    public void Normalize()
    {
        float tolerance = 0.0001f;
        float m = magnitude;

        if(m <= tolerance)
        {
            x = 0; y = 0;

            return;
        }

        x /= m;
        y /= m;

    }

    // - DEBUG -
    //para dibujar el vector
    public void Draw(Color color)
    {
        Debug.DrawLine(
            Vector3.zero,
            new Vector3(x, y, 0),
            color);
    }

    public void Draw(MyVector2D origin, Color color)
    {
        Debug.DrawLine(
            new Vector3(origin.x, origin.y, 0),
            new Vector3(x + origin.x, y + origin.y, 0),
            color);
    }


    // - OVERRIDES -
    //override para que el log escriba el vector como (x, y)
    public override string ToString()
    {
        return $"[{x}, {y}]";
    }

    //override para sumar, asi se puede hacer vector1 + vector2
    public static MyVector2D operator +(MyVector2D a, MyVector2D b)
    {
        return new MyVector2D(
            a.x + b.x,
            a.y + b.y
            );
    }

    //override para restar, y se pueda hacer vector1 - vector 2
    public static MyVector2D operator -(MyVector2D a, MyVector2D b)
    {
        return new MyVector2D(
            a.x - b.x,
            a.y - b.y
            );
    }

    //override para multiplicar escalar por vector, en la forma 'a * vector'
    public static MyVector2D operator *(float a, MyVector2D b)
    {
        return new MyVector2D(
            a * b.x,
            a * b.y
            );
    }
    //override para multiplicar escalar por vector, en la forma 'vector * a'
    public static MyVector2D operator *(MyVector2D b, float a)
    {
        return new MyVector2D(
            b.x * a,
            b.y * a
            );
    }

    public static MyVector2D operator /(MyVector2D b, float a)
    {
        return new MyVector2D(
            b.x / a,
            b.y / a
            );
    }
}
