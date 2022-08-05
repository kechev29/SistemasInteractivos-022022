using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing : MonoBehaviour
{

    [SerializeField] MyVector2D a = new MyVector2D(2, 4);
    [SerializeField] MyVector2D b = new MyVector2D(-2, 4);
    [SerializeField] MyVector2D newOrigin = new MyVector2D();
    [Range(0, 1f)]
    [SerializeField] float connectingLength;

    MyVector2D zero = new MyVector2D(0, 0);

    MyVector2D connecting1;
    MyVector2D connecting2;

    void Update()
    {
        a.Draw(Color.green);
        b.Draw(newOrigin, Color.red);

        // - Conectar a y b
        connecting1 = (b + newOrigin) - (a + zero);
        connecting1 = connectingLength * connecting1;

        // - Conectar origen y final de connecting1
        //connecting2 = (connecting1 + a) - zero;
        connecting2 = connecting1 + a;

        connecting2.Draw(Color.blue);
        connecting1.Draw(a, Color.yellow);

    }

}
