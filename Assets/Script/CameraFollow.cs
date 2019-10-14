using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform ballTransform;
    private float offsetY;

    void Start()
    {
        ballTransform = GameObject.Find("BallCameraOfset").transform;
        offsetY = transform.position.y - ballTransform.position.y;
    }


    public void ballAttach(Transform baTransform)
    {
        this.ballTransform = baTransform.GetChild(0);
        transform.position = new Vector3(0,6.3f,-9);
        offsetY = transform.position.y - ballTransform.position.y;
    }

    void LateUpdate()
    {
        if(ballTransform.gameObject.active)
            transform.position = new Vector3(transform.position.x, ballTransform.position.y + offsetY, transform.position.z);
    }
}
