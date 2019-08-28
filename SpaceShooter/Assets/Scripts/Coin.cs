using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 3f);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag=="Player")
        {
            Destroy(gameObject);
            GameManager.Instance.AddCoin();
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
