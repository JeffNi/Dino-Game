using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PauseAndEnd : MonoBehaviour
{
    private Canvas menu;
    private Color col;
    private GameObject end;
    public GlobalVariables global;
    public GameObject next;
    public TextMeshProUGUI extinct;
    public DoomWall doomWall;
    public int value;
    public string ID;
    public List<int> superIDs = new List<int>();
    public List<int> superValue = new List<int>();
    // Start is called before the first frame update
    void Start()
    {
        menu = this.gameObject.GetComponent<Canvas>();
        next.SetActive(false);
        col = extinct.color;
        col.a = 0;
        extinct.color = col;
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
        } 
        else if (ID.Equals("End") && end.GetComponent<LevelComplete>().win == true) 
        {
            next.SetActive(true);
            for (int i = 0; i < superIDs.Count; i++)
            {
                global.superCollected[superIDs[i]] = true;
                global.dna += superValue.IndexOf(i);
            }
        }
        else if (doomWall.lose == true)
        {
            next.SetActive(true);
            col.a += 0.002f;
            extinct.color = col;
        }

    }
}
