using System;
using System.Collections.Generic;
using UnityEngine;


public enum MiniGameState { Pause, Idle, Playing }

public class MiniGame_Manager : MonoBehaviour
{
    private static MiniGame_Manager instance;
    public static MiniGame_Manager Instance { get => instance; }

    public MiniGameState minigameState;

    public UnitElementsScriptable curUnit; //The unit to play
    public SO_BaseMiniGames curMiniGame;
    public List<SO_BaseMiniGames> miniGamesToPlay;
    public int indexMiniGame = -1;

    [Header("Fraction")]
    public int numerator = 1;
    public int denominator = 0;
    public int integer = 1;

    [Header("Camera's dimension")]
    public float height = 0f;
    public float width = 0;
    Camera cam;

    [Header("Mini games elements")]
    public int totalHits = 0;
    //[HideInInspector]
    public int curHits = 0;

    public int badAnswer = 0;
    

    //Delegates
    public delegate void MiniGameStructure(TypeUnitFractions theUnit);
    MiniGameStructure InitMiniGame;


    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        miniGamesToPlay = new List<SO_BaseMiniGames>();

        cam = Camera.main;
        //The width and heigth of the camera
        height = 2f * cam.orthographicSize;
        width = cam.aspect * 2f * cam.orthographicSize / 2f - 1.9f;
        width = (float)(Math.Round((double)width, 1));

        AppManager.Instance.MiniGamesSequence();
    }

    //Call this method when the list of mini games is ready 
    public void SetMiniGame()
    {
        indexMiniGame++;
        if(indexMiniGame == miniGamesToPlay.Count)
        {
            Debug.LogError("Mini juegos terminados");
        }
        curMiniGame = miniGamesToPlay[indexMiniGame];

        InitMiniGame += curMiniGame.InitGame;
        InitMiniGame += curMiniGame.GenerateGameElement;

        InitMiniGame(curUnit.unitFractionName);
        UI_Controller.Instance.UpdateGoalGame();
        minigameState = MiniGameState.Playing;
    }
}
