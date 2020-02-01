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
    Animator anim;

    public Transform cubeCentre;

    // Start is called before the first frame update
    void Start()
    {
        player = ReInput.players.GetPlayer(0);
        anim = GetComponent<Animator>();
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

                speed = 3;
                float playrate = Mathf.Abs(axisx) + Mathf.Abs(axisy);
                anim.speed = playrate;
                anim.SetBool("Moving", true);
            }
            else
            {
                anim.speed = 1;
                speed = 0;
                anim.SetBool("Moving", false);
            }
            //moves character in a direction
            transform.Translate(direction * speed * Time.deltaTime);
            direction = new Vector2(axisx, axisy);
            if (axisx < 0)
            {
                GetComponent<SpriteRenderer>().flipX = true;
            }
            else
            {
                GetComponent<SpriteRenderer>().flipX = false;
            }
        }
        else
        {
            anim.SetBool("Bounce", true);
            transform.Translate(-direction * bounceSpeed * Time.deltaTime);
            bounceTimer -= Time.deltaTime;
            if (bounceTimer <= 0)
            {
                anim.SetBool("Bounce", false);
                bounceTimer = .1f;
                bounceBack = false;
            }
        }
        #endregion

        #region collisionCast
        //raycast 
        colRay = Physics2D.Raycast(transform.position,direction,1);
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

       
        if(transform.position.x >= 9 || transform.position.x <= -9)
        {
            if(transform.position.x >= 9)
            {
                cubeCentre.Rotate(new Vector3(0, 90, 0));

                normalise();
            }
            else if(transform.position.x <= -9)
            {
                cubeCentre.Rotate(new Vector3(0, -90, 0));

                normalise();
            }
            transform.position = new Vector3(-transform.position.x, transform.position.y,transform.position.z);
        }

        if (transform.position.y >= 9 || transform.position.y <= -9)
        {
            if (transform.position.y >= 9)
            {
                cubeCentre.Rotate(new Vector3(-90, 0, 0));

                normalise();
            }
            else if (transform.position.y <= -9)
            {
                cubeCentre.Rotate(new Vector3(90, 0, 0));

                normalise();
            }
            transform.position = new Vector3(transform.position.x, -transform.position.y, transform.position.z);
        }
        #endregion
    }


    void normalise()
    {
        Transform[] children = new Transform[8];
        for (int i = 0; i < 8; i++)
        {
            children[i] = cubeCentre.GetChild(i);
        }
        cubeCentre.DetachChildren();

        cubeCentre.rotation = Quaternion.identity;

        for (int i = 0; i < 8; i++)
        {
            children[i].SetParent(cubeCentre);
        }
    }
    
}
