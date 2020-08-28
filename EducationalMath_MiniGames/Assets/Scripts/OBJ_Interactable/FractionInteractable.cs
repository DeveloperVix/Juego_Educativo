using TMPro;
using UnityEngine;

public class FractionInteractable : BaseObjInteractable
{
    public SpriteRenderer integerSprite;
    public TypeUnitFractions typeFractionToSelect;

    public TextMeshPro numeratorTxt;
    public TextMeshPro denominatorTxt;
    public TextMeshPro integerTxt;
    public GameObject integerFeedbak;

    [Header("When the fraction is drag and drop")]
    public bool dragDrop = false;
    public bool correctAnswer = false;


    //Call this method when initialize the mini game "Select fraction"
    public void SetFractionTxt(int integer, int numerator, int denominator, bool canDragDrop)
    {
        if (integer != 0)
        {
            integerFeedbak.SetActive(true);
            integerTxt.text = "" + integer;
        }
        else
            integerFeedbak.SetActive(false);
        
        denominatorTxt.text = "" + denominator;
        numeratorTxt.text = "" + numerator;

        if(canDragDrop)
            dragDrop = true;
    }

    public override void OnMouseDown()
    {
        if (MiniGame_Manager.Instance.minigameState != MiniGameState.Playing)
            return;
        else if(dragDrop)
            return;
            
        if(!selected)
        {
            imgStatus.color = Color.red;
            integerSprite.color = Color.red;
            UI_Controller.Instance.UpdateGameCondition(typeFractionToSelect);
            selected = true;
        }
        else if(selected)
        {
            imgStatus.color = Color.white;
            integerSprite.color = Color.white;
            UI_Controller.Instance.ChangeAnswerMiniGame(typeFractionToSelect);
            selected = false;
        }
    }
}
