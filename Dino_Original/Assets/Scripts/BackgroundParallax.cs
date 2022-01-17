using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundParallax : MonoBehaviour
{
    private float startPosX, startPosY;
    public GameObject mainCam;
    public float parallax, length;

    // Start is called before the first frame update
    void Start()
    {
        startPosX = transform.position.x;
        startPosY = transform.position.y;
        //length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float temp = (mainCam.transform.position.x * (1 - parallax));
        float distanceX = (mainCam.transform.position.x * parallax);
        float distanceY = (mainCam.transform.position.y * parallax);

        transform.position = new Vector3(startPosX + distanceX, startPosY + distanceY, transform.position.z);

        if (temp > startPosX + length)
        {
            startPosX += length;
        }
        else if (temp < startPosX - length)
        {
            startPosX -= length;
        }
    }
}
