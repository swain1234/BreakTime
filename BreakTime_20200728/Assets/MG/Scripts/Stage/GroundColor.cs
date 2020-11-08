using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundColor : MonoBehaviour
{
    SpriteRenderer sr;
    Color color;
    string SpriteName;
    float duration = 1;
    float smoothness = 0.01f;

    Material material;
    bool isDissolving = false;
    float fade = 1f;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        color = GetComponent<SpriteRenderer>().color;
        SpriteName = sr.sprite.name;
        material = GetComponent<SpriteRenderer>().material;
        if(transform.name.IndexOf("Clone") != -1)
        {
            sr.sortingOrder = 1;
            StartCoroutine(Dissolve());
            isDissolving = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (isDissolving == false)
            {
                Instantiate(this, transform.position, transform.rotation);
                Invoke("ChangeSprite", 0.2f);
            }
        }
    }

    void ChangeSprite()
    {
        sr.sprite = Resources.Load(SpriteName + "_color", typeof(Sprite)) as Sprite;
    }

    IEnumerator Dissolve()
    {
        float increment = smoothness / duration;
        fade = 1f;
        while (fade >= 0f)
        {
            material.SetFloat("_Fade", fade);
            fade -= increment;
            yield return new WaitForSeconds(smoothness);
        }
        //isDissolving = false;
        //땅이 다시 검게 변하지 않으니까 필요없을듯
        yield return true;
    }

}
