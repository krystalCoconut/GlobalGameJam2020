using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bread_Hider : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject[] BT = GameObject.FindGameObjectsWithTag("BT");
        transform.parent = BT[Random.Range( 0, BT.Length)].transform;
        transform.localPosition = Vector3.zero;
        

    }
    void Update()
    {
        transform.eulerAngles = new Vector3(0, 0, 0);
    }

}
