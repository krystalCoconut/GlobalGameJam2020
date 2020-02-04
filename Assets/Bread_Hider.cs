using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bread_Hider : MonoBehaviour
{
    private Transform mySide;
    // Start is called before the first frame update
    void Start()
    {
        GameObject[] BT = GameObject.FindGameObjectsWithTag("BT");
        transform.parent = BT[Random.Range( 0, BT.Length)].transform;
        transform.localPosition = Vector3.zero;
        

    }
    void Update()
    {
        transform.eulerAngles = new Vector3(0,0, 0);

        // Raycast into the cube to find out which one its on
        Ray romano = new Ray(transform.position, transform.forward);
        RaycastHit rh;
        Debug.DrawRay(transform.position, transform.forward);

        Physics.Raycast(romano, out rh);
        if(rh.collider != null)
        mySide = rh.collider.transform;

        if(CubeHandler.Instance.front.colliders.Contains(rh.collider))
        {
            //GetComponent<SpriteRenderer>().enabled = true;
            GetComponent<BoxCollider2D>().enabled = true;
        }
        else
        {
            //GetComponent<SpriteRenderer>().enabled = true;
            GetComponent<BoxCollider2D>().enabled = false;
        }
    }

}
