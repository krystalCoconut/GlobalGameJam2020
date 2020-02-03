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
    public float axisx, axisy, bounceTimer = 0.1f;
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
    public bool usingmobile;
    

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
        
            if(usingmobile!=true)
            {
                axisx = player.GetAxis("Horizontal");
                axisy = player.GetAxis("Vertical");
            }

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

            GetComponent<PolygonCollider2D>().enabled = false;
            GetComponent<PolygonCollider2D>().enabled = true;

            if (attackTimer <= 0)
            {
                anim.SetBool("Wrench", false);
                attackTimer = 0f;
                myState = State.normal;
                GetComponent<PolygonCollider2D>().enabled = false;
                GetComponent<PolygonCollider2D>().enabled = true;
            }
        }
        #endregion

        #region collisionCast
        //raycast 
        colRay = Physics2D.Raycast(transform.position,direction,radius);
        // raycast detects collision
        if(colRay.collider!=null)
        {
            if (colRay.collider.CompareTag("Wall"))
            {
                Debug.Log(colRay.collider);
                //Debug.Log("you hit wall ^____^^");
                myState = State.bounceback;
            }
            if (colRay.collider.CompareTag("Bread"))
            {
                winner();
            }
            if(colRay.collider.CompareTag("Iter"))
            {

                colRay.collider.GetComponent<RotTester>().DestroyMe();
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

    void winner()
    {
        myState = State.winning;
        Debug.Log("you are Winner");
        winscreen.SetActive(true);
    }
    
}
