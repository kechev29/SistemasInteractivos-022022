using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Forces : MonoBehaviour
{
    public enum ForceMode
    {
        Friction,
        Fluid,
        Gravity
    }

    [System.NonSerialized] private MyVector2D objectTransform;

    [SerializeField] private ForceMode forceMode;

    [Header("Forces")]
    [SerializeField] private MyVector2D wind;
    [SerializeField] private MyVector2D planetGravity;
    [Range(0f, 1f)][SerializeField] private float frictionCoefficient;
    [Range(0f, 1f)][SerializeField] private float dragCoefficient;

    private MyVector2D netForce;
    private MyVector2D weight;

    [Header("Gravity")]
    [SerializeField] public float mass = 1;
    [SerializeField] private Forces secondMass;

    [Header("Extra")]
    [Range(0f, 1f)][SerializeField] private float dampening;
    [SerializeField] Camera cam;

    [Header("Debug")]
    [SerializeField] private MyVector2D velocity;
    [SerializeField] private MyVector2D acceleration;

    void Start()
    {
        
    }

    private void FixedUpdate()
    {
        objectTransform = new MyVector2D(transform.position.x, transform.position.y);

        netForce = new MyVector2D(0, 0);
        weight = mass * planetGravity;
        
        if(forceMode != ForceMode.Gravity)
        {
            ApplyForce(weight);
            if (forceMode == ForceMode.Fluid)
            {
                if (transform.position.y <= -0.3)
                    ApplyForce(CalculateFluidResistance());
            }
            else
            {
                ApplyForce(CalculateFriction());
            }
            
        } else
        {
            ApplyForce(Gravity());
        }

        ApplyForce(wind);

        Move();
    }

    void Update()
    {
        velocity.Draw(objectTransform, Color.green);

        netForce.Draw(objectTransform, Color.yellow);
    }

    //Metodo para mover la pelota
    public void Move()
    {
        //movimiento
        velocity = velocity + Time.fixedDeltaTime * acceleration;
        objectTransform = objectTransform + Time.fixedDeltaTime * velocity;

        //check bounds
        if (forceMode != ForceMode.Gravity)
        {
            objectTransform.x = CheckBounds(objectTransform.x, ref velocity.x);
            objectTransform.y = CheckBounds(objectTransform.y, ref velocity.y);
        }
        else
        {
            if (velocity.magnitude >= 10) velocity = 10f * velocity.normalized;
        }

        //aplicar movimiento
        transform.position = new Vector3(objectTransform.x, objectTransform.y, 0);
    }

    //Metodo para revisar si la pelota se salio de la camara. En caso tal, invertir la direccion
    private float CheckBounds(float vectorCoord, ref float velocity)
        {
            if ((Mathf.Abs(vectorCoord) + 0.5f) >= cam.orthographicSize)
            {
                velocity *= -1;
                vectorCoord = Mathf.Sign(vectorCoord) * (cam.orthographicSize - 0.5f);
                velocity *= dampening; //ralentiza la velocidad de la bola por la friccion
            }
        return vectorCoord;
        }

    private void ApplyForce(MyVector2D force)
    {
        netForce += force;
        acceleration = netForce / mass;
    }

    private MyVector2D CalculateFriction()
    {
        float netforceMagnitude = planetGravity.magnitude * mass;

        MyVector2D d = -frictionCoefficient * netforceMagnitude * velocity.normalized;
        return d;
    }

    private MyVector2D CalculateFluidResistance()
    {
        float frontalArea = transform.localScale.x;
        int density = 1;

        MyVector2D d = -0.5f * density * Mathf.Pow(velocity.magnitude, 2) * frontalArea * dragCoefficient * velocity.normalized; 
        return d;
    }

    private MyVector2D Gravity()
    {
        MyVector2D r = secondMass.objectTransform - objectTransform;

        MyVector2D g = ((mass * secondMass.mass) / Mathf.Pow(r.magnitude, 2)) * r.normalized;
        return g;
    }
}
