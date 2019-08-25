using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Transform objective;
    [SerializeField]
    [Range(0f, 100f)]
    public float speed = 1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(objective)
        {
            transform.LookAt(objective);
        }
        transform.position += transform.forward;
    }
}
