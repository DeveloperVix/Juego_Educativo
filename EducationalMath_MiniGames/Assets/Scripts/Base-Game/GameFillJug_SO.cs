using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Mini Games/Fill Jug", fileName = "Fill Jug_MiniGame")]
public class GameFillJug_SO : SO_BaseMiniGames
{
    public SO_BaseMiniGames miniGameFunctions;
    public override void InitGame(TypeUnitFractions curUnit)
    {
        miniGameFunctions.InitGame(curUnit);
    }

    public override void GenerateGameElement(TypeUnitFractions curUnit)
    {
        if (curUnit == TypeUnitFractions.ProperFractions)
        {
            int posX = 0;
            int posY = -1;
            Vector3 objPartPosition = new Vector3(posX, posY, 0);
            GameObject newJug = Instantiate(objPrefab[0], objPartPosition, Quaternion.identity);
            newJug.GetComponent<FullingCarboy>().SetCarboy(MiniGame_Manager.Instance.denominator, true, true);
            newJug = null;
        }
        else if (curUnit == TypeUnitFractions.ImproperFractions || curUnit == TypeUnitFractions.MixedFractions)
        {
            float posX = 0f;
            int posY = -1;
            Vector3 objPartPosition = new Vector3(posX, posY, 0);

            GameObject tempJug;
            float totalFigures = 0;
            if (curUnit == TypeUnitFractions.ImproperFractions)
            {
                totalFigures = ((float)MiniGame_Manager.Instance.numerator / (float)MiniGame_Manager.Instance.denominator);
            }
            else
            {
                totalFigures = (MiniGame_Manager.Instance.integer + ((float)MiniGame_Manager.Instance.numerator / (float)MiniGame_Manager.Instance.denominator));
            }
            totalFigures = (float)(Math.Ceiling(totalFigures));
            bool right = false;
            if ((totalFigures % 2) == 0)
            {
                posX = 1.5f;
                right = false;
            }
            else
                right = true;
            objPartPosition.x = posX;
            tempJug = Instantiate(objPrefab[0], objPartPosition, Quaternion.identity);
            tempJug.GetComponent<FullingCarboy>().SetCarboy(MiniGame_Manager.Instance.denominator, true, false);
            bool showTapEmpty = false;
            for (int i = 0; i < totalFigures - 1; i++)
            {
                if(i == totalFigures-2)
                {
                    showTapEmpty = true;
                }

                if (right)
                {
                    posX += 3.0f;
                    objPartPosition.x = posX;
                    tempJug = Instantiate(objPrefab[0], objPartPosition, Quaternion.identity);
                    tempJug.GetComponent<FullingCarboy>().SetCarboy(MiniGame_Manager.Instance.denominator, false, showTapEmpty);
                    right = false;
                }
                else
                {
                    posX *= -1;
                    objPartPosition.x = posX;
                    tempJug = Instantiate(objPrefab[0], objPartPosition, Quaternion.identity);
                    tempJug.GetComponent<FullingCarboy>().SetCarboy(MiniGame_Manager.Instance.denominator, false, showTapEmpty);
                    posX *= -1;
                    right = true;
                }
            }
        }
    }
}
