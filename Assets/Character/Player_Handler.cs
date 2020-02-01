using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Handler : MonoBehaviour
{
    int speed = 10;
    float axisx, axisy;
    Vector3 direction;
    RaycastHit2D colRay;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        #region movement
        if (axisx !=0 || axisy != 0  &&  colRay.collider == null)
        {
            speed = 10;
        }
        else
        {
            speed = 0;
        }
        axisx = Input.GetAxis("Horizontal");
        axisy = Input.GetAxis("Vertical");
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        direction = new Vector3(axisx, axisy, 0);
        transform.LookAt(transform.position + 10 * direction, Vector3.forward);
        // transform.eulerAngles = new Vector3(90, transform.eulerAngles.y, transform.eulerAngles.y);
        #endregion

        #region collisionCast
        colRay = Physics2D.Raycast(transform.position,transform.forward,2);
        Debug.DrawRay (transform.position, Vector2.down* 2);

        if (colRay.collider != null)
        {
            Debug.Log("BOWBOWBOWBOWOBW");
        }
        
        #endregion
    }
}
