using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotTester : MonoBehaviour
{
    public Vector3 theRot;
    public Sprite[] thesprites;
    public bool dontRotate;
    public CubeSide front;
    public Transform mySide;

    public GameObject smoke;
    private SpriteRenderer spriteRenderer;
    private PolygonCollider2D polygonCollider2D;

    public int myRot;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        polygonCollider2D = GetComponent<PolygonCollider2D>();
        transform.position = new Vector3(Mathf.Floor(transform.position.x) + 0.5f, Mathf.Floor(transform.position.y) + 0.5f,transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        if (dontRotate)
        {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 0);
        }

        

        theRot = transform.parent.parent.parent.eulerAngles;
        if (theRot.z == 0f + myRot)
        {
            spriteRenderer.flipX = false;
            spriteRenderer.sprite = thesprites[0];
        }else
            if(theRot.z == 90 + myRot)
        {
            spriteRenderer.flipX = true;
            spriteRenderer.sprite = thesprites[1];
        }else if (theRot.z == 270 + myRot)
        {
            spriteRenderer.flipX = false;
            spriteRenderer.sprite = thesprites[1];
        }
        else if (theRot.z == 180 + myRot)
        {
            spriteRenderer.flipX = false;
            spriteRenderer.sprite = thesprites[2];
        }


        // Raycast into the cube to find out which one its on
        Ray romano = new Ray(transform.position, -transform.forward);
        RaycastHit rh;
        Debug.DrawRay(transform.position, -transform.forward);

        Physics.Raycast(romano, out rh);
        if(rh.collider != null)
        mySide = rh.collider.transform;

        if(CubeHandler.Instance.front.colliders.Contains(rh.collider))
        {
            //Debug.Log("HIT");
            //spriteRenderer.enabled = true;
            polygonCollider2D.enabled = true;
        }
        else
        {
            //spriteRenderer.enabled = false;
            polygonCollider2D.enabled = false;
        }

    }

    /// <summary>
    /// Sent when another object enters a trigger collider attached to this
    /// object (2D physics only).
    /// </summary>
    /// <param name="other">The other Collider2D involved in this collision.</param>
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("PlayerAttack"))
        {
            DestroyMe();
        }
    }

    public void DestroyMe()
    {
        Instantiate(smoke, (Vector2)transform.position,Quaternion.identity);
        Destroy(gameObject);
    }

}


