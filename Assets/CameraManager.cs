using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class CameraManager : MonoBehaviour
{
    public Transform cubeCentre;
    public Vector3 startPos;
    Player player;
    public AnimationCurve shake;
    public bool isShaking;
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        player = ReInput.players.GetPlayer(0);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, startPos + new Vector3(player.GetAxis("RotateCameraHori") * 30f, player.GetAxis("RotateCameraVert") * 30f, 0),Time.deltaTime);
        transform.LookAt(cubeCentre);

        if(isShaking)
        {
            screenShake();
        }
    }

    void screenShake()
    {

    }
}
