using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundColor : MonoBehaviour
{
    SpriteRenderer sr;
    string SpriteName;
    float duration = 2;
    float smoothness = 0.05f;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        SpriteName = sr.sprite.name;
        sr.color = Color.grey;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            StartCoroutine(LerpColor());
        }
    }

    IEnumerator LerpColor() // 자연스럽게 점차 색바꾸기
    {
        float progress = 0; //This float will serve as the 3rd parameter of the lerp function.
        float increment = smoothness / duration; //The amount of change to apply.
        while (progress < 0.5f)
        {
            sr.color = Color.Lerp(Color.grey, Color.white, progress);
            progress += increment;
            yield return new WaitForSeconds(smoothness);
        }
        sr.sprite = Resources.Load(SpriteName + "_color", typeof(Sprite)) as Sprite;
        while(progress < 1f)
        {
            sr.color = Color.Lerp(Color.grey, Color.white, progress);
            progress += increment;
            yield return new WaitForSeconds(smoothness);
        }
        yield return true;
    }
}
