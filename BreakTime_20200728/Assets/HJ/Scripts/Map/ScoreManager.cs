using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    static int score = 0;
    static bool isGet = false;

    public GameObject candy;

    public static void setCandy(int value)
    {
        score += value;
        isGet = true;
    }

    public static bool getCandy()
    {
        return isGet;
    }
}
