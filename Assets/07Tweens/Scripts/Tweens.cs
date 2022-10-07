using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tweens : MonoBehaviour
{
    [SerializeField] Transform targetPoint;
    [SerializeField, Range(0f, 1f)] float normalizedTime;
    [SerializeField] float duration;
    [SerializeField] Color initialColor;
    [SerializeField] Color finalColor;

    [SerializeField] AnimationCurve curve;

    private float currentTime;
    private Vector3 initialPosition;
    private Vector3 finalPosition;

    private SpriteRenderer sprite;

    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        StartTween();
    }


    void Update()
    {
        normalizedTime = currentTime / duration;
        transform.position = Vector3.Lerp(initialPosition, finalPosition, curve.Evaluate(normalizedTime));
        sprite.color = Color.Lerp(initialColor, finalColor, curve.Evaluate(normalizedTime));

        currentTime += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Space)) StartTween();

    }

    private void StartTween()
    {
        currentTime = 0f;
        sprite.color = initialColor;
        initialPosition = transform.position;
        finalPosition = targetPoint.position;
    }

    private float EaseInQuad(float x)
    {
        return x * x;
    }

}
