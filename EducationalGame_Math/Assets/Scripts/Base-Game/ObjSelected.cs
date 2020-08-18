using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjSelected : MonoBehaviour
{
    bool selected = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnMouseDown()
    {
        Debug.Log("Tap en :" + gameObject.name);
        if(!selected)
        {
            MiniGame_Elements.Instace.UpdateGameCondition();
        }
        else
        {
            MiniGame_Elements.Instace.ChangeAnswer();
        }
    }
}
