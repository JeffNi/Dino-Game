using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomGeneration : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform location;
    public GameObject[] next;
    private int num;
    void Start()
    {
        //Clones a random object in the array
        num = Random.Range(0, next.Length);
        Instantiate(next[num], location);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
