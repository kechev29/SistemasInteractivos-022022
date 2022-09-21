using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour
{
    public enum MovementMode { 
        ConstantSpeed,
        Acceleration
    }

    [SerializeField] private MovementMode mode;
    [SerializeField] float startingSpeed = 5f;
    Vector3 velocity = new Vector3();

    void Start()
    {
        velocity = new Vector3(startingSpeed, 0f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = GetWorldMousePosition();

        Vector3 directionToTarget = mousePos - transform.position;

        float angle;

        if (mode == MovementMode.ConstantSpeed)
        {
            angle = Mathf.Atan2(directionToTarget.y, directionToTarget.x);
        }
        else
        {
            angle = Mathf.Atan2(velocity.y, velocity.x);
        }

        RotateZ(angle);
        Move(directionToTarget);

    }

    private Vector4 GetWorldMousePosition()
    {
        Camera camera = Camera.main;
        Vector3 screenPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, camera.nearClipPlane);
        Vector4 worldPos = Camera.main.ScreenToWorldPoint(screenPos);
        return worldPos;
    }

    private void RotateZ(float angle)
    {
        transform.rotation = Quaternion.Euler(0.0f, 0.0f, angle * Mathf.Rad2Deg);
    }

    private void Move(Vector3 target)
    {
        if (mode == MovementMode.ConstantSpeed)
        {
            //VEL NO ACCEL
            if (target.magnitude > 1f)
                transform.position += target.normalized * startingSpeed * Time.deltaTime;
        } else { 
            //ACCEL
            Vector3 acceleration = (Vector3)GetWorldMousePosition() - transform.position;
            velocity += acceleration * Time.deltaTime;
            transform.position += new Vector3(velocity.x, velocity.y, 0f) * Time.deltaTime;
        }
    }

}
