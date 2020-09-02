using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Mini Games/Input fraction", fileName = "InputFraction_MiniGame")]
public class GameInputFraction_SO : SO_BaseMiniGames
{
    public override void InitGame(TypeUnitFractions curUnit)
    {
        int[] fractionRand = new int[3];
        fractionRand = GenerateFraction(curUnit, 11);

        MiniGame_Manager.Instance.numerator = fractionRand[0];
        MiniGame_Manager.Instance.denominator = fractionRand[1];

        if (curUnit == TypeUnitFractions.MixedFractions)
        {
            MiniGame_Manager.Instance.integer = fractionRand[2];
            MiniGame_Manager.Instance.totalHits = 3;
        }
        else
        {
            UI_Controller.Instance.inputFractionUI[2].gameObject.SetActive(false);
            MiniGame_Manager.Instance.totalHits = 2;
        }

        for (int i = 0; i < UI_Controller.Instance.fraction.Length; i++)
        {
            UI_Controller.Instance.fraction[i].gameObject.transform.parent.gameObject.SetActive(false);
        }

        UI_Controller.Instance.inputfraction.SetActive(true);

        for (int i = 0; i < UI_Controller.Instance.inputFractionUI.Length; i++)
        {
            UI_Controller.Instance.inputFractionUI[i].text = "0";
        }
    }

    public override void GenerateGameElement(TypeUnitFractions curUnit)
    {
        if (curUnit == TypeUnitFractions.ProperFractions)
        {
            int posX = -4;
            int posY = 0;
            GameObject newPiece;
            Vector3 objPartPosition = new Vector3(posX, posY, 0);
            int totalPiecesSelected = MiniGame_Manager.Instance.numerator;

            for (int i = 0; i < MiniGame_Manager.Instance.denominator; i++)
            {
                newPiece = Instantiate(objPrefab[0], objPartPosition, Quaternion.identity);
                if (totalPiecesSelected > 0)
                {
                    newPiece.GetComponent<BaseObjInteractable>().SetObjNotInteractable(true);
                    totalPiecesSelected--;
                }
                else
                    newPiece.GetComponent<BaseObjInteractable>().SetObjNotInteractable(false);
                posX++;
                objPartPosition.x = posX;
            }
        }
        else if (curUnit == TypeUnitFractions.ImproperFractions || curUnit == TypeUnitFractions.MixedFractions)
        {
            float posX = (MiniGame_Manager.Instance.width - 0.3f) * -1;
            float posY = (MiniGame_Manager.Instance.height / 2f) - 4f;
            Vector3 objPartPosition = new Vector3(posX, posY, 0);

            GameObject tempFigure;
            float totalFigures = 0;
            if (curUnit == TypeUnitFractions.ImproperFractions)
            {
                totalFigures = ((float)MiniGame_Manager.Instance.numerator / (float)MiniGame_Manager.Instance.denominator);
            }
            else
            {
                totalFigures = (MiniGame_Manager.Instance.integer + ((float)MiniGame_Manager.Instance.numerator / (float)MiniGame_Manager.Instance.denominator));
            }
            //Debug.Log(MiniGame_Manager.Instance.numerator / MiniGame_Manager.Instance.denominator);
            //Debug.Log("Piezas a crear: " + (totalFigures));

            totalFigures = (float)(Math.Ceiling(totalFigures));
            //Debug.Log("Piezas a crear: " + (totalFigures));
            float countFigures = totalFigures;
            Debug.Log("Piezas a colocar: " + countFigures);
            int row = 2;
            if (totalFigures == 3)
                row = 1;
            else if (totalFigures == 2)
                posX = 0;

            objPartPosition.x = posX;
            int piecesSelected;
            if (curUnit == TypeUnitFractions.MixedFractions)
                piecesSelected = (MiniGame_Manager.Instance.integer * MiniGame_Manager.Instance.denominator) + MiniGame_Manager.Instance.numerator;
            else
                piecesSelected = MiniGame_Manager.Instance.numerator;
            Debug.Log("Piezas seleccionadas: " + piecesSelected);

            for (int y = 0; y < row; y++)
            {
                for (int x = 0; x < 3; x++)
                {
                    if (countFigures > 0)
                    {
                        tempFigure = Instantiate(objPrefab[1], objPartPosition, Quaternion.identity);
                        if (countFigures == 1)
                        {
                            Debug.Log("Selecciono piezas restantes");
                            // Decirle al objeto cuantas piezas activas
                            tempFigure.GetComponent<FigureManager>().SetFigure(MiniGame_Manager.Instance.denominator, false, piecesSelected);
                        }
                        else
                        {
                            piecesSelected -= MiniGame_Manager.Instance.denominator;
                            Debug.Log("Piezas resultantes: " + piecesSelected);
                            // Decirle al objeto cuantas piezas activas
                            tempFigure.GetComponent<FigureManager>().SetFigure(MiniGame_Manager.Instance.denominator, false, MiniGame_Manager.Instance.denominator);
                        }

                        countFigures--;
                        if (totalFigures == 2)
                            x = 3;
                        else
                        {
                            posX += 5;
                            objPartPosition.x = posX;
                        }
                    }
                    else
                    {
                        x = 3;
                    }
                }
                if (totalFigures == 4 || totalFigures == 2)
                {
                    posX = 0;
                }
                else
                {
                    posX = (MiniGame_Manager.Instance.width - 1) * -1;
                }
                posY -= 3;
                objPartPosition.y = posY;
                objPartPosition.x = posX;
            }
            tempFigure = null;
        }
    }
}
