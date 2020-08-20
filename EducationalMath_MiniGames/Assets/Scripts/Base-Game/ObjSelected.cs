﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjSelected : MonoBehaviour
{
    public bool selected = false;
    public SpriteRenderer imgStatus;
    // Start is called before the first frame update
    void Start()
    {
    }

    private void OnMouseDown()
    {
        if (MiniGame_SelectObj.Instace.minigameState != MiniGameState.Playing)
            return;

        Debug.Log("Tap en :" + gameObject.name);
        if(!selected)
        {
            imgStatus.color = Color.red;
            MiniGame_SelectObj.Instace.UpdateGameCondition();
            selected = true;
        }
        else
        {
            imgStatus.color = Color.white;
            MiniGame_SelectObj.Instace.ChangeAnswer();
            selected = false;
        }
    }
}