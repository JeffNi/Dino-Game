using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedFlower : MonoBehaviour
{
    private GameObject playerEffect;
    private Player movement;
    private float timer, powerSeconds;
    // Start is called before the first frame update
    void Start()
    {
        powerSeconds = 10;
        timer = 0;
        movement = gameObject.GetComponent<Player>();
        playerEffect = GameObject.Find("Speed Flower Effect");
    }

    // Update is called once per frame
    void Update()
    {
        //If power is active displays particles and activates effects
        if (timer > 0) {
            timer -= Time.deltaTime;
            playerEffect.SetActive(true);
            movement.speedBonus = 1.3f;
        } else {
            //Disables effects after power wares off
            playerEffect.SetActive(false);
            movement.speedBonus = 1f;
        }

    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other != null) {
            //If player collides with powerup activates power for certain amount of time
            if (other.CompareTag("SpeedFlower")) {
                timer = powerSeconds;
                other.gameObject.SetActive(false);
            }
        }
     }
}
