using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public static LevelLoader instance;

    public Animator transition;
    public float transitionTime = 1f;

    private void Awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(this.gameObject);
            instance = this;
        }
        else
            Destroy(this.gameObject);
    }

    public static LevelLoader Instance
    {
        get
        {
            if (null == instance)
            {
                return null;
            }
            return instance;
        }
    }


    public void LoadLevel(string levelName)
    {
        StartCoroutine(ILoadLevel(levelName));
    }

    IEnumerator ILoadLevel(string levelName)
    {
        transition.SetTrigger("Start");
        AudioManager.Instance.Play("gear");
        yield return new WaitForSeconds(transitionTime);
        transition.SetTrigger("End");
        SceneManager.LoadScene(levelName);
        yield return new WaitForSeconds(0.1f);
        AudioManager.Instance.Play("gear");
    }
}

