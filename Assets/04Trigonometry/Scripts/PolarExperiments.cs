using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PolarExperiments : MonoBehaviour
{
    [SerializeField] float radius;
    [Range(0f, 360f)][SerializeField] float angleDegree;

    [Header("Spiral")]
    [SerializeField] bool spiralAnimation;
    [SerializeField] float angleSpeed = 0.2f;
    [SerializeField] float angleAcceleration;
    [SerializeField] float radiusSpeed = 0.01f;
    [SerializeField] float radiusAcceleration;

    private void Start()
    {
        
    }

    
    private void Update()
    {
        if (spiralAnimation)
        {
            transform.position = Spiral();
        }

        //Debug.DrawLine(Vector3.zero, ConvertPolarToCartesian(radius, angleDegree), Color.red);

    }

    private Vector3 ConvertPolarToCartesian(float r, float a)
    {
        Vector3 newCoords = new Vector3(r * Mathf.Cos(a * Mathf.Deg2Rad), //x
                                        r * Mathf.Sin(a * Mathf.Deg2Rad), //y
                                        0f );

        return newCoords;
    }

    private Vector3 Spiral()
    {
        angleSpeed += angleAcceleration * Time.deltaTime;
        angleDegree += angleSpeed * Time.deltaTime;

        radiusSpeed += radiusAcceleration * Time.deltaTime;
        radius += radiusSpeed * Time.deltaTime;


        if(Mathf.Abs(radius) >= Camera.main.orthographicSize)
        {
            angleSpeed *= -1;
            radiusSpeed *= -1;

            angleAcceleration *= -1;
            radiusAcceleration *= -1;
        }
        

        return ConvertPolarToCartesian(radius, angleDegree); 
    }


}
