using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.Tilemaps;
using Rewired;

public class CubeSide : MonoBehaviour
{
    public string key;
    private BoxCollider bc;

    public List<Collider> colliders;


    public float rotationSpeed = 1f;
    public Vector3 startRotation;
    public bool rotating;
    public float rotateDir;
    public float rotationAmount;
    public CubeHandler ch;
    public Vector3 axis;
    public Player player;

    public CubeSide oppositeSide;

    public Transform cubeCentre;

    public bool collidersEnabled;
    public bool collidersDisabled;

    private CinemachineVirtualCamera vCam;
    public bool currentSide;

    // Start is called before the first frame update
    void Start()
    {
        // used for rotating the current face
        player = ReInput.players.GetPlayer(0);

        vCam = transform.GetChild(0).GetComponent<CinemachineVirtualCamera>();
        
        
        bc = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        // Make sure the side doesn't contain the block the player is standing on
        if(player.GetButtonDown(key) && player.GetButton("Invert") && ch.numRotatingSides == 0)
        {
            if(!colliders.Contains(CubeHandler.Instance.currentCorner))
            {
                // Rotate anticlockwise
                rotateDir = -1;
                rotate();
            }
            else
            {
                oppositeSide.rotateDir = 1;
                oppositeSide.rotate();
            }
            
        }
        else if(player.GetButtonDown(key) && ch.numRotatingSides == 0)
        {
            if (!colliders.Contains(CubeHandler.Instance.currentCorner))
            {
                // Rotate Clockwise
                rotateDir = 1;
                rotate();
            }
            else
            {
                // Rotate other side anticlockwise
                oppositeSide.rotateDir = -1;
                oppositeSide.rotate();
            }
            

        }

        if(rotating)
        {


            rotationAmount += Time.deltaTime *rotationSpeed ;
            if (axis.y != 0)
            {
                float rotationVal = Mathf.Lerp(startRotation.y, startRotation.y + (90 * rotateDir), rotationAmount);
                transform.eulerAngles = new Vector3 (transform.eulerAngles.x,rotationVal,transform.eulerAngles.z);
            }else
            if ( axis.x != 0)
            {
                float rotationVal = Mathf.Lerp(startRotation.x, startRotation.x + (90 * rotateDir), rotationAmount);
                transform.eulerAngles = new Vector3(rotationVal, startRotation.y, startRotation.z);
            }
            else
            if(axis.z != 0)
            {
                float rotationVal = Mathf.Lerp(startRotation.z, startRotation.z + (90 * rotateDir), rotationAmount);
                transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, rotationVal);
            }


            if (rotationAmount >= 1f)
            {
                rotating = false;
                foreach (Collider c in colliders)
                {
                    c.transform.SetParent(cubeCentre);
                }
                rotationAmount = 0f;
                ch.numRotatingSides -= 1;
                transform.rotation = Quaternion.identity;
            }

        }
    }



    public void rotate()
    {

        startRotation = transform.eulerAngles;
        ch.numRotatingSides += 1;

        // Parent all to the rotator
        foreach (Collider c in colliders)
        {
            c.transform.SetParent(transform);
        }

        rotating = true;
        

    }

    private void OnTriggerEnter(Collider other)
    {
        if (!colliders.Contains(other) && other.name != "RotateChecker")
        {
            
            colliders.Add(other);

            if(collidersEnabled)
            {
                for(int i=0;i<3;i++)
                {
                    // enable collision tilemap
                    other.transform.GetChild(i).GetChild(0).gameObject.GetComponent<TilemapCollider2D>().enabled = true;
                }
            }
        }
        
//        Debug.Log(other.name);
        
        if(other.name == "RotateChecker") {
            transform.GetChild(0).GetComponent<CinemachineVirtualCamera>().Priority = 1000;
            vCam.transform.rotation = CubeHandler.Instance.player.transform.rotation;

            currentSide = true;
            //transform.GetChild(0).GetComponent<CinemachineVirtualCamera>().m_Lens.Dutch = FindObjectOfType<Player_Handler>().transform.eulerAngles.z;
        }
    }
    
    Vector3 getDominantAxis(Vector3 position) {
        if( Mathf.RoundToInt(Mathf.Abs(position.x)) == 30 ) 
            return new Vector3(Mathf.Sign(position.y),0,0);
        else if( Mathf.RoundToInt(Mathf.Abs(position.y)) == 30 )
            return new Vector3(0,Mathf.Sign(position.y),0);
        else if ( Mathf.RoundToInt(Mathf.Abs(position.z)) == 30 )
            return new Vector3(0, 0, Mathf.Sign(position.z));
        else
            return Vector3.zero;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.name != "RotateChecker") {
            colliders.Remove(other);
            if (collidersEnabled )
            {
                for (int i = 0; i < 3; i++)
                {
                    // enable collision tilemap
                    other.transform.GetChild(i).GetChild(0).gameObject.GetComponent<TilemapCollider2D>().enabled = false;
                }
            }
        }

        if (other.name == "RotateChecker") {
            transform.GetChild(0).GetComponent<CinemachineVirtualCamera>().Priority = 00;
            vCam.transform.rotation = CubeHandler.Instance.player.transform.rotation;
            currentSide = false;
            //transform.GetChild(0).GetComponent<CinemachineVirtualCamera>().m_Lens.Dutch = FindObjectOfType<Player_Handler>().transform.eulerAngles.z;
            //transform.GetChild(0).GetComponent<CinemachineVirtualCamera>().
        }
    }

}
