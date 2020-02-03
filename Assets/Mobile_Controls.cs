using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mobile_Controls : MonoBehaviour
{
   Player_Handler PH;
    // Start is called before the first frame update
    void Start()
    {
       PH = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Handler>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MoveUP ()
    {
        PH.usingmobile = true;
        PH.axisx = 0;
        PH.axisy = 1;
       
        //PH.enabled = false;
    }
    public void MoveDown()
    {
        PH.usingmobile = true;
        PH.axisx = 0;
        PH.axisy = -1;
    }
    public void MoveLeft()
    {
        PH.usingmobile = true;
        PH.axisx = -1;
        PH.axisy = 0;
    }
    public void MoveRight()
    {
        PH.usingmobile = true;
        PH.axisx = 1;
        PH.axisy = 0;
    }
    public void StopMove()
    {
        PH.usingmobile = true;
        PH.axisx = 0;
        PH.axisy = 0;
    }
}
