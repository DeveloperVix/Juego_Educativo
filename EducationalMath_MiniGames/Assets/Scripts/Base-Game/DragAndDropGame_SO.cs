using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Mini Games/Drag and Drop", fileName = "Drag and Drop")]
public class DragAndDropGame_SO : SO_BaseMiniGames
{

    [Header("The circle with pieces")]
    public GameObject[] circlePrefab;

    public bool[] fractions;

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

        randCircle = UnityEngine.Random.Range(0, circlePrefab.Length);

        int[] fractionRand = new int[3];
        if (curUnit == TypeUnitFractions.ProperFractions)
        {
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
        else if (curUnit == TypeUnitFractions.MixedFractions || curUnit == TypeUnitFractions.ImproperFractions)
        {
            
            switch (randCircle)
            {
                case 0:
                    MiniGame_Manager.Instance.denominator = 2;
                    break;
                case 1:
                    MiniGame_Manager.Instance.denominator = 3;
                    break;
                case 2:
                    MiniGame_Manager.Instance.denominator = 4;
                    break;
                case 3:
                    MiniGame_Manager.Instance.denominator = 5;
                    break;
                case 4:
                    MiniGame_Manager.Instance.denominator = 6;
                    break;
                case 5:
                    MiniGame_Manager.Instance.denominator = 7;
                    break; 
            }
            int numeratorBase = MiniGame_Manager.Instance.denominator+1;
            int numeratorRand = UnityEngine.Random.Range(numeratorBase, 11);
            MiniGame_Manager.Instance.numerator = numeratorRand;
            
            if (curUnit == TypeUnitFractions.MixedFractions)
            {
                bool ready = false;
                while (!ready)
                {
                    numeratorRand = UnityEngine.Random.Range(1, 11);
                    if (numeratorRand < MiniGame_Manager.Instance.denominator)
                    {
                        ready = true;
                    }
                }
                MiniGame_Manager.Instance.numerator = numeratorRand;
                MiniGame_Manager.Instance.integer = UnityEngine.Random.Range(1, 6);
            }
        }
    }

    public override void GenerateGameElement(TypeUnitFractions curUnit)
    {
        float posX;
        float posY = 1f;
        if (curUnit == TypeUnitFractions.ProperFractions)
        {
            posX = (MiniGame_Manager.Instance.width - 2.9f) * -1f;
            GameObject newCircleFraction = Instantiate(circlePrefab[randCircle], new Vector3(posX, 0f, 0f), Quaternion.identity);
            newCircleFraction.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            newCircleFraction.GetComponent<FractionPieces>().SetCircle(MiniGame_Manager.Instance.numerator);
        }
        else
        {
            posX = MiniGame_Manager.Instance.width * -1f;
            Vector3 objPartPosition = new Vector3(posX, posY, 0);
            GameObject newCircleFraction;
            float totalFigures = 0;
            if (curUnit == TypeUnitFractions.ImproperFractions)
            {
                totalFigures = ((float)MiniGame_Manager.Instance.numerator / (float)MiniGame_Manager.Instance.denominator);
            }
            else
            {
                totalFigures = (MiniGame_Manager.Instance.integer + ((float)MiniGame_Manager.Instance.numerator / (float)MiniGame_Manager.Instance.denominator));
            }

            int piecesSelected;
            if (curUnit == TypeUnitFractions.MixedFractions)
                piecesSelected = (MiniGame_Manager.Instance.integer * MiniGame_Manager.Instance.denominator) + MiniGame_Manager.Instance.numerator;
            else
                piecesSelected = MiniGame_Manager.Instance.numerator;
            Debug.Log("Piezas seleccionadas: " + piecesSelected);

            totalFigures = (float)(Math.Ceiling(totalFigures));
            Debug.Log("Piezas a crear: " + (totalFigures));
            float countFigures = totalFigures;
            int row = 2;
            if (totalFigures == 3)
                row = 1;
            /*else if (totalFigures == 2)
                posX = 0;*/

            objPartPosition.x = posX;

            for (int i = 0; i < row; i++)
            {
                for (int x = 0; x < 3; x++)
                {
                    if (countFigures > 0)
                    {
                        newCircleFraction = Instantiate(circlePrefab[randCircle], objPartPosition, Quaternion.identity);
                        if (countFigures == 1)
                        {
                            Debug.Log("Selecciono piezas restantes");
                            newCircleFraction.GetComponent<FractionPieces>().SetCircle(piecesSelected);
                        }
                        else
                        {
                            piecesSelected -= MiniGame_Manager.Instance.denominator;
                            Debug.Log("Piezas resultantes: " + piecesSelected);
                        }
                        countFigures--;
                        posX += 2.5f;
                        objPartPosition.x = posX;
                    }
                    else
                    {
                        x = 3;
                    }
                }

                posX = MiniGame_Manager.Instance.width * -1;
                posY -= 3;
                objPartPosition.y = posY;
                objPartPosition.x = posX;
            }
        }

        posX = MiniGame_Manager.Instance.width;
        posY = 1f;
        Vector3 objPos = new Vector3(posX, posY, 0f);
        int indexPosFraction = 0;

        int[] fractionRand = new int[3];
        GameObject curFractionG;
        TypeUnitFractions otherFraction1 = TypeUnitFractions.ImproperFractions;
        TypeUnitFractions otherFraction2 = TypeUnitFractions.MixedFractions;

        if (curUnit == TypeUnitFractions.ImproperFractions)
        {
            otherFraction1 = TypeUnitFractions.ProperFractions;
        }
        else if (curUnit == TypeUnitFractions.MixedFractions)
        {
            otherFraction2 = TypeUnitFractions.ProperFractions;
        }

        fractions = new bool[4];

        for (int i = 0; i < 4; i++)
        {
            fractions[i] = false;
        }

        int randPos = UnityEngine.Random.Range(0, fractions.Length);
        if (fractions[randPos] == false)
        {
            fractions[randPos] = true;
        }

        for (int i = 0; i < 2; i++)
        {
            for (int y = 0; y < 2; y++)
            {
                curFractionG = Instantiate(objPrefab[0], objPos, Quaternion.identity);
                if (fractions[indexPosFraction])
                {
                    fractionRand[0] = MiniGame_Manager.Instance.numerator;
                    fractionRand[1] = MiniGame_Manager.Instance.denominator;
                    fractionRand[2] = MiniGame_Manager.Instance.integer;
                    curFractionG.GetComponent<FractionInteractable>().typeFractionToSelect = curUnit;
                    curFractionG.GetComponent<FractionInteractable>().SetFractionTxt(fractionRand[2], fractionRand[0], fractionRand[1], true);
                    curFractionG.GetComponent<FractionInteractable>().correctAnswer = true;
                }
                else
                {
                    Debug.Log("Fracción aleatoria");
                    int setRandF = UnityEngine.Random.Range(1, 20);
                    if (setRandF <= 10)
                    {
                        fractionRand = GenerateFraction(otherFraction1, 11);
                        curFractionG.GetComponent<FractionInteractable>().typeFractionToSelect = otherFraction1;
                    }
                    else
                    {
                        fractionRand = GenerateFraction(otherFraction2, 11);
                        curFractionG.GetComponent<FractionInteractable>().typeFractionToSelect = otherFraction2;
                    }
                    curFractionG.GetComponent<FractionInteractable>().SetFractionTxt(fractionRand[2], fractionRand[0], fractionRand[1], true);
                }

                posX -= 4f;
                objPos.x = posX;
                indexPosFraction++;
            }
            posX = MiniGame_Manager.Instance.width;
            posY = -2f;
            objPos.x = posX;
            objPos.y = posY;
        }
        curFractionG = null;
    }
}
