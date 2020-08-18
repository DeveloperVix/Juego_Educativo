using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum MiniGameState { Pause, Playing, Correct, Failed}

public class MiniGame_Elements : MonoBehaviour
{
    private static MiniGame_Elements instace;
    public static MiniGame_Elements Instace { get => instace;}

    public MiniGameState minigameState;
    [TextArea]
    public string gameInstruction;

    public UnitElementsScriptable curUnit; //The unit to play

    public GameObject[] miniGamePrefabs; //Game object to spawn

    public int numerator = 1;
    public int denominator = 0;

    public float height = 0f;
    public float width = 0;
    Camera cam;

    [Header("Mini games elements")]
    public int totalHits = 0;
    int curHits = 0;
    /*
    0 -> btn check answer
    1 -> btn next mini game 
    */
    public GameObject[] btnsFeedback;
    public TextMeshProUGUI txtFeedbackAnswers;

   


    // Start is called before the first frame update
    void Start()
    {
        instace = this;

        cam = Camera.main;
        //The width and heigth of the camera
        height = 2f * cam.orthographicSize;
        width = cam.aspect * 2f * cam.orthographicSize /2f - 1.9f;
        width = (float)(Math.Round((double)width, 1));

        //This is for proper fraction, at the begining, numerator = 1, denominator = 0
        while (numerator > denominator && denominator != numerator)
        {
            numerator = UnityEngine.Random.Range (1,11);
            denominator = UnityEngine.Random.Range(2,11);
        }

        GenerateGameElements();
    }

    public void CheckAnswer()
    {
        if(curHits == totalHits)
        {
            txtFeedbackAnswers.text = "Respuesta correcta";
            //Suma puntos completos a los puntos actuales del jugador
        }
        else
        {
            txtFeedbackAnswers.text = "Respuesta incorrecta";
            //pasar el mini juego al final de la lista de los juegos por jugar
            //Suma la mitad de puntos a los puntos actuales del jugador
        }
        btnsFeedback[1].SetActive(true);
    }

    //The method is called when the player choose an object or an answer
    public void UpdateGameCondition()
    {
        //For mini game "Select Object"
        curHits++;
        if(!btnsFeedback[0].activeInHierarchy)
        {
            btnsFeedback[0].SetActive(true); //btn check answer
        }
    }

    public void ChangeAnswer()
    {
        //if the player changes his answer by reselecting a previously selected object
        curHits--;
        if(curHits <= 0)
        {
            curHits = 0;
            btnsFeedback[0].SetActive(false);
        }
    }

    public void InitGameElements()
    {
        //Initialize the mini game elements, UI, etc
    }

    public virtual void GenerateGameElements()
    {
        //Create all the elements for the mini game


        //For the mini game "Select Object"
        //for the proper fractions, the player need to select the number of objects based on the numerator
        totalHits = numerator; 
        int posX = 0;
        int posY = 0;
        
        Vector3 objPartPosition = new Vector3(posX, posY,0);
        Instantiate(miniGamePrefabs[0], objPartPosition, Quaternion.identity);
        bool right = true;
        
        for (int i = 0; i < denominator-1; i++)
        {    
            
            if(right)
            {
                posX++;
                objPartPosition.x = posX;
                Instantiate(miniGamePrefabs[0], objPartPosition, Quaternion.identity);
                right = false;
            }
            else
            {
                posX *= -1;
                objPartPosition.x = posX;
                Instantiate(miniGamePrefabs[0], objPartPosition, Quaternion.identity);
                posX *= -1;
                right = true;
            } 
        }
    }
}
