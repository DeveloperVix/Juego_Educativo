using System.Collections;
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
        if (MiniGame_Elements.Instace.minigameState != MiniGameState.Playing)
            return;

        Debug.Log("Tap en :" + gameObject.name);
        if(!selected)
        {
            imgStatus.color = Color.red;
            MiniGame_Elements.Instace.UpdateGameCondition();
            selected = true;
        }
        else
        {
            imgStatus.color = Color.white;
            MiniGame_Elements.Instace.ChangeAnswer();
            selected = false;
        }
    }
}
