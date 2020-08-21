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
            UI_Controller.Instance.UpdateGameCondition();
            selected = true;
        }
        else
        {
            imgStatus.color = Color.white;
            UI_Controller.Instance.ChangeAnswerMiniGame();
            selected = false;
        }
    }
}
