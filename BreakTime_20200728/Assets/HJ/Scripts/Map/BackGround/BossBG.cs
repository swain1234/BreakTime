using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBG : MonoBehaviour
{
    public float speed = 0.5f;

    Option option;

    public int stageIndex;
    [SerializeField] int tempNum;

    // Start is called before the first frame update
    void Start()
    {
        option = FindObjectOfType<Option>();

        if (option != null)
        {
            string[] s = option.currentLevel.LevelName.Split('_');
            stageIndex = int.Parse(s[0]) - 1;
        }
        else
        {
            stageIndex = tempNum;
        }
    }

    private void FixedUpdate()
    {
        if (stageIndex == 8 || stageIndex == 9)
        {
            Vector3 currentPos = transform.position;
            Vector3 nextPos = Vector3.right * speed * Time.deltaTime;
            transform.position = currentPos + nextPos;
        }
    }
}
