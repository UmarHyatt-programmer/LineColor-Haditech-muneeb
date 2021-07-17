using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneScript : MonoBehaviour
{

    //public string sceneName;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Loading());
    }

    IEnumerator Loading()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(1);
    }
}
