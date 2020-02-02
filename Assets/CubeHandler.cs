using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeHandler : MonoBehaviour
{
    public int numRotatingSides;
    public bool isRotating;
    public CubeSide right, up, down, left, front, back;
    private static CubeHandler _instance;

    public bool hasRotatedOnce = false;
    public Collider currentCorner;

    public static CubeHandler Instance { get { return _instance; } }


    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(numRotatingSides == 1)
        {
            hasRotatedOnce = true;
        }
    }
}
