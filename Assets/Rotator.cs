using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    public GameObject left, middle, right;

    public bool rotating;
    public bool rotatingRight;
    public float rotateSpeed;
    public float targetPosition;
    public Vector2 startPos;
    // Start is called before the first frame update
    void Start()
    {
        rotating = false;   
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.RightArrow) && !rotating)
        {
            rotateRight();
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow) && !rotating)
        {
            rotateLeft();
        }
        
        if(rotating)
        {
            // Must be rotating Right
            if(Mathf.Abs(transform.position.x - (startPos.x + targetPosition)) < 0.01f)
            {
                // TODO: remove the lerp
                // tween over certain amount of time ( like .3 of a second) 
                // use animation curve
                transform.position = new Vector2(transform.position.x + (1f/16f * Mathf.Sign(targetPosition)),transform.position.y);
            }
            else
            {
                transform.position = new Vector2(startPos.x + targetPosition, transform.position.y);
                rotating = false;
                left.transform.SetParent(null);
                middle.transform.SetParent(null);
                right.transform.SetParent(null);

                // Move to the right 
                if(targetPosition < 0)
                {
                    left.transform.position = new Vector2(left.transform.position.x + 30, left.transform.position.y);
                    GameObject tmp;
                    tmp = left;
                    left = middle;
                    middle = right;
                    right = tmp;

                }else 
                if(targetPosition > 0)
                {
                    right.transform.position = new Vector2(right.transform.position.x - 30, right.transform.position.y);
                    GameObject tmp;
                    tmp = right;
                    right = middle;
                    middle = left;
                    left = tmp;
                }
            }
           
            
        }
    }
    
    void rotateRight()
    {
        startPos = transform.position;
        targetPosition = 10;
        rotating = true;
        rotatingRight = true;
        left.transform.SetParent(transform);
        middle.transform.SetParent(transform);
        right.transform.SetParent(transform);

    }

    void rotateLeft()
    {
        startPos = transform.position;
        targetPosition = -10;
        rotating = true;
        rotatingRight = false;
        left.transform.SetParent(transform);
        middle.transform.SetParent(transform);
        right.transform.SetParent(transform);
    }
}


