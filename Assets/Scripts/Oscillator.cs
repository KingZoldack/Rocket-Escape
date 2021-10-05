using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{

    Vector3 _startingPos;

    [SerializeField]
    Vector3 _movementVector;
    
    [SerializeField] [Range(0,1)]
    float _movementFactor;

    [SerializeField]
    float period = 2f;

    private void Awake()
    {
        _startingPos = transform.position;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (period <= Mathf.Epsilon) { return; } // Prevents NaN Error
        float cycles = Time.time / period; // Continually growing over time

        const float tau = Mathf.PI * 2; //Constant value of 6.283
        float rawSinWave = Mathf.Sin(cycles * tau); // Going from -1 to 1

        _movementFactor = (rawSinWave + 1f) / 2f; // Recalculated to go from 0 to 1 so it's cleaner

        Vector3 offset = _movementVector * _movementFactor;
        transform.position = _startingPos + offset;
    }
}
