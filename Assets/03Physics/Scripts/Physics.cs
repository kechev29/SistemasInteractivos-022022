using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Physics : MonoBehaviour
{

    private MyVector2D objectTransform;
    private MyVector2D blackHoleTransform;

    [SerializeField] private MyVector2D velocity;
    private MyVector2D acceleration;

    [SerializeField] Transform blackHole; 

    private void FixedUpdate()
    {
        Move();
        objectTransform = new MyVector2D(transform.position.x, transform.position.y);
        blackHoleTransform = new MyVector2D(blackHole.position.x, blackHole.position.y);
        acceleration = blackHoleTransform - objectTransform;
    }

    void Update()
    {
        acceleration.Draw(objectTransform, Color.green);
    }

    //Metodo para mover la pelota
    public void Move()
    {
        //movimiento
        velocity = velocity + Time.fixedDeltaTime * acceleration;
        objectTransform = objectTransform + Time.fixedDeltaTime * velocity;

        //aplicar movimiento
        transform.position = new Vector3(objectTransform.x, objectTransform.y, 0);
    }

}
