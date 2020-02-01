using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSide : MonoBehaviour
{
    public KeyCode key;
    private BoxCollider bc;

    public List<Collider> colliders;


    public float rotationSpeed = 1f;
    public Vector3 startRotation;
    public bool rotating;
    public float rotateDir;
    public float rotationAmount;
    public CubeHandler ch;
    public Vector3 axis;
    // Start is called before the first frame update
    void Start()
    {
        bc = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {

        if(Input.GetKeyDown(key) && Input.GetKey(KeyCode.LeftShift) && ch.numRotatingSides == 0)
        {
            // Rotate anticlockwise
            startRotation = transform.eulerAngles;
            rotateDir = -1;
            ch.numRotatingSides += 1;
            rotate();
        }else
        if(Input.GetKeyDown(key) && ch.numRotatingSides == 0)
        {
            // Rotate Clockwise
            startRotation = transform.eulerAngles;
            rotateDir = 1;
            ch.numRotatingSides += 1;
            rotate();

        }

        if(rotating)
        {

            rotationAmount += Time.deltaTime *rotationSpeed ;
            if (axis.y != 0)
            {
                float rotationVal = Mathf.Lerp(startRotation.y, startRotation.y + (90 * rotateDir), rotationAmount);
                transform.eulerAngles = new Vector3 (transform.eulerAngles.x,rotationVal,transform.eulerAngles.z);
            }else
            if ( axis.x != 0)
            {
                float rotationVal = Mathf.Lerp(startRotation.x, startRotation.x + (90 * rotateDir), rotationAmount);
                transform.eulerAngles = new Vector3(rotationVal, startRotation.y, startRotation.z);
            }
            else
            if(axis.z != 0)
            {
                float rotationVal = Mathf.Lerp(startRotation.z, startRotation.z + (90 * rotateDir), rotationAmount);
                transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, rotationVal);
            }

            if (rotationAmount >= 1f)
            {
                rotating = false;
                foreach (Collider c in colliders)
                {
                    c.transform.SetParent(null);
                }
                rotationAmount = 0f;
                ch.numRotatingSides -= 1;
                transform.rotation = Quaternion.identity;
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
