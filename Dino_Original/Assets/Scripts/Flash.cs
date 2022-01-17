using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flash : MonoBehaviour
{
    SpriteRenderer rend;
    Color col;
    public float mistStrength;
    public float incSpeed;
    private bool doMist;
    private bool unMist;
    // Start is called before the first frame update
    void Start()
    {
        rend = this.GetComponent<SpriteRenderer>();
        col = rend.color; 
        doMist = false;
        unMist = false;
    }

    // Update is called once per frame
    void Update()
    {
        Mist();
        DeMist();
    }
    
    // Makes flash semi-transparent to simulate fog effect
    void Mist() 
    {
        if (doMist)
        {
            if (col.a < mistStrength)
            {
                col.a += incSpeed * Time.deltaTime;
            }
            else
            {
                col.a = mistStrength;
                doMist = false;
            }
            col.a = Mathf.Clamp(col.a, 0, mistStrength);
            rend.color = col;
        }
    }

    // Makes flash transparent
    void DeMist() {
        //print(rend.color);
        if (unMist)
        {
            if (col.a > 0)
            {
                col.a -= incSpeed * Time.deltaTime;
            }
            else
            {
                col.a = 0;
                unMist = false;
            }
            col.a = Mathf.Clamp(col.a, 0, mistStrength);
            rend.color = col;
        }
    }

    // Makes flash quickly opaque then transparent again to simulate lightning or meteor
    public void Strike(float fadeSpeed)
    {
        col.a = 1f;
        incSpeed = fadeSpeed;
        rend.color = col;
        unMist = true;
    }
}
