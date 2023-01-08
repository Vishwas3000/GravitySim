using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControler : MonoBehaviour
{
    public Transform player;
    public float dampTime = 0.4f;
    public float dampSpeed = 0.03f;
    public float zoomRate = 2f;
    public float moveSpeed;


    Vector2 _camPos;
    Vector2 _velocity = Vector3.zero;
    bool targetPlayer = true;
    Vector2 _moveTo ;
    float fixedHeight;

    // Start is called before the first frame update
    void Start()
    {
        _camPos= transform.position;
        fixedHeight = transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Y))
        {
            targetPlayer ^= true;
            if(!targetPlayer)
            {
                _moveTo = transform.position;
            }
        }
        if(Input.mouseScrollDelta.y > 0)
        {
            Camera.main.orthographicSize /= zoomRate;
        }
        if(Input.mouseScrollDelta.y < 0)
        {
            Camera.main.orthographicSize *= zoomRate;
        }
        MoveCam();
    }

    private void MoveCam()
    {
        if (targetPlayer)
            FollowPlayer();

        else
        {
            ControlCamera();
        }
    }

    void FollowPlayer()
    {
        Vector2 playerPos = player.transform.position;
        _camPos = Vector2.SmoothDamp(transform.position, playerPos, ref _velocity, dampTime);
        transform.position = new Vector3(_camPos.x, _camPos.y, fixedHeight);
    }
    void ControlCamera()
    {

        if(Input.GetKey(KeyCode.W))
        {
            _moveTo.y += moveSpeed;
        }
        if (Input.GetKey(KeyCode.S))
        {
            _moveTo.y -= moveSpeed;
        }
        if (Input.GetKey(KeyCode.A))
        {
            _moveTo.x -= moveSpeed;
        }
        if (Input.GetKey(KeyCode.D))
        {
            _moveTo.x += moveSpeed;
        }

        Vector2 currPos = Vector2.Lerp(transform.position, _moveTo, Time.deltaTime * dampSpeed);
        transform.position = new Vector3(currPos.x, currPos.y, fixedHeight);
        //Vector2 currPos = Vector2.SmoothDamp(transform.position, _moveTo, ref _velocity, dampSpeed);
        //transform.position = new Vector3(currPos.x, currPos.y, fixedHeight);
    }
}
