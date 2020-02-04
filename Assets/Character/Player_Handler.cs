using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Rewired;

public class Player_Handler : MonoBehaviour
{
    //public GameObject daCube;
  //  Cube_Rotator c_rot;
    int speed = 5 , bounceSpeed = 5;
    float axisx, axisy, bounceTimer = 0.1f;
    Vector2 direction;
    RaycastHit2D colRay;
    public enum State { normal, bounceback, attack, winning} ;
    public State myState;
    Player player;
    Animator anim;
    public float radius;
    public float attackTimer;
    public Transform cubeCentre;
    public GameObject winscreen;

    // rightone 0 right two 1 leftone 2 lefttwo 3
    public BoxCollider2D[] swingHitBoxes = new BoxCollider2D[4];

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

        // Raycast into the cube to find out which one its on
        Ray romano = new Ray(transform.position,transform.forward);
        RaycastHit rh;
        //Debug.DrawRay(transform.position, transform.forward);

        Physics.Raycast(romano, out rh);
        CubeHandler.Instance.currentCorner = rh.collider;

        //moves and rotates character when input is detected & not colliding

        if (player.GetButtonDown("Action"))
        {
            if (myState == State.winning)
            {
                Debug.Log("restart");
                SceneManager.LoadScene(0);
            }
            else if (myState == State.normal)
            {
                anim.speed = 1;
                speed = 0;
                anim.SetBool("Wrench", true);
                attackTimer = 0.6f;
                myState = State.attack;
            }


        }
        if (myState == State.normal)
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
            else if(axisx > 0)
            {
                GetComponent<SpriteRenderer>().flipX = false;
            }
        }
        else if(myState == State.bounceback)
        {
            anim.SetBool("Bounce", true);
            
            transform.Translate(-direction * bounceSpeed * Time.deltaTime);
            bounceTimer -= Time.deltaTime;
            if (bounceTimer <= 0)
            {
                anim.SetBool("Bounce", false);
                bounceTimer = .1f;
                myState = State.normal;
            }
        }
        else if(myState == State.attack)
        {
            attackTimer -= Time.deltaTime;

            if(attackTimer >= 0.25f )
            {
                swingHitBoxes[0].enabled = false;
                swingHitBoxes[1].enabled = false;
                swingHitBoxes[2].enabled = true;
                swingHitBoxes[3].enabled = true;
            }
            else if(attackTimer >= 0.125f )
            {
                swingHitBoxes[0].enabled = true;
                swingHitBoxes[1].enabled = true;
                swingHitBoxes[2].enabled = false;
                swingHitBoxes[3].enabled = false;
            }

            if (attackTimer <= 0)
            {
                anim.SetBool("Wrench", false);

                // turn off all the hitboxes
                swingHitBoxes[0].enabled = false;
                swingHitBoxes[1].enabled = false;
                swingHitBoxes[2].enabled = false;
                swingHitBoxes[3].enabled = false;

                attackTimer = 0f;
                myState = State.normal;
               
            }
        }
        #endregion

        #region collisionCast
       
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

        if (transform.position.y >= 9.5f || transform.position.y <= -9.5f)
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

    void winner()
    {
        myState = State.winning;
        Debug.Log("you are Winner");
        winscreen.SetActive(true);
    }

    /// <summary>
    /// Sent when an incoming collider makes contact with this object's
    /// collider (2D physics only).
    /// </summary>
    /// <param name="other">The Collision2D data associated with this collision.</param>
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("Bread"))
            {
                winner();
            }
    }
    
}
