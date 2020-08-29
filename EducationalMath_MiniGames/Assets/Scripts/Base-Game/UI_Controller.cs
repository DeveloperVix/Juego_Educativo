using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_Controller : MonoBehaviour
{
    private static UI_Controller instance;
    public static UI_Controller Instance { get => instance; }

    [Header("Initial concept")]
    public TextMeshProUGUI conceptUnitTxt;
    public Image imgConcept;
    int indexInfoUnit = 0;

    [Header("Results")]
    public GameObject backgroundResults;
    public TextMeshProUGUI messageResult;

    [Header("Mini Games Elements")]
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
    public TextMeshProUGUI txtGameInstrucion;
    public TextMeshProUGUI txtGameGoal;

    public Animator backgroundLoad;

    public Animator instructionsMiniGame;

    [Header("SelectObj Elements")]
    public TextMeshProUGUI[] fraction; //0 -> numerator, 1 -> denominator, 2 -> integer

    [Header("Input fraction")]
    public GameObject inputfraction;
    public TMP_InputField[] inputFractionUI; //0 -> numerator, 1 -> denominator, 2 -> Integer

    void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        SetInfoUnit();
    }

    public void ShowResultsUnit(string theMessage)
    {
        messageResult.text = theMessage;
        backgroundResults.SetActive(true);
    }

    public void SetInfoUnit()
    {
        conceptUnitTxt.text = MiniGame_Manager.Instance.curUnit.textInstructionsMiniGames[indexInfoUnit];
        imgConcept.sprite = MiniGame_Manager.Instance.curUnit.spritesInstructionsMiniGame[indexInfoUnit];
        indexInfoUnit++;
    }

    public void UpdateGoalGame()
    {
        if (MiniGame_Manager.Instance.curMiniGame.goalGame.Length > 1)
        {
            switch (MiniGame_Manager.Instance.curUnit.unitFractionName)
            {
                case TypeUnitFractions.ProperFractions:
                    txtGameInstrucion.text = MiniGame_Manager.Instance.curMiniGame.goalGame[0];
                    break;
                case TypeUnitFractions.ImproperFractions:
                    txtGameInstrucion.text = MiniGame_Manager.Instance.curMiniGame.goalGame[1];
                    break;
                case TypeUnitFractions.MixedFractions:
                    txtGameInstrucion.text = MiniGame_Manager.Instance.curMiniGame.goalGame[2];
                    break;
            }
        }
        else
        {
            txtGameInstrucion.text = MiniGame_Manager.Instance.curMiniGame.goalGame[0];
        }
        txtGameGoal.text = MiniGame_Manager.Instance.curMiniGame.instructionGame;

        if(inputfraction.activeInHierarchy)
        {
            for (int i = 0; i < inputFractionUI.Length; i++)
            {
                inputFractionUI[i].interactable = true;
            }
        }
        instructionsMiniGame.Play("HUD_Start");
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

            MiniGame_Manager.Instance.curMiniGame.completed = true;
        }
        else
        {
            txtFeedbackAnswers.text = "Respuesta incorrecta";
            goodBadSprite.sprite = goodBadSprites[1];
            btnsFeedback[1].image.sprite = goodBadSprites[3];

            //pasar el mini juego al final de la lista de los juegos por jugar
            MiniGame_Manager.Instance.MiniGameAtEnd();
            //Suma la mitad de puntos a los puntos actuales del jugador
        }
        txtFeedbackAnswers.gameObject.SetActive(true);
        goodBadSprite.gameObject.SetActive(true);
        MiniGame_Manager.Instance.minigameState = MiniGameState.Idle;
        btnsFeedback[1].gameObject.SetActive(true);
        if(inputfraction.activeInHierarchy)
        {
            for (int i = 0; i < inputFractionUI.Length; i++)
            {
                inputFractionUI[i].interactable = false;
            }
        }
    }

    public void ResetUI()
    {
        txtFeedbackAnswers.gameObject.SetActive(false);
        goodBadSprite.gameObject.SetActive(false);
        btnsFeedback[0].gameObject.SetActive(false);
        btnsFeedback[1].gameObject.SetActive(false);
    }

    public void LoadNextMiniGame()
    {
        StartCoroutine(LoadMiniGame());
    }

    IEnumerator LoadMiniGame()
    {
        if (MiniGame_Manager.Instance.minigameState == MiniGameState.Finish)
        {
            backgroundLoad.Play("LoadMiniGame_Enter");
        }
        else
        {
            backgroundLoad.Play("LoadMiniGame_Enter");
            yield return new WaitForSeconds(0.5f);
            backgroundLoad.Play("LoadMiniGame_Exit");
        }
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
            if (MiniGame_Manager.Instance.badAnswer <= 0)
            {
                MiniGame_Manager.Instance.badAnswer = 0;
            }
        }
    }

    public void InputFraction(int index)
    {
        int answerInput = 0;
        bool isNumeric = int.TryParse(inputFractionUI[index].text, out answerInput);
        if (isNumeric)
        {
            //Debug.Log("Es un número");
            switch (index)
            {
                case 0:
                    if (answerInput == MiniGame_Manager.Instance.numerator)
                    {
                        UpdateGameCondition();
                    }
                    else
                    {
                        ChangeAnswerMiniGame();
                    }
                    break;
                case 1:
                    if (answerInput == MiniGame_Manager.Instance.denominator)
                    {
                        UpdateGameCondition();
                    }
                    else
                    {
                        ChangeAnswerMiniGame();
                    }
                    break;
                case 2:
                    if (answerInput == MiniGame_Manager.Instance.integer)
                    {
                        UpdateGameCondition();
                    }
                    else
                    {
                        ChangeAnswerMiniGame();
                    }
                    break;
            }
            if (!btnsFeedback[0].gameObject.activeInHierarchy)
            {
                btnsFeedback[0].gameObject.SetActive(true); //btn check answer
            }
        }
        else
        {
            //Debug.LogError("No es un número");
            btnsFeedback[0].gameObject.SetActive(false); //btn check answer
        }

    }
}
