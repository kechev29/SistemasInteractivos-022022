using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphMaker : MonoBehaviour
{
    [SerializeField] GameObject template;
    [SerializeField] int pointsAmount;
    [SerializeField] float distanceFactor;
    [SerializeField] float amplitude;

    GameObject[] pointsArray; 

    // Start is called before the first frame update
    void Start()
    {
        pointsArray = new GameObject[pointsAmount];

        for (int i = 0; i < pointsAmount; i++)
        {
            pointsArray[i] = Instantiate(template, transform);
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < pointsAmount; i++)
        {
            float x = i * distanceFactor;
            float y = amplitude * Mathf.Sin(x + Time.time);
            pointsArray[i].transform.localPosition = new Vector3(x, y, 0);
        }
    }
}
