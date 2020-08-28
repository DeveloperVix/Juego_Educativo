using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Mini Games/Drag and Drop", fileName = "Drag and Drop")]
public class DragAndDropGame_SO : SO_BaseMiniGames
{

    [Header("The circle with pieces")]
    public GameObject[] circlePrefab;

    int randCircle;
    public override void InitGame(TypeUnitFractions curUnit)
    {
        //There is only 1 correct answer
        MiniGame_Manager.Instance.totalHits = 1;
        UI_Controller.Instance.inputfraction.SetActive(false);
        for (int i = 0; i < UI_Controller.Instance.fraction.Length; i++)
        {
            UI_Controller.Instance.fraction[i].gameObject.transform.parent.gameObject.SetActive(false);
        }

        randCircle = Random.Range(0, circlePrefab.Length);

        //Definir la fracción ganadora en base a las piezas del circulo
        int[] fractionRand = new int[3];
        switch (randCircle)
        {
            case 0:
                fractionRand = GenerateFraction(curUnit, 3);
                MiniGame_Manager.Instance.denominator = 2;
                break;
            case 1:
                fractionRand = GenerateFraction(curUnit, 4);
                MiniGame_Manager.Instance.denominator = 3;
                break;
            case 2:
                fractionRand = GenerateFraction(curUnit, 5);
                MiniGame_Manager.Instance.denominator = 4;
                break;
            case 3:
                fractionRand = GenerateFraction(curUnit, 6);
                MiniGame_Manager.Instance.denominator = 5;
                break;
            case 4:
                fractionRand = GenerateFraction(curUnit, 7);
                MiniGame_Manager.Instance.denominator = 6;
                break;
            case 5:
                fractionRand = GenerateFraction(curUnit, 8);
                MiniGame_Manager.Instance.denominator = 7;
                break;
        }
        MiniGame_Manager.Instance.numerator = fractionRand[0];
    }

    public override void GenerateGameElement(TypeUnitFractions curUnit)
    {
        if (curUnit == TypeUnitFractions.ProperFractions)
        {
            float posX = (MiniGame_Manager.Instance.width - 2.9f) *-1f;
            GameObject newCircleFraction = Instantiate(circlePrefab[randCircle], new Vector3(posX, 0f, 0f), Quaternion.identity);
            newCircleFraction.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            newCircleFraction.GetComponent<FractionPieces>().SetCircle(MiniGame_Manager.Instance.numerator);

            //Crear las fracciones que se pueden arrastrar
            //Inicializar 3 de forma aleatoria
            //Seleccionar una con la respuesta correcta e inicializarla y decir que es la respuesta correcta en su script
            
        }
    }
}
