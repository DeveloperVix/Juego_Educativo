using UnityEngine;

public class BaseObjInteractable : MonoBehaviour
{
    public bool isInteractable = true;
    public bool selected = false;
    public SpriteRenderer imgStatus;

    public Color pieceSelected;

    public virtual void OnMouseDown() 
    {
        if(MiniGame_Manager.Instance.minigameState != MiniGameState.Playing)
            return;
        //Debug.Log("Tap en :" + gameObject.name);
        if(!isInteractable)
            return;

        if(!selected)
        {
            imgStatus.color = pieceSelected;
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

    public virtual void SetObjNotInteractable(bool selected)
    {
        isInteractable = false;
        if(selected)
        {
            imgStatus.color = pieceSelected;
        }
    }
}
