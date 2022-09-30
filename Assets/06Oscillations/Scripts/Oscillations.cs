using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillations : MonoBehaviour
{
    private enum OscillationMode
    {
        X,
        Diagonal,
        Xnoise
    }

    [SerializeField] OscillationMode mode;

    [Range(0f, 5f)] [SerializeField] float oscillationScale = 1;
    [Range(0f, 5f)] [SerializeField] float oscillationTime;

    Vector3 initialPosition;

    // Start is called before the first frame update
    void Start()
    {
        initialPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        switch (mode)
        {
            case OscillationMode.X:
                XOscillation();
                break;
            case OscillationMode.Diagonal:
                DiagonalOscillation();
                break;
            case OscillationMode.Xnoise:
                Noise();
                break;
            default:
                break;
        }

    }

    private void Noise()
    {
        float x = Mathf.Sin(Random.Range(0, 10) * Time.time) + Mathf.Cos(Random.Range(0, 10) * Time.time) + Mathf.Sin(Random.Range(0, 10) * Time.time) + Mathf.Cos(Random.Range(0, 10) * Time.time);
        transform.position = initialPosition + new Vector3(0.5f * x, 0f, 0f);
    }

    private void XOscillation()
    {
        float x = oscillationScale * Mathf.Sin(2 * Mathf.PI * (Time.time / oscillationTime));
        transform.position = initialPosition + new Vector3(x, 0f, 0f);
    }

    private void DiagonalOscillation()
    {
        float x = oscillationScale * Mathf.Sin(2 * Mathf.PI * (Time.time / oscillationTime));
        transform.position = initialPosition + new Vector3(x, x, 0f);
    }
}
