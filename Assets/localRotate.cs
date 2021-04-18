using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class localRotate : MonoBehaviour {
    public CubeSide side;
    public float rotation;
    public Quaternion target;
    public bool front;
    // Start is called before the first frame update
    void Start()
    {
           
    }

    // Update is called once per frame
    void Update() {
        transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * 3);
        front = side.currentSide;
        if(front)
            target = Quaternion.AngleAxis(45, Vector3.right) *  Quaternion.AngleAxis(rotation, Vector3.forward) * Quaternion.AngleAxis(90, -Vector3.right);
        else {
            target = Quaternion.AngleAxis(90, -Vector3.right) * Quaternion.AngleAxis(-rotation, Vector3.up);
        }
    }
}
