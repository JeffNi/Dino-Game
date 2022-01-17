using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    Animator animator;
    Player player;
    public bool running;
    public bool jumping;
    public bool falling;
    public bool idle;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        player = this.GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        //Sets the animator variables to script variables
        animator.SetBool("Running", running);
        animator.SetBool("Jumping", jumping);
        animator.SetBool("Idle", idle);
        animator.SetBool("Falling", falling);

        //Conditions for script variables
        running = Input.GetAxis("Horizontal") != 0 && !jumping;
        jumping = !player.getGrounded() && !falling;
        idle = !running && !jumping && !falling;
    }
}
