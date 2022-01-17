using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBackup : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody2D rb;
    private SpriteRenderer sprite;
    public float accel, maxSpeed, speed, jump, slowFactor, radius, jumpTime, slopeAng;
    private float input, maxSpd, maxJump, maxAccel, jumpCounter;
    private bool grounded, wet, jumping, ascending;
    public Transform feet;
    public GameObject splash;
    public LayerMask isGround, isWater;
    void Start()
    {
        //Gets components
        sprite = this.GetComponent<SpriteRenderer>();
        rb = this.GetComponent<Rigidbody2D>();
        //Sets max constants that won't change throughout the level
        maxJump = jump;
        maxSpd = maxSpeed;
        maxAccel = accel;
    }

    void FixedUpdate()
    {
        //Moves character 
        input = Input.GetAxis("Horizontal");

        //Accelerates when moving, deccelerates when stopping or turning
        if (input == 0) {
            speed -= 0.5f;
        } else {
            speed += accel;
        }

        speed = Mathf.Clamp(speed, 0, maxSpeed);

        //Flips sprite based on direction of movement
        if (input > 0) {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        } else if (input < 0) {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        rb.velocity = new Vector2(input * speed, rb.velocity.y);
    }

    void Update() {
        //This section allows player to jump

        //Checks if player is on ground or in water
        grounded = Physics2D.OverlapCircle(feet.position, radius, isGround);
        wet = Physics2D.OverlapCircle(feet.position, radius, isWater);

        //Speed, acceleration and jump height reduction in water or tar pits
        if (wet) {
            if (speed <= 0) {
                splash.SetActive(false);
            } else {
                splash.SetActive(true);
            }
            maxSpeed = maxSpd / slowFactor;
            jump = maxJump / slowFactor;
            accel = maxAccel / slowFactor;
            transform.position = new Vector3 (transform.position.x, transform.position.y, -1.1f);
        } else {
            splash.SetActive(false);
            maxSpeed = maxSpd;
            //Jump doesn't return to normal until on land
            if (!jumping) {
                jump = maxJump;
            }
            accel = maxAccel;
            transform.position = new Vector3 (transform.position.x, transform.position.y, -4f);
        }

        //Checks if player's feet are on the ground and if true and "w" pressed, jumps
        if (grounded == true && Input.GetKey(KeyCode.W)) {
            rb.velocity = Vector2.up * jump;
            jumpCounter = jumpTime;
            jumping = true;
        }

        //Allows player to jump higher by holding jump
        if (Input.GetKey(KeyCode.W) && jumping) {
            if (jumpCounter > 0) {
                rb.velocity = Vector2.up * jump;
                jumpCounter -= Time.deltaTime;
            } else {
                jumping = false;
            }
        }
        //If player lets go of "w", player starts falling
        if (Input.GetKeyUp(KeyCode.W)) {
            jumping = false;
        }

        //Slows movement based on slope angle
        if (rb.velocity.y > 0) {
            ascending = true;
        } else {
            ascending = false;
        }

        //Reduces max speed when ascending slopes
        if (grounded && ascending) {

            RaycastHit2D[] hits = new RaycastHit2D[2];
            int buffer = Physics2D.RaycastNonAlloc(transform.position, -Vector2.up, hits); 
            if (buffer > 1) { 
                print(slopeAng);
                slopeAng = Mathf.Abs(Mathf.Atan2(hits[1].normal.x, hits[1].normal.y)*Mathf.Rad2Deg); 
                maxSpeed = maxSpd / (slopeAng/10);
 
            }
        } else if (!wet) {
            maxSpeed = maxSpd;
        }
    }

    public bool getGrounded() {
        return grounded;
    }
}
