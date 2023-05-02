using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleMoving : MonoBehaviour
{
    public float moveSpeed;
    public float moveDistance;
    public bool moveUp = true;

    void Update()
    {
        if (moveUp)
        {
            transform.Translate(Vector3.up * Time.deltaTime * moveSpeed);
        }
        else
        {
            transform.Translate(Vector3.down * Time.deltaTime * moveSpeed);
        }   

        if (transform.position.y >= moveDistance)
        {
            moveUp = false;
        }
        else if (transform.position.y <= 0)
        {
            moveUp = true;
        }

    }
}
