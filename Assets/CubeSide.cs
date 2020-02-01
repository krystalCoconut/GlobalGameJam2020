using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSide : MonoBehaviour
{
    public KeyCode key;
    private BoxCollider bc;

    public List<Collider> colliders;


    public float rotationSpeed = 1f;
    public float startRotation;
    public bool rotating;
    public float rotateDir;

    public Quaternion startRot;
    // Start is called before the first frame update
    void Start()
    {
        bc = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(key))
        {
            // Rotate Clockwise
            startRotation = transform.rotation.eulerAngles.y;
            startRot = transform.rotation;
            rotateDir = 1;
            rotate();
        }

        if(Input.GetKeyDown(key) && Input.GetKey(KeyCode.LeftShift))
        {
            // Rotate anticlockwise
            startRotation = transform.rotation.eulerAngles.y;
            startRot = transform.rotation;
            rotateDir = -1;
            rotate();
        }

        if(rotating)
        {
            // Limit rotation to 90 degrees
            if( Mathf.Abs(transform.rotation.eulerAngles.y -(startRotation + 90 * rotateDir)) > 0.1f )
                transform.Rotate(startRot.eulerAngles + transform.up, Time.deltaTime * rotationSpeed * rotateDir);
            else
            {
                
                transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.eulerAngles.x, startRotation + 90 * rotateDir, transform.rotation.eulerAngles.z));
                // Stop rotating
                rotating = false;
                
                // Unparent all the children
                foreach(Collider c in colliders)
                {
                    c.transform.SetParent(null);
                }
            }
        }
    }

    public void rotate()
    {
        // Parent all to the rotator
        foreach (Collider c in colliders)
        {
            c.transform.SetParent(transform);
        }

        rotating = true;
        

    }

    private void OnTriggerEnter(Collider other)
    {
        if (!colliders.Contains(other)) { colliders.Add(other); }
    }

    private void OnTriggerExit(Collider other)
    {
        colliders.Remove(other);
    }

}
