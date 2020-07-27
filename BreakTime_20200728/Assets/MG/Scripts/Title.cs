using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Title : MonoBehaviour
{
    private SceneChanage sceneChange;

    [SerializeField]
    private string nextScene;

    private void Start()
    {
        sceneChange = FindObjectOfType<SceneChanage>();
    }

    private void Update()
    {
        if(Input.anyKeyDown)
        {
            sceneChange.ChangeScene(nextScene);
        }
    }
}
