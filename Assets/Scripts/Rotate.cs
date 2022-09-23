using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    [SerializeField] float XAngle = 0f;
    [SerializeField] float YAngle = 0f;
    [SerializeField] float ZAngle = 0f;
    void Update()
    {
        transform.Rotate(XAngle, YAngle,ZAngle);
    }
}
