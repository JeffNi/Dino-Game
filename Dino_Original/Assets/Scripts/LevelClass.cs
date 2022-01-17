using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelClass : MonoBehaviour
{
    private int levelID, stageID;
    private string lvlName, image;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Getters
    public int getLevelID()
    {
        return this.levelID;
    }

    public int getStageID()
    {
        return this.stageID;
    }

    public string getLvlName()
    {
        return this.lvlName;
    }
}
