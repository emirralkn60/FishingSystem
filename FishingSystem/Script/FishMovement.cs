using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishMovement : MonoBehaviour
{
    public float maxLeft = -250f;
    public float maxRight = 250f;

    public float moveSpeed = 250f;
    public float changeFrekans = 0.01f;

    public float targetPosition;
    public bool movingRight = true;

    private void Start()
    {
        targetPosition=Random.Range(maxLeft,maxRight);
    }
    private void Update()
    {
        transform.localPosition = Vector3.MoveTowards(transform.localPosition,
            new Vector3(targetPosition, transform.localPosition.y, transform.localPosition.z), moveSpeed * Time.deltaTime);
        if(Mathf.Approximately(transform.localPosition.x,targetPosition))
        {
            targetPosition = Random.Range(maxLeft, maxRight);
        }
        if(Random.value<changeFrekans)
        {
            movingRight = !movingRight;
            targetPosition= movingRight ?maxRight :maxLeft;
        }
    }
}
