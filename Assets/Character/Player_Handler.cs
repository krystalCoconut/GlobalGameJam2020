using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class Player_Handler : MonoBehaviour
{
    //public GameObject daCube;
  //  Cube_Rotator c_rot;
    int speed = 5 , bounceSpeed = 5;
    float axisx, axisy, bounceTimer = 0.1f;
    Vector2 direction;
    RaycastHit2D colRay;
    bool bounceBack;
    Player player;

    // Start is called before the first frame update
    void Start()
    {
        player = ReInput.players.GetPlayer(0);
    }

    // Update is called once per frame
    void Update()
    {

        #region movement
        //mapping input to float value
        axisx = player.GetAxis("Horizontal");
        axisy = player.GetAxis("Vertical");

        //moves and rotates character when input is detected & not colliding
        if (bounceBack != true)
        {
            if (axisx != 0 || axisy != 0 && colRay.collider == null)
            {
                speed = 5;
                float rot_z = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
            }
            else
            {
                speed = 0;
            }
            //moves character in a direction
            transform.Translate(Vector3.up * speed * Time.deltaTime);
            direction = new Vector2(axisx, axisy);
        }
        else
        {
            transform.Translate(Vector3.down * bounceSpeed * Time.deltaTime);
            bounceTimer -= Time.deltaTime;
            if (bounceTimer <= 0)
            {
                bounceTimer = .1f;
                bounceBack = false;
            }
        }
        #endregion

        #region collisionCast
        //raycast 
        colRay = Physics2D.Raycast(transform.position,transform.up,1);
        // raycast detects collision
        if(colRay.collider!=null)
        {
            if (colRay.collider.CompareTag("Wall"))
            {
                //Debug.Log("you hit wall ^____^^");
                //Debug.Log("you hit wall ^____^^");
                bounceBack = true;
            }
            if (colRay.collider.CompareTag("Spin"))
            {
                //c_rot.spin();
            }
        }

       





        #endregion
    }

    
}
