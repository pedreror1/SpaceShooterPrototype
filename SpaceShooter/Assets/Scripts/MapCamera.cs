using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCamera : MonoBehaviour
{
    [SerializeField]
    Transform target;
    [SerializeField]
    Vector3 Offset;
     

    // Update is called once per frame
    void Update()
    {
        transform.position = target.position + Offset;
    }
}
