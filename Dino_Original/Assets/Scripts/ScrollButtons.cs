using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollButtons : MonoBehaviour
{
    public bool next, prev;
    public ShowGui script;
    private GameObject front, back;
    private Color available = new Color (1, 1, 1);
    private Color unavailable = new Color (0.3f, 0.3f, 0.3f);
    private ShowGui showGui;
    // Start is called before the first frame update
    void Start()
    {
        front = GameObject.Find("Next");
        back = GameObject.Find("Prev");
        front.GetComponent<Button>().onClick.AddListener(FrontClick);
        back.GetComponent<Button>().onClick.AddListener(BackClick);

        showGui = GameObject.Find("Levels").GetComponent<ShowGui>();
    }

    // Update is called once per frame
    void Update()
    {
        if (next) {
            front.GetComponent<Image>().color = available;
        } else {
            front.GetComponent<Image>().color = unavailable;
        }

        if (prev) {
            back.GetComponent<Image>().color = available;
        } else {
            back.GetComponent<Image>().color = unavailable;
        }
    }

    //Goes to next level if available
    void FrontClick() {
        if (next) 
        {
            showGui.lvlID ++;
        }
    }

    //Goes to previous level if available
    void BackClick() {
        if (prev)
        {
            showGui.lvlID --;
        }
    }
}

