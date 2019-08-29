using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
public class WaypointManager : MonoBehaviour
{
    [SerializeField][MinMaxSlider(-2000f, 2000f)]
    private Vector2 xRange;
    [SerializeField]
    [MinMaxSlider(-2000f, 2000f)] private Vector2 yRange;
    [SerializeField] [MinMaxSlider(-2000f, 2000f)]
    private Vector2 zRange;
    [SerializeField] bool debug = true;
    public static WaypointManager instance;
    void Start()
    {
        instance = this;
        Reset();
    }
    private void OnDrawGizmos()
    {
        if (debug)
        {
            Gizmos.color = Color.red;
            for (int i = 0; i < transform.childCount; i++)
            {
                Gizmos.DrawSphere(transform.GetChild(i).position, 10f);
            }
        }
    }
    [Button("Calculate Positions")]
    public void Reset()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).position = new Vector3(Random.Range(xRange.x, xRange.y),
                                                         Random.Range(yRange.x, yRange.y),
                                                         Random.Range(zRange.x, zRange.y));
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
