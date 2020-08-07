﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EasyMobile;

public class Init_EasyMobile : MonoBehaviour
{
    private void Awake()
    {
        if (!RuntimeManager.IsInitialized())
            RuntimeManager.Init();
    }
}
