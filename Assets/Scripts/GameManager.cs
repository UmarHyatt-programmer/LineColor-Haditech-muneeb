using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static bool resume = false, skipable=false;
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
                instance = new GameObject("GameManager").AddComponent<GameManager>(); //create game manager object if required
            return instance;
        }
    }

    private static GameManager instance = null;

    private void Awake()
    {
        if (instance)
        {
            DestroyImmediate(gameObject); //Delete duplicate
        }
        else
        {
            instance = this; //Make this object the only instance
            DontDestroyOnLoad(gameObject); //Set as do not destroy

            if (!PlayerPrefs.HasKey("LevelNo"))
            {
                PlayerPrefs.SetInt("LevelNo", 1);
            }
            if (!PlayerPrefs.HasKey("ColorAssign"))
            {
                PlayerPrefs.SetInt("ColorAssign", 1);
            }
            if (!PlayerPrefs.HasKey("GameComplete"))
            {
                PlayerPrefs.SetInt("GameComplete", 0);
            }


        }
    }
}
