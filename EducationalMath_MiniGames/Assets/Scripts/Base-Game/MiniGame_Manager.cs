using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum MiniGameState { Pause, Idle, Playing, Finish }

public class MiniGame_Manager : MonoBehaviour
{
    private static MiniGame_Manager instance;
    public static MiniGame_Manager Instance { get => instance; }

    public MiniGameState minigameState;

    public UnitElementsScriptable curUnit; //The unit to play
    public SO_BaseMiniGames curMiniGame;
    public List<SO_BaseMiniGames> miniGamesToPlay;
    public int indexMiniGame = -1;
    [Header("Progress")]
    public int totalCorrectAnswers = 5;
    public int curCorrectAnswers = 0;
    public int curWrongAnswers = 0;
    float average = 0;

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
    void Awake()
    {
        instance = this;

        miniGamesToPlay = new List<SO_BaseMiniGames>();

        cam = Camera.main;
        //The width and heigth of the camera
        height = 2f * cam.orthographicSize;
        width = cam.aspect * 2f * cam.orthographicSize / 2f - 1.9f;
        width = (float)(Math.Round((double)width, 1));
    }

    private void Start()
    {
        AppManager.Instance.MiniGamesSequence();
        //SetMiniGame();
    }

    public void LoadThisScene(int index)
    {
        StartCoroutine(AppManager.Instance.LoadTheScene(index));
    }

    //Call this method when the list of mini games is ready also in the click button event "Next" on the UI
    public void SetMiniGame()
    {
        indexMiniGame++;
        UI_Controller.Instance.UpdateProgress(true);
        if (indexMiniGame == miniGamesToPlay.Count)
        {
            //            Debug.LogError("Mini juegos terminados");
            minigameState = MiniGameState.Finish;
            StartCoroutine(ShowResults());
        }
        else
        {
            curMiniGame = miniGamesToPlay[indexMiniGame];

            curHits = 0;
            badAnswer = 0;
            StartCoroutine(WaitMiniGame());
        }
    }

    IEnumerator ShowResults()
    {
        yield return new WaitForSeconds(1f);
        average = ((curCorrectAnswers - curWrongAnswers) * 10) / totalCorrectAnswers;

        if (average == 10)
        {
            UI_Controller.Instance.ShowResultsUnit("¡Perfecto!, has completado la lección sin ningún error, has estado estudiando", 0);
        }
        else if (average >= 8)
        {
            UI_Controller.Instance.ShowResultsUnit("¡Bien hecho!, has completado la lección, dominas el tema, recuerda practicar", 0);
        }
        else if (average >= 6)
        {
            UI_Controller.Instance.ShowResultsUnit("¡Bien!, has completado la lección, no olvides practicar para mejorar", 0);
        }
        else if (average <= 5)
        {
            UI_Controller.Instance.ShowResultsUnit("No pasa nada, recuerda que tienes el botón de ayuda (?) si te sientes perdido", 0);
        }


        curUnit.unitComplete = true;
        DataToSave_Load.Instance.unitsStatus[curUnit.indexUnit] = true;
        LoadSave.Instance.SaveGame();
    }

    public void ShowPointsUnit()
    {
        StartCoroutine(ShowPointsOnUI());
    }

    IEnumerator ShowPointsOnUI()
    {
        yield return new WaitForSeconds(0.35f);
        float totalPoints = average * (float)curUnit.unitPoints;
        //UI_Controller.Instance.ShowResultsUnit("Haz obtenido <b>{0}</b> de puntaje, estos puntos puedes usarlos en la tienda que esta en el menú", totalPoints);
        UI_Controller.Instance.ShowResultsUnit("Haz obtenido <b>{0}</b> de puntaje", average);
        DataToSave_Load.Instance.currentPoints += (int)average;
        LoadSave.Instance.SaveGame();
    }

    IEnumerator WaitMiniGame()
    {
        yield return new WaitForSeconds(0.6f);
        UI_Controller.Instance.ResetUI();
        var objsGame = GameObject.FindGameObjectsWithTag("ObjInteractable");
        for (int i = 0; i < objsGame.Length; i++)
        {
            Destroy(objsGame[i].gameObject);
        }

        InitMiniGame += curMiniGame.InitGame;
        InitMiniGame += curMiniGame.GenerateGameElement;

        InitMiniGame(curUnit.unitFractionName);
        InitMiniGame -= curMiniGame.InitGame;
        InitMiniGame -= curMiniGame.GenerateGameElement;
        UI_Controller.Instance.UpdateGoalGame();
        minigameState = MiniGameState.Playing;
    }

    public void MiniGameAtEnd()
    {
        SO_BaseMiniGames tempMiniGame = curMiniGame;
        indexMiniGame--;
        UI_Controller.Instance.UpdateProgress(false);

        for (int i = 0; i < miniGamesToPlay.Count; i++)
        {
            if (miniGamesToPlay[i].name == curMiniGame.name)
            {
                miniGamesToPlay.RemoveAt(i);
            }
        }
        miniGamesToPlay.Add(tempMiniGame);
        tempMiniGame = null;
    }
}
