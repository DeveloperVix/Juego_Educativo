using UnityEngine;

public class BaseObjInteractable : MonoBehaviour
{
    public bool selected = false;
    public SpriteRenderer imgStatus;

    public virtual void OnMouseDown() 
    {
        Debug.Log("Tap en :" + gameObject.name);
        if(!selected)
        {
            imgStatus.color = Color.red;
            MiniGame_Manager.Instace.UpdateGameCondition();
            selected = true;
        }
        else
        {
            imgStatus.color = Color.white;
            MiniGame_Manager.Instace.ChangeAnswer();
            selected = false;
        }
    }
}
