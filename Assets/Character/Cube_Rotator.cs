using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube_Rotator : MonoBehaviour
{
    public float lerpAlpha;
    public bool timeToSpin;
    public int xrot, yrot, origVal;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (timeToSpin == true)
        {
            lerpAlpha = lerpAlpha += Time.deltaTime;
           //float lerpVal = Mathf.Lerp(xrot,yrot,lerpAlpha);
            transform.eulerAngles = new Vector3(lerpVal, 0, 0);
            if (lerpAlpha > 1)
            {
                timeToSpin = false;
                lerpAlpha = 0;
            }
        
        }
    }

    public void spin(Vector2 colpos)
    {

        timeToSpin = true;
    }
}
