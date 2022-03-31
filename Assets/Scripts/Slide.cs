using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slide : MonoBehaviour
{
    Vector3 startingPosition;
    [SerializeField] Vector3  movementVector;
    [SerializeField] [Range(0, 1)] float movementFactor;
    [SerializeField] float period;

    void Start()
    {
        startingPosition = transform.position;    
    }

    
    void Update()
    {
        Slider();
    }

    private void Slider() {
        float cycles = Time.time / period;
        const float tau = Mathf.PI * 2;
        float rawSinwave = Mathf.Sin(cycles * tau);
        movementFactor = (rawSinwave + 1f) / 2f;
        Vector3 offset = movementVector * movementFactor;
        transform.position = startingPosition + offset;
    }
}
