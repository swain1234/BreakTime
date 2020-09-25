﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalVolume : MonoBehaviour
{
    static public GlobalVolume instance;

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
}
