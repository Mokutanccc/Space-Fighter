using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static float height = 7.25f;
    public static float width = 4.25f;
    public float plyaerSpeed;

    void FixedUpdate()
    {
        Vector3 pos = transform.position;
        pos.y += Input.GetAxis("Vertical") * plyaerSpeed;
        pos.x += Input.GetAxis("Horizontal") * plyaerSpeed;
        if (pos.y < 5.75f)
        {
            pos.y = 5.75f;
        }
        if (pos.y > height)
        {
            pos.y = height;
        }
        if (pos.x > width)
        {
            pos.x = width;
        }
        if (pos.x < 0.75f)
        {
            pos.x = 0.75f;
        }
        transform.position = pos;
    }
}
