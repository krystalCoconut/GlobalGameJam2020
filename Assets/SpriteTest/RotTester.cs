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
        if (theRot.x < 90 && theRot.x > -90)
        {
            GetComponent<SpriteRenderer>().sprite = thesprites[2];
        }
    }
}
