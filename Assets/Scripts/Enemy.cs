using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Vector3 startingPosition;
    [SerializeField] Vector3 movementvector;
    float movementfactor;
    [SerializeField] float period = 2f;
    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(period <= Mathf.Epsilon){return;}
        float cycles = Time.time / period;
        const float taru = Mathf.PI *2;
        float rawSinWave = Mathf.Sin(cycles*taru);
        movementfactor = (rawSinWave +1f) / 2f;
        Vector3 offset = movementvector * movementfactor;
        transform.position = startingPosition + offset;
    }
}

