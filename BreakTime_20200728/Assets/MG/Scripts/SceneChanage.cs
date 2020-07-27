using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanage : MonoBehaviour
{
    static public SceneChanage instance;

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



    public virtual void ChangeScene(string s)
    {
        SceneManager.LoadScene(s);
    }
}
