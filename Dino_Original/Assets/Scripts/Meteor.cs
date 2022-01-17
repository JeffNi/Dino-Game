using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

public class Meteor : MonoBehaviour
{
    private float timer;
    private float expTime;
    private float speed;
    private bool exploded;
    public Flash flash;

    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
        expTime = 2f;
        speed = 20;
        exploded = false;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        transform.Translate(Vector3.left * speed * Time.deltaTime, Space.World);
        transform.Translate(Vector3.down * speed * 0.5f * Time.deltaTime, Space.World);

        if (timer > expTime)
        {
            if (!exploded)
            {
                CameraShaker.Instance.ShakeOnce(15f, 20f, 0.1f, 5f);
                flash.Strike(1f);
                exploded = true;
            }
            if (timer > expTime + 5)
            {
                Destroy(GameObject.Find("Meteor"), 0);
            }
        }
    }
}
