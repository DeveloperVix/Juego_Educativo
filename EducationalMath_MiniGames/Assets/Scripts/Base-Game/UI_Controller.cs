using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_Controller : MonoBehaviour
{
    private static UI_Controller instance;
    public static UI_Controller Instance{get => instance;}

    /*
     * 0 -> correct checkmark
     * 1 -> wrong cross
     * 2 -> btn correct
     * 3 -> btn wrong
     */
    public Image goodBadSprite;
    public Sprite[] goodBadSprites;
    /*
    0 -> btn check answer
    1 -> btn next mini game 
    */
    public Button[] btnsFeedback;
    public TextMeshProUGUI txtFeedbackAnswers;
    public TextMeshProUGUI txtGoalGame;

    [Header("SelectObj Elements")]
    public TextMeshProUGUI[] fraction; //0 -> numerator, 1 -> denominator, 2 -> integer

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    public void UpdateGoalGame()
    {
        if (MiniGame_Manager.Instance.curMiniGame.goalGame.Length > 1)
        {
            switch (MiniGame_Manager.Instance.curUnit.unitFractionName)
            {
                case TypeUnitFractions.ProperFractions:
                    txtGoalGame.text = MiniGame_Manager.Instance.curMiniGame.goalGame[0];
                    break;
                case TypeUnitFractions.ImproperFractions:
                    txtGoalGame.text = MiniGame_Manager.Instance.curMiniGame.goalGame[1];
                    break;
                case TypeUnitFractions.MixedFractions:
                    txtGoalGame.text = MiniGame_Manager.Instance.curMiniGame.goalGame[2];
                    break;
            }
        }
        else
        {
            txtGoalGame.text = MiniGame_Manager.Instance.curMiniGame.goalGame[0];
        }
    }

    public void CheckAnswer()
    {
        if (MiniGame_Manager.Instance.curHits == MiniGame_Manager.Instance.totalHits
        && MiniGame_Manager.Instance.badAnswer == 0)
        {
            txtFeedbackAnswers.text = "Respuesta correcta";
            goodBadSprite.sprite = goodBadSprites[0];
            btnsFeedback[1].image.sprite = goodBadSprites[2];

            //Suma puntos completos a los puntos actuales del jugador
        }
        else
        {
            txtFeedbackAnswers.text = "Respuesta incorrecta";
            goodBadSprite.sprite = goodBadSprites[1];
            btnsFeedback[1].image.sprite = goodBadSprites[3];

            //pasar el mini juego al final de la lista de los juegos por jugar
            //Suma la mitad de puntos a los puntos actuales del jugador
        }
        txtFeedbackAnswers.gameObject.SetActive(true);
        goodBadSprite.gameObject.SetActive(true);
        MiniGame_Manager.Instance.minigameState = MiniGameState.Idle;
        btnsFeedback[1].gameObject.SetActive(true);
    }

    //The method is called when the player choose an object or an answer
    public void UpdateGameCondition()
    {
        //Increase if the answer is correct
        MiniGame_Manager.Instance.curHits++;
        if (!btnsFeedback[0].gameObject.activeInHierarchy)
        {
            btnsFeedback[0].gameObject.SetActive(true); //btn check answer
        }
    }

    public void UpdateGameCondition(TypeUnitFractions objFraction)
    {
        if (objFraction == MiniGame_Manager.Instance.curUnit.unitFractionName)
        {
            UpdateGameCondition();
        }
        else
        {
            MiniGame_Manager.Instance.badAnswer++;
        }
        
        if (!btnsFeedback[0].gameObject.activeInHierarchy)
        {
            btnsFeedback[0].gameObject.SetActive(true); //btn check answer
        }
    }

    public void ChangeAnswerMiniGame()
    {
        //if the player changes his answer by reselecting a previously selected object
        MiniGame_Manager.Instance.curHits--;
        if (MiniGame_Manager.Instance.curHits <= 0)
        {
            MiniGame_Manager.Instance.curHits = 0;
            btnsFeedback[0].gameObject.SetActive(false);
        }
    }

    public void ChangeAnswerMiniGame(TypeUnitFractions objFraction)
    {
        if (objFraction == MiniGame_Manager.Instance.curUnit.unitFractionName)
        {
            ChangeAnswerMiniGame();
        }
        else
        {
            MiniGame_Manager.Instance.badAnswer--;
            if(MiniGame_Manager.Instance.badAnswer <= 0)
            {
                MiniGame_Manager.Instance.badAnswer = 0;
            }
        }
    }
}
