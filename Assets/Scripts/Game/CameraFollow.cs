using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform lookAt;

    public float boundX;
    public float boundY;
    public float speed;

    Vector3 desiredPosition;

    void LateUpdate()
    {
        Vector3 delta = Vector3.zero;

        float dx = lookAt.position.x - transform.position.x;

        if(dx > boundX || dx < -boundX)
        {
            if(transform.position.x < lookAt.position.x)
            {
                delta.x = dx - boundX;
            }
            else
            {
                delta.x = dx + boundX;
            }
        }




        float dy = lookAt.position.y - transform.position.y;

        if (dy > boundY || dy < -boundY)
        {
            if (transform.position.y < lookAt.position.y)
            {
                delta.y = dy - boundY;
            }
            else
            {
                delta.y = dy + boundY;
            }
        }


        desiredPosition = transform.position + delta;
        transform.position = Vector3.Lerp(transform.position, desiredPosition, speed);


    }
}
