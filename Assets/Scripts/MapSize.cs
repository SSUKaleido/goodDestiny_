using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSize : MonoBehaviour
{
    public Vector2 center;
    public Vector2 mapSize;
    public void Awake()
    {
        CameraPrac.instance.mapSize = mapSize;
        CameraPrac.instance.center = center;
    }
}
