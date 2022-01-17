using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalVariables : MonoBehaviour
{
    public float maxAccel, maxSpd, maxJump;
    private GameObject end;
    public int levelID, stageID;
    public int[] levelUnlock = new int[3]; 
    private string[,] levelNames = new string[9,3];
    public int dna;

    // Variables to store whether each super egg was collected
        // 0 = raptor
    public bool[] superCollected;

    // Start is called before the first frame update
    void Start()
    {
        end = null;
        /*
        for (int i = 0; i < levelUnlock.Length; i++)
        {
            levelUnlock[i] = 1;
        }*/
        // Setting level names

    }

    // Update is called once per frame
    void Update()
    {
        if (end == null)
        {
            try 
            {
                end = GameObject.FindWithTag("End");
            } finally
            {
                //print("No end");
            }
        } else 
        {
            // Check if a new level is unlocked
            if (end.GetComponent<LevelComplete>().win == true && levelID <= levelUnlock[stageID]) 
            {
                levelUnlock[stageID] = levelID + 1;
            }
        }
    
    }

    void Awake()
    {
        DontDestroyOnLoad(this);
    }
}
