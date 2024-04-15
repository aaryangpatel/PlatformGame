using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObstacle : MonoBehaviour
{
    void Update()
    {
        transform.position += transform.forward * 0.02f;
    }

    void OnCollisionEnter(Collision collision)
    {
        transform.Rotate(Vector3.up, 180f);
    }
}
