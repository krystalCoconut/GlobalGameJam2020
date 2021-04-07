using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteRotate : MonoBehaviour {
    public float angle;

    public Vector3 axis;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update() {

        axis = getDominantAxis(transform.position);
        angle = CubeHandler.Instance.player.transform.eulerAngles.z;
        transform.rotation = Quaternion.AngleAxis(-angle, axis)  ;

    }

    
    Vector3 getDominantAxis(Vector3 position) {
        if( Mathf.RoundToInt(Mathf.Abs(position.x)) > 10 ) 
            return new Vector3(Mathf.Sign(position.y),0,0);
        else if( Mathf.RoundToInt(Mathf.Abs(position.y)) > 10 )
            return new Vector3(0,Mathf.Sign(position.y),0);
        else if ( Mathf.RoundToInt(Mathf.Abs(position.z)) > 10 )
            return new Vector3(0, 0, Mathf.Sign(position.z));
        else
            return Vector3.zero;
    }
    
}
