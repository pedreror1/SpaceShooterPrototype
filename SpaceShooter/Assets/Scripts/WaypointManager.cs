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

    public static WaypointManager instance;
    void Start()
    {
        instance = this;
        Reset();
    }
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
