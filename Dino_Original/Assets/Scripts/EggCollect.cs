using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggCollect : MonoBehaviour
{
    public int value;
    public bool super;
    private bool store;
    private bool exit;
    private bool collected;
    public GlobalVariables global;
    public PauseAndEnd end;

    // Variable for which super egg script corresponds to
    public int superID;

    // Start is called before the first frame update
    void Start()
    {
        collected = false;

        // Prevents double collecting supers
        super = global.superCollected[superID];
    }

    // Update is called once per frame
    void Update()
    {
        if (collected)
        {
            // Stores rewards of egg if collected
            if (store)
            {
                global.dna += value;
                store = false;
                
                if (super)
                {
                    global.superCollected[superID] = true;
                }
            }
            else if (exit)
            {
                if (!super)
                {
                    global.dna += value;
                    exit = false;
                }
            }
        }
    }

    void onTriggerEnter2D(Collider2D other)
    {
        if (other.tag.Equals("Player"))
        {
            collected = true;
            if (super)
            {
                end.superIDs.Add(superID);
                end.superValue.Add(value);
            } 
            else
            {
                end.value += value;
            }

            Destroy(this, 0);
        }
    }
}
