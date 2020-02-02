using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotTester : MonoBehaviour
{
    public Vector3 theRot;
    public Sprite[] thesprites;
    public bool dontRotate;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(Mathf.Floor(transform.position.x) + 0.5f, Mathf.Floor(transform.position.y) + 0.5f,transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        if (dontRotate)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }

        

        theRot = transform.parent.parent.parent.eulerAngles;
        if (theRot.z == 0f)
        {
            GetComponent<SpriteRenderer>().flipX = false;
            GetComponent<SpriteRenderer>().sprite = thesprites[0];
        }else
            if(theRot.z == 90)
        {
            GetComponent<SpriteRenderer>().flipX = true;
            GetComponent<SpriteRenderer>().sprite = thesprites[1];
        }else if (theRot.z == 270)
        {
            GetComponent<SpriteRenderer>().flipX = false;
            GetComponent<SpriteRenderer>().sprite = thesprites[1];
        }
        else if (theRot.z == 180)
        {
            GetComponent<SpriteRenderer>().flipX = false;
            GetComponent<SpriteRenderer>().sprite = thesprites[2];
        }
    }
}
