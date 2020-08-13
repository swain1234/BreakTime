using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestBookControl : MonoBehaviour
{
    [SerializeField] Book book;
    Image image;

    // Update is called once per frame
    void Update()
    {
        //if(Input.GetKeyDown("z"))
        //{
        //    //book.rectTransform.sizeDelta = new Vector2(upImage.rectTransform.sizeDelta.x, progress);
        //    //book.gameObject.transform.localScale = new Vector2(3, 3);
        //    StartCoroutine(ChangeBookScale());    

        //    //book.gameObject.transform.position = new Vector2(book.transform.position.x - 100, book.transform.position.y);
        //}
        //if(Input.GetKeyDown("x"))
        //{
        //    ResetBookScale();
        //}
        
    }
    IEnumerator ChangeBookScale()
    {
        float progress = 0;
        float up = 0.01f;
        while (progress < 0.3f)
        {
            book.gameObject.transform.localScale -= new Vector3(up, up, 0);
            progress += up;
            yield return new WaitForSeconds(0.05f);
        }
        yield return true;
    }
    void ResetBookScale()
    {
        book.gameObject.transform.localScale = new Vector2(1, 1);
    }
}
