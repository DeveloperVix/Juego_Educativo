using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Mini Games/Fill Jug", fileName = "Fill Jug_MiniGame")]
public class GameFillJug_SO : SO_BaseMiniGames
{
    public SO_BaseMiniGames miniGameFuncitions;
    public override void InitGame(TypeUnitFractions curUnit)
    {
        miniGameFuncitions.InitGame(curUnit);
    }

    public override void GenerateGameElement(TypeUnitFractions curUnit)
    {
        if(curUnit == TypeUnitFractions.ProperFractions)
        {
            int posX = 0;
            int posY = -1;
            Vector3 objPartPosition = new Vector3(posX, posY, 0);
            GameObject newJug = Instantiate(objPrefab[0], objPartPosition, Quaternion.identity);
            newJug.GetComponent<FullingCarboy>().SetCarboy(MiniGame_Manager.Instance.denominator);
            newJug = null;
        }
        else if(curUnit == TypeUnitFractions.ImproperFractions || curUnit == TypeUnitFractions.MixedFractions)
        {
            float posX = (MiniGame_Manager.Instance.width + 0.15f) * -1;
            int posY = 0;
            Vector3 objPartPosition = new Vector3(posX, posY, 0);

            GameObject tempJug;
            float totalFigures = 0;
            if(curUnit == TypeUnitFractions.ImproperFractions)
            {
                totalFigures = ((float)MiniGame_Manager.Instance.numerator / (float)MiniGame_Manager.Instance.denominator);
            }
            else
            {
                totalFigures = (MiniGame_Manager.Instance.integer + ((float)MiniGame_Manager.Instance.numerator / (float)MiniGame_Manager.Instance.denominator));
            }
            totalFigures = (float)(Math.Ceiling(totalFigures));

            for (int i = 0; i < totalFigures; i++)
            {
                tempJug = Instantiate(objPrefab[0], objPartPosition, Quaternion.identity);
                tempJug.GetComponent<FullingCarboy>().SetCarboy(MiniGame_Manager.Instance.denominator);
                posX += 3.0f;
                objPartPosition.x = posX;
            }
        }
    }
}
