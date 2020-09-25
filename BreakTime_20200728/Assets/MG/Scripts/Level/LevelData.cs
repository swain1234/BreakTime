using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "Data/LevelData", order = 1)]
public class LevelData : ScriptableObject
{

    [SerializeField]
    private string levelName;
    public string LevelName
    {
        get { return levelName; }
        set
        {
            levelName = value;
        }
    }

    [SerializeField]
    [TextArea(3, 10)]
    private string script;
    public string Script
    {
        get { return script; }
        set
        {
            script = value;
        }
    }

    [SerializeField]
    private Sprite icon;
    public Sprite Icon
    {
        get { return icon; }
        set
        {
            icon = value;
        }
    }

}
