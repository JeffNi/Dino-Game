using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelComplete : MonoBehaviour
{
    private GameObject player;
    private Player moveScript;
    public bool win;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        moveScript = player.GetComponent<Player>();
        win = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (win)
        {
            moveScript.input = 1;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player") {
            moveScript.canControl = false;
            win = true;
        }
    }
}
