﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollViewPractice : MonoBehaviour
{
    // 스크롤 뷰와 관련된 수정을 하기 위해 가지고 있는 변수
    ScrollRect scrollRect;

    // Use this for initialization
    void Start()
    {
        scrollRect = GetComponent<ScrollRect>();    // 게임 오브젝트가 가지고 있는 ScrollRect를 가져온다.

    }

    void SetContentSize()
    {
        // scrollRect.content를 통해서 Hierachy 뷰에서 봤던 Viewport 밑의 Content 게임 오브젝트에 접근할 수 있다.
        // 그리고 sizeDelta 값을 통해서 Content의 높이와 넓이를 수정할 수 있다.
        //scrollRect.content.sizeDelta = new Vector2(width, height);
    }
}