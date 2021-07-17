using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleEffect : MonoBehaviour
{

    public GameObject child1, child2;
    public ParticleSystem p1, p2;

    private void Start()
    {
        p1 = child1.GetComponent<ParticleSystem>();
        p2 = child2.GetComponent<ParticleSystem>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            child1.SetActive(true);
            child2.SetActive(true);
            p1.Play();
            p2.Play();
            p1.Play();
            p2.Play();
            //foreach (Transform child in transform)
            //{
            //    child.gameObject.SetActive(true);
            //}
        }
    }
}
