using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMaterail : MonoBehaviour
{

    public Material[] mats;
    // Start is called before the first frame update
    void Start()
    {
        
        GetComponent<Renderer>().material = mats[PlayerPrefs.GetInt("ColorAssign")-1];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
