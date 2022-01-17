using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class ShowGui : MonoBehaviour
{
    //Allow it to determine what Gui looks like
    private GameObject gui;
    private TextMeshProUGUI lvlName;
    private Image image;
    //Allows script to determine if color of buttons so player knows which level is first and last
    public ScrollButtons nextPrev;
    //Buttons
    private Button levelOne;
    private Button close;
    private Button run;
    private Button front, back;
    //Keeps track of current level and stage
    public int lvlID, stageID;
    private GlobalVariables global;

    //Scene name arrays
    public List<string> grasslands = new List<string>();

    //Image arrays
    public Sprite[] grass;

    // Start is called before the first frame update
    void Start()
    {
        gui = GameObject.Find("Gui");
        image = GameObject.Find("Image").GetComponent<Image>();
        lvlName = GameObject.Find("Level Name").GetComponent<TextMeshProUGUI>();
        close = GameObject.Find("Back").GetComponent<Button>();
        run = GameObject.Find("Run").GetComponent<Button>();
        gui.SetActive(false);
        levelOne = GameObject.Find("Level 1").GetComponent<Button>();
        levelOne.onClick.AddListener(Clicked);
        close.onClick.AddListener(Closed);
        run.onClick.AddListener(RunClicked);

        try 
        {
            global = GameObject.Find("GlobalVariables").GetComponent<GlobalVariables>();
        } finally
        {
            //print("Global Variables Null");
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Changes if next/prev buttons appear prominately
        if (grasslands.IndexOf(lvlName.text) < global.levelUnlock[stageID]) {
            nextPrev.next = true;
        } else {
            nextPrev.next = false;
        }

        if (grasslands.IndexOf(lvlName.text) != 0) {
            nextPrev.prev = true;
        } else {
            nextPrev.prev = false;
        }
 
        switch(stageID)
        {
            case 0:
                lvlName.SetText(grasslands[lvlID]);
                image.sprite = grass[lvlID]; 
                break;
            default:
                break;
        }
    }

    void Clicked() {
        gui.SetActive(true);
        lvlName.SetText(grasslands[0]);
        image.sprite = grass[0]; 
        stageID = 0;
        lvlID = 0;
    }

    void Closed() {
        gui.SetActive(false);
    }

    void RunClicked() {
        global.levelID = lvlID;
        global.stageID = this.stageID;
        SceneManager.LoadScene(lvlName.text, LoadSceneMode.Single);
    }

}

