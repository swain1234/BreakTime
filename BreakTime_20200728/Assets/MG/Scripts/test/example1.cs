using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class example1 : MonoBehaviour
{
    public GameObject sprite;
    public ParticleSystem particles;


    private void Start()
    {
        StartCoroutine(waitForIt());
    }

    IEnumerator waitForIt()
    {
        yield return new WaitForSeconds(1.5f);
        exPlay();
    }

    void exPlay()
    {
        sprite.gameObject.SetActive(false);
        particles.Emit(9999);
    }
}
