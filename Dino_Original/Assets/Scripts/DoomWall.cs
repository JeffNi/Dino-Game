using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoomWall : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform player;
    private Color col, coverCol, playerCol;
    public SpriteRenderer cover, playerSprite;
    private float x; 
    public float r, g, b;
    public float speed, time;
    public bool lose;
    public AudioSource music;
    private AudioSource rumble;
    void Start()
    {
        r = RenderSettings.ambientLight.r;
        g = RenderSettings.ambientLight.g;
        b = RenderSettings.ambientLight.b;
        col = new Color(r, g, b);
        coverCol = cover.material.color;
        playerCol = playerSprite.material.color;
        x = 0;
        time = 0;
        rumble = gameObject.GetComponent<AudioSource>();
        lose = false;
    }

    // Update is called once per frame
    void Update()
    {
        if ((player.position.x - this.transform.position.x) < -15) {
            lose = true;
        }
        if (lose)
        {
           rumble.volume -= 0.005f; 
        } 
        else
        {
            rumble.volume = 1 - music.volume;
        }
        //Changes color of screen based on proximity to doom wall
        x = Mathf.Clamp(Mathf.Clamp((player.position.x - this.transform.position.x)/5 - 1.5f, 0, 1) - time, 0, 1);
        col = new Color(Mathf.Clamp(r * x + 0.2f, 0, 1), x * g, x * b);
        RenderSettings.ambientLight = col;

        //Doom cover becomes less transparent when close (turns sky red instead of black)
        coverCol.a = Mathf.Clamp(1 - (r * x), 0, 0.9f);
        cover.material.color = coverCol;

        //Player turns black when near
        playerCol = new Color(x * r, x * g, x * b);
        playerSprite.material.color = playerCol;

        //Moves forward
        if (!lose)
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        }
        else
        {
            transform.position = new Vector3(player.position.x + 20, player.position.y, player.position.z);
        }

        //Music turns to rumble when near
        music.volume = Mathf.Clamp((player.position.x - this.transform.position.x)/20, 0, 1);
    }
}
