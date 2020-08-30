using System.Collections;
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

    public GameObject tapInstruction;

    [Header("When the fraction is drag and drop")]
    public bool dragDrop = false;
    public bool correctAnswer = false;

    Vector3 originPosition;

    Vector3 screenPoint;
    Vector3 offset;


    //Call this method when initialize the mini game "Select fraction"
    public void SetFractionTxt(int integer, int numerator, int denominator, bool canDragDrop, bool showTap)
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

        if (canDragDrop)
        {
            dragDrop = true;
            originPosition = transform.position;
        }
        else if(showTap)
        {
            StartCoroutine(HideInstruction());
        }
    }

    IEnumerator HideInstruction()
    {
        yield return new WaitForSeconds(2f);
        tapInstruction.SetActive(false);
    }

    public override void OnMouseDown()
    {
        if (MiniGame_Manager.Instance.minigameState != MiniGameState.Playing)
            return;
        else if (dragDrop)
        {
            //Convierte las coordenadas del objeto seleecionado a coordenadas de pantalla
            screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
            //Compensa la posicion del objeto respecto a la posicion del mouse en pantalla
            offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
            imgStatus.color = Color.red;
            integerSprite.color = Color.red;
            return;
        }


        if (!selected)
        {
            imgStatus.color = Color.red;
            integerSprite.color = Color.red;
            UI_Controller.Instance.UpdateGameCondition(typeFractionToSelect);
            selected = true;
        }
        else if (selected)
        {
            imgStatus.color = Color.white;
            integerSprite.color = Color.white;
            UI_Controller.Instance.ChangeAnswerMiniGame(typeFractionToSelect);
            selected = false;
        }
    }
    private void OnMouseDrag()
    {
        if (dragDrop)
        {
            //La posicion actual del mouse 
            Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
            //La posicion a la que se movera el objeto, convirtiendo la posicion actual del mouse a coordenadas de espacio, se le suma la compensación
            Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
            //Se asigna la posicion al objeto
            transform.position = curPosition;
            //Si el usuario lleva la fraccion fuera de los limites de pantalla, regresa el objeto a su posición original
            if (transform.position.x > MiniGame_Manager.Instance.width || transform.position.x < MiniGame_Manager.Instance.width * -1 ||
            transform.position.y > MiniGame_Manager.Instance.height / 2 || transform.position.y < (MiniGame_Manager.Instance.height / 2) * -1)
            {
                transform.position = originPosition;
                imgStatus.color = Color.white;
                integerSprite.color = Color.white;
            }
        }
    }

    private void OnMouseUp()
    {
        if (dragDrop)
        {
            imgStatus.color = Color.white;
            integerSprite.color = Color.white;
        }
    }
}
