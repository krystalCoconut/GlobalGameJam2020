using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class localRotate : MonoBehaviour {

    public float rotation;
    // Start is called before the first frame update
    void Start()
    {
        transform.Rotate(transform.right,50);
        
    }

    // Update is called once per frame
    void Update() {

        transform.rotation = Quaternion.AngleAxis(45, Vector3.right) *  Quaternion.AngleAxis(rotation, Vector3.forward) * Quaternion.AngleAxis(90, -Vector3.right);
    }
}
