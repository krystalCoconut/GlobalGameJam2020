using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class modelRotate : MonoBehaviour {
    public float angle,angle2;

    public Vector3 axis,axis2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update() {

        axis = getDominantAxis(transform.position);
        angle = CubeHandler.Instance.player.transform.eulerAngles.z;
        angle2 = CubeHandler.Instance.player.transform.eulerAngles.z;
        transform.rotation = Quaternion.AngleAxis(-angle, axis) * Quaternion.AngleAxis(angle2, axis2) ;

    }

    public void RotateRight() {
        
    }

    
    Vector3 getDominantAxis(Vector3 position) {
        return axis;
        if( Mathf.RoundToInt(Mathf.Abs(position.x)) > 10 ) 
            return new Vector3(0,Mathf.Sign(position.y),Mathf.Sign(position.y));
        else if( Mathf.RoundToInt(Mathf.Abs(position.y)) > 10 )
            return new Vector3(Mathf.Sign(position.y),0,Mathf.Sign(position.y));
        else if ( Mathf.RoundToInt(Mathf.Abs(position.z)) > 10 )
            return new Vector3(Mathf.Sign(position.y), Mathf.Sign(position.y), 0);
        else
            return Vector3.zero;
    }
    
}

