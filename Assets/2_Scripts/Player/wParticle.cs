using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wParticle : MonoBehaviour
{

    ParticleSystem particle;


    private void Awake()
    {
        particle = GetComponent<ParticleSystem>();
    }



    private void OnEnable()
    {
        particle.Play();

        StartCoroutine(ParticleRoutien());  
    }

    IEnumerator ParticleRoutien()
    {
        yield return new WaitForSeconds(8);
        gameObject.SetActive(false);
    }
}
