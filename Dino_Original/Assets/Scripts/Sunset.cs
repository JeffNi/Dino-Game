using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sunset : MonoBehaviour
{
    // Start is called before the first frame update
    public DoomWall script;
    void Start()
    {
        script.time = 0.1f;
    }

    // Update is called once per frame
    void Update()
    {
        if (this.transform.position.y > 2.5f) {
            this.transform.position += Vector3.down * Time.deltaTime * 2;
        }
        if (script.time < 0.8f) {
            script.time += Time.deltaTime * 0.05f;
        }
        if (script.b < 2f) {
            script.b += Time.deltaTime * 0.1f;
        }
    }
}
