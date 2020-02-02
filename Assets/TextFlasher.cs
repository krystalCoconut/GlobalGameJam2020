using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextFlasher : MonoBehaviour
{
    float timer;
    Text daText;
    public Color[] ColorList;
    // Start is called before the first frame update
    void Start()
    {
        daText = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            ChangeColor();
        }
    }

    void ChangeColor()
    {
        daText.color = ColorList[Random.Range(0, ColorList.Length)];
        timer = Random.Range(0.05f, 0.1f);
    }

}
