using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotTester : MonoBehaviour
{
    public Vector3 theRot;
    public Sprite[] thesprites;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        theRot = transform.parent.transform.parent.eulerAngles;
        if (theRot.z == 0f)
        {
            GetComponent<SpriteRenderer>().sprite = thesprites[0];
        }else
            if(theRot.z == 90 || theRot.z == 270 )
        {
            GetComponent<SpriteRenderer>().sprite = thesprites[1];
        }
        else if (theRot.z == 180)
        {
            GetComponent<SpriteRenderer>().sprite = thesprites[2];
        }
    }
}
