using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public enum MiniGameState { Pause, Idle, Playing }

public class MiniGame_Manager : MonoBehaviour
{
    private static MiniGame_Manager instace;
    public static MiniGame_Manager Instace { get => instace; }

    public MiniGameState minigameState;

    public UnitElementsScriptable curUnit; //The unit to play
    public SO_BaseMiniGames curMiniGame;

    public int numerator = 1;
    public int denominator = 0;

    [Header("Camera's dimension")]
    public float height = 0f;
    public float width = 0;
    Camera cam;

    [Header("Mini games elements")]
    public int totalHits = 0;
    [HideInInspector]
    public int curHits = 0;
    /*
     * 0 -> correct
     * 1 -> wrong
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

    //Delegates
    public delegate void MiniGameStructure(TypeUnitFractions theUnit);
    MiniGameStructure InitMiniGame;
    public delegate void MiniGameUpdateAnswer(TypeUnitFractions theUnit);
    MiniGameUpdateAnswer UpdateAnswer;
    public delegate void MiniGameChangeAnswer(TypeUnitFractions theUnit);
    MiniGameChangeAnswer ChangeAnswer;

    [Header("SelectObj Elements")]
    public TextMeshProUGUI[] fraction; //0 -> numerator, 1 -> denominator

    // Start is called before the first frame update
    void Start()
    {
        instace = this;

        cam = Camera.main;
        //The width and heigth of the camera
        height = 2f * cam.orthographicSize;
        width = cam.aspect * 2f * cam.orthographicSize / 2f - 1.9f;
        width = (float)(Math.Round((double)width, 1));

        InitMiniGame += curMiniGame.InitGame;
        InitMiniGame += curMiniGame.GenerateGameElement;

        UpdateAnswer = curMiniGame.UpdateGameCondition;
        ChangeAnswer = curMiniGame.ChangeAnswer;

        InitMiniGame(curUnit.unitFractionName);
        UpdateGoalGame();
    }

    void UpdateGoalGame()
    {
        if (curMiniGame.goalGame.Length > 1)
        {
            switch (curUnit.unitFractionName)
            {
                case TypeUnitFractions.ProperFractions:
                    txtGoalGame.text = curMiniGame.goalGame[0];
            break;
                case TypeUnitFractions.ImproperFractions:
                    txtGoalGame.text = curMiniGame.goalGame[1];
                    break;
                case TypeUnitFractions.MixedFractions:
                    txtGoalGame.text = curMiniGame.goalGame[2];
                    break;
            }
        }
        else
        {
            txtGoalGame.text = curMiniGame.goalGame[0];
        }
    }

    public void CheckAnswer()
    {
        if (curHits == totalHits)
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
        minigameState = MiniGameState.Idle;
        btnsFeedback[1].gameObject.SetActive(true);
    }

    //The method is called when the player choose an object or an answer
    public void UpdateGameCondition()
    {
        UpdateAnswer(curUnit.unitFractionName);
    }

    public void ChangeAnswerMiniGame()
    {
        ChangeAnswer(curUnit.unitFractionName);
    }
}
