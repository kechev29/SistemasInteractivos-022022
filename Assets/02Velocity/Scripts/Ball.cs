using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private MyVector2D displacement;
    private MyVector2D objectTransform;

    [SerializeField] private MyVector2D velocity;
    [SerializeField] private MyVector2D acceleration;
    [Range(0f, 1f)][SerializeField] private float dampening;
    [SerializeField] Camera cam;

    void Start()
    {
        objectTransform = new MyVector2D(transform.position.x, transform.position.y);
    }

    private void FixedUpdate()
    {
        Move();
    }

    void Update()
    {
        acceleration.Draw(objectTransform, Color.red);
        

        if (Input.GetKeyDown(KeyCode.Space))
        {
            acceleration = SwitchGravity(acceleration, ref velocity);
            Debug.Log("Acceleration:" + acceleration);
        }
    }

    //Metodo para mover la pelota
    public void Move()
    {
        //movimiento
        velocity = velocity + Time.fixedDeltaTime * acceleration;
        objectTransform = objectTransform + Time.fixedDeltaTime * velocity;

        //check bounds
        objectTransform.x = CheckBounds(objectTransform.x, ref velocity.x);
        objectTransform.y = CheckBounds(objectTransform.y, ref velocity.y);

        //aplicar movimiento
        transform.position = new Vector3(objectTransform.x, objectTransform.y, 0);
    }

    //Metodo para revisar si la pelota se salio de la camara. En caso tal, invertir la direccion
    private float CheckBounds(float vectorCoord, ref float velocity)
        {
            if (Mathf.Abs(vectorCoord) >= cam.orthographicSize)
            {
                velocity *= -1;
                vectorCoord = Mathf.Sign(vectorCoord) * cam.orthographicSize;
                velocity *= dampening; //ralentiza la velocidad de la bola por la friccion
            }
        return vectorCoord;
        }

    private MyVector2D SwitchGravity(MyVector2D acceleration, ref MyVector2D velocity)
    {

        if (acceleration.x != 0) 
        {
            acceleration.y = acceleration.x;
            acceleration.x = 0;
            velocity *= 0;
        } 
        else if (acceleration.y != 0)
        {
            acceleration.x = -acceleration.y;
            acceleration.y = 0;
            velocity *= 0;
        }

        return acceleration;
    }
}
