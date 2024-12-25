using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatcherMovement : MonoBehaviour
{
    public float maxLeft = -250f;
    public float maxRight = 250f;
    public float moveSpeed = 250f;
 
    void Update()
    {
        float moveInput = Input.GetAxis("Horizontal");
        if(moveInput!= 0 )
        {
            MoveCatcher(moveInput);
        }
    }

    private void MoveCatcher(float moveInput)
    {
        Vector3 movement = Vector3.right * moveInput * moveSpeed * Time.deltaTime;
        Vector3 newPos = transform.localPosition + movement;
        newPos.x=Mathf.Clamp(newPos.x,maxLeft, maxRight);
        transform.localPosition= newPos;
    }
}
