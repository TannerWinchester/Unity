using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class Oscillator : MonoBehaviour
{
    [SerializeField] Vector3 movementVector;
    [Range(0, 1)] // you can stack attributes 
    [SerializeField] float movementFactor; // 0 for not moved, 1 for fully moved
    [SerializeField] float period = 4.0f; // default period is 2 seconds

    Vector3 startingPos; // must be stored for absolute movement

    // Start is called before the first frame update
    void Start()
    {
        startingPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(period <= Mathf.Epsilon) { return; }
        float cycles = Time.time / period;
        const float tau = Mathf.PI * 2.0f;
        float rawSineWave = Mathf.Sin(cycles * tau);
        movementFactor = rawSineWave / 2.0f + 0.5f;
        Vector3 offset = movementVector * movementFactor;
        transform.position = startingPos + offset;
    }
}
