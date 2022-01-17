using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    public string scene;
    private Button button;

    // Start is called before the first frame update
    void Start()
    {
        button = this.GetComponent<Button>();
        button.onClick.AddListener(Clicked);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Clicked(){
        SceneManager.LoadScene(scene, LoadSceneMode.Single);
    }
}
