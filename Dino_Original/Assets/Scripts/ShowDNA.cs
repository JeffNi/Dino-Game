using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowDNA : MonoBehaviour
{
    private GlobalVariables global;
    private Text txt;

    // Start is called before the first frame update
    void Start()
    {
        txt = this.GetComponent<Text>();
        global = GameObject.Find("Global Variables").GetComponent<GlobalVariables>();
    }

    // Update is called once per frame
    void Update()
    {
        //txt.text = global.dna.ToString();
    }
}
