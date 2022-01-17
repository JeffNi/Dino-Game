using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Follow : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform player;
    public bool followX, followY;
    private float x, y;
    public float z;
    //Where the camera stops following player
    public float minX, maxX;
    private Transform endRock;

    void Start()
    {

    }

    // Update is called once per frame
    //Camera follows player
    void FixedUpdate()
    {
        try 
        {
            endRock = GameObject.FindGameObjectWithTag("End").transform.Find("End").transform;
            maxX = endRock.position.x;
            //Sets x or y to be the same as the object followed, unless told not to
            if (followX) {
                x = player.position.x;
            } else {
                x = this.transform.position.x;
            }
            if (followY) {
                y = player.position.y;
            } else {
                y = this.transform.position.y;
            }
            transform.position = new Vector3(Mathf.Clamp(x, minX, maxX), y, z);
        }
        catch (Exception e)
        {
            
        }
    }
}
