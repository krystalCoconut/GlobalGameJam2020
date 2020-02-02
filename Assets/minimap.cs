using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class minimap : MonoBehaviour
{
    bool showingMap;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<RawImage>().CrossFadeAlpha(0f, 0f, true);
    }

    // Update is called once per frame
    void Update()
    {
        if(CubeHandler.Instance.hasRotatedOnce)
        {
            GetComponent<RawImage>().CrossFadeAlpha(1.0f, 1f, true);
        }
    }
}
