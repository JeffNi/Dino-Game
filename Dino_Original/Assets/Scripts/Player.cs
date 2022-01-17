using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody2D rb;
    private SpriteRenderer sprite;
    public float input, accel, maxSpeed, speed, jump, slowFactor, radius, jumpTime, slopeAng, speedBonus;
    private float maxSpd, maxJump, maxAccel, jumpCounter;
    private bool grounded, wet, jumping, ascending;
    private GameObject end;
    public bool waterFruit, canControl;
    public Transform feet;
    public GameObject splash;
    public LayerMask isGround, isWater;
    public GlobalVariables global;
    
    void Start()
    {
        // Gets components
        sprite = this.GetComponent<SpriteRenderer>();
        rb = this.GetComponent<Rigidbody2D>();
        global = GameObject.Find("GlobalVariables").GetComponent<GlobalVariables>();
        // Sets max constants that won't change throughout the level
        maxJump = global.maxJump;
        // MaxSpeed is temporary max speed, changes with terrain
        maxSpd = global.maxSpd;
        maxAccel = global.maxAccel;
        // Bonus changes when player eats speed flower
        speedBonus = 1;
        // Other setup variables
        canControl = true;
    }

    void FixedUpdate()
    {
        if (Input.GetKeyDown("p"))
        {
            if (canControl)
            {
                canControl = false;
            }
            else
            {
                canControl = true;
            }
        }
        if (canControl)
        {
            // Moves character 
            input = Input.GetAxis("Horizontal");
        }
        else if (end.GetComponent<LevelComplete>().win)
        {
            input = 1;
        }

        // Accelerates when moving, deccelerates when stopping or turning
        if (input == 0) {
            speed -= 0.5f;
        } else if (grounded) {
            // Normal acceleration when jumping
            speed += accel;
        } else if (speed < 2) {
            // Slower acceleration in air up to a certain point
            speed += accel/5;
        }

        speed = Mathf.Clamp(speed, 0, maxSpeed);

        // Flips sprite based on direction of movement
        if (input > 0) {
            // transform.rotation = Quaternion.Euler(0, 0, 0);
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        } else if (input < 0) {
            // transform.rotation = Quaternion.Euler(0, 180, 0);
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x) * -1, transform.localScale.y, transform.localScale.z);
        }
        rb.velocity = new Vector2(input * speed, rb.velocity.y);
        // transform.Translate(Vector2.right * input * speed * Time.deltaTime, Space.World);
    }

    void Update() {
        // This section allows player to jump

        // Checks if player is on ground or in water
        grounded = Physics2D.OverlapCircle(feet.position, radius, isGround);
        wet = Physics2D.OverlapCircle(feet.position, radius, isWater);

        // Checks if end has been instantiated yet
        if (end == null)
        {
            try 
            {
                end = GameObject.FindWithTag("End");
            } finally
            {
                //print("No end");
            }
        }

        // Speed, acceleration and jump height reduction in water or tar pits
        if (wet) {
            if (speed <= 0) {
                splash.SetActive(false);
            } else {
                splash.SetActive(true);
                float dir = transform.localScale.x/Mathf.Abs(transform.localScale.x);
                splash.transform.rotation = Quaternion.Euler(0.0f, 0.0f, -45f * (1-dir)); ;
            }
            if (!waterFruit) {
                maxSpeed = maxSpd / slowFactor;
                jump = maxJump / slowFactor;
                accel = maxAccel * speedBonus / slowFactor;
            }
            sprite.sortingOrder = 5;
        } else {
            splash.SetActive(false);
            // Jump doesn't return to normal until on land
            if (!jumping) {
                jump = maxJump;
            }
            accel = maxAccel * speedBonus;
            sprite.sortingOrder = 9;
        }
        if (canControl)
        {
            // Checks if player's feet are on the ground and if true and "w" pressed, jumps
            if (grounded == true && Input.GetKey(KeyCode.W)) {
                rb.velocity = new Vector2 (rb.velocity.x, jump);
                jumpCounter = jumpTime;
                jumping = true;
            }

        // Allows player to jump higher by holding jump
            if (Input.GetKey(KeyCode.W) && jumping) {
                if (jumpCounter > 0) {
                    rb.velocity = new Vector2 (rb.velocity.x, jump);
                    jumpCounter -= Time.deltaTime;
                } else {
                    jumping = false;
                }
            }
            // If player lets go of "w", player starts falling
            if (Input.GetKeyUp(KeyCode.W)) {
                jumping = false;
            }
        }

        // Slows movement based on slope angle
        if (rb.velocity.y > 0 && !jumping) {
            ascending = true;
        } else {
            ascending = false;
        }

        // Reduces max speed when ascending slopes
        if (grounded && ascending) {
            // Sends raycast to check slope of ground
            RaycastHit2D[] hits = new RaycastHit2D[2];
            int buffer = Physics2D.RaycastNonAlloc(transform.position, -Vector2.up, hits); 
            if (buffer > 1) { 
                slopeAng = Mathf.Abs(Mathf.Atan2(hits[1].normal.x, hits[1].normal.y)*Mathf.Rad2Deg); 
                maxSpeed = maxSpd / ((slopeAng/30f) + 1);
            }
        }
        
        // Returns max speed to normal
        if (!wet && !ascending && grounded) {
            maxSpeed = maxSpd * speedBonus;
        }
        
    }

    public bool getGrounded() {
        return grounded;
    }
}
