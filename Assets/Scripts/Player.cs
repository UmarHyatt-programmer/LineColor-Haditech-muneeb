﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public GameObject cube;
    public PathCreator pathCreator;
    public float speed;
    float distanceTraveled;
    public EndOfPathInstruction endOfPathInstruction;
    public Slider slider;
    public Text percentComplete;
    [Header("Materials")]
    public Material[] toAssign, cubeMats;
    [Header("RoadMesh")]
    public GameObject[] mats;

    bool stop = false;

    // Start is called before the first frame update
    void Start()
    {

        pathCreator = GameObject.FindGameObjectWithTag("PlayerPath").gameObject.GetComponent<PathCreator>();
        //Debug.Log(pathCreator.bezierPath.NumAnchorPoints);
        slider.maxValue = pathCreator.path.length;

        if (PlayerPrefs.GetInt("ColorAssign") > 5)
        {
            PlayerPrefs.SetInt("ColorAssign", 1);
        }
        mats[PlayerPrefs.GetInt("LevelNo") - 1].GetComponent<Renderer>().material = toAssign[PlayerPrefs.GetInt("ColorAssign") - 1];
        cube.GetComponent<Renderer>().material = cubeMats[PlayerPrefs.GetInt("ColorAssign") - 1];
        toAssign[PlayerPrefs.GetInt("LevelNo") - 1].SetFloat("_Fill", 0);
        percentComplete.enabled = false;
        transform.position = pathCreator.path.GetPointAtDistance(distanceTraveled, endOfPathInstruction);
        transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTraveled, endOfPathInstruction);
        if (PlayerPrefs.GetInt("ColorAssign") == 1)
        {
            slider.gameObject.transform.Find("Fill Area").Find("Fill").GetComponent<Image>().color = Color.blue;
        }
        if (PlayerPrefs.GetInt("ColorAssign") == 2)
        {
            slider.gameObject.transform.Find("Fill Area").Find("Fill").GetComponent<Image>().color = Color.green;
        }
        if (PlayerPrefs.GetInt("ColorAssign") == 3)
        {
            slider.gameObject.transform.Find("Fill Area").Find("Fill").GetComponent<Image>().color = Color.magenta;
        }
        if (PlayerPrefs.GetInt("ColorAssign") == 4)
        {
            slider.gameObject.transform.Find("Fill Area").Find("Fill").GetComponent<Image>().color = Color.red;
        }
        if (PlayerPrefs.GetInt("ColorAssign") == 5)
        {
            slider.gameObject.transform.Find("Fill Area").Find("Fill").GetComponent<Image>().color = Color.yellow;
        }
    }
    void AndroidMovement()
    {
        if (distanceTraveled <= pathCreator.path.length)
        {
            distanceTraveled += speed * Time.deltaTime;


            transform.position = pathCreator.path.GetPointAtDistance(distanceTraveled, endOfPathInstruction);
            transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTraveled, endOfPathInstruction);
            slider.value = distanceTraveled;
            percentComplete.text = ((int)(distanceTraveled / slider.maxValue * 100)).ToString() + "% Complete";
            toAssign[PlayerPrefs.GetInt("LevelNo") - 1].SetFloat("_Fill", distanceTraveled / slider.maxValue);
        }
        else
        {
            stop = false;
            UiManager.Instance.OnLevelComplete();


        }
    }
    void PcMovement()
    {
        if (distanceTraveled <= pathCreator.path.length)
        {
            if (speed < 4.5f)
            {
                speed += 0.1f;

            }

            distanceTraveled += speed * Time.deltaTime;
            transform.position = pathCreator.path.GetPointAtDistance(distanceTraveled, endOfPathInstruction);
            transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTraveled, endOfPathInstruction);
            slider.value = distanceTraveled;
            percentComplete.text = ((int)(distanceTraveled / slider.maxValue * 100)).ToString() + "% Complete";
            toAssign[PlayerPrefs.GetInt("LevelNo") - 1].SetFloat("_Fill", distanceTraveled / slider.maxValue);
        }
        else
        {
            stop = false;
            UiManager.Instance.OnLevelComplete();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (stop)
        {
            if (Application.platform == RuntimePlatform.Android)
            {
                if (Input.touchCount > 0&& Input.touchCount<2)
                {
                        Debug.Log("pc movement");
                    PcMovement();
                }
                else
                {
                    speed = 2.5f;
                }
            }

            if (Application.platform == RuntimePlatform.WindowsEditor)
            {
                if (Input.anyKey)
                {
                    Debug.Log("pc movement");
                    AndroidMovement();
                    //PcMovement();
                }
                else
                {
                    speed = 2.5f;
                }
            }

        }

    }


    #region TriggerDetection
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hurdle"))
        {
            stop = false;
            UiManager.Instance.OnContinuePlaying();
            this.gameObject.SetActive(false);
            percentComplete.enabled = true;
        }
    }
    #endregion

    public void ContinuePlay()
    {
        stop = true;
        gameObject.GetComponent<BoxCollider>().enabled = false;
        Invoke("GetTriggerActivate", 4.0f);
    }

    void GetTriggerActivate()
    {
        gameObject.GetComponent<BoxCollider>().enabled = true;
    }

    public void ActivatePlayerMovement()
    {
        stop = true;
    }
}
