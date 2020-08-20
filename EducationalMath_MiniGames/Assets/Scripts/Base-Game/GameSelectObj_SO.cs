using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Mini Games/Select Obj", fileName = "Select Obj")]
public class GameSelectObj_SO : ScriptableObject
{
    public void InitGame(TypeUnitFractions curUnit)
    {
        Debug.Log("Set Conditions");
        if (curUnit == TypeUnitFractions.ProperFractions)
        {
            //This is for proper fraction, at the begining, numerator = 1, denominator = 0
            while (MiniGame_SelectObj.Instace.minigameState == MiniGameState.Idle)
            {
                MiniGame_SelectObj.Instace.numerator = UnityEngine.Random.Range(1, 11);
                MiniGame_SelectObj.Instace.denominator = UnityEngine.Random.Range(2, 11);
                if (MiniGame_SelectObj.Instace.numerator < MiniGame_SelectObj.Instace.denominator)
                {
                    MiniGame_SelectObj.Instace.minigameState = MiniGameState.Playing;
                }
            }
            MiniGame_SelectObj.Instace.fraction[0].text = MiniGame_SelectObj.Instace.numerator.ToString();
            MiniGame_SelectObj.Instace.fraction[1].text = MiniGame_SelectObj.Instace.denominator.ToString();
            //for the proper fractions, the player need to select the number of objects based on the numerator
            MiniGame_SelectObj.Instace.totalHits = MiniGame_SelectObj.Instace.numerator;
        }
    }

    public void GenerateGameElement(TypeUnitFractions curUnit)
    {
        Debug.Log("Elements mini game");
        if (curUnit == TypeUnitFractions.ProperFractions)
        {
            int posX = 0;
            int posY = 0;

            Vector3 objPartPosition = new Vector3(posX, posY, 0);
            Instantiate(MiniGame_SelectObj.Instace.miniGamePrefabs[0], objPartPosition, Quaternion.identity);
            bool right = true;

            for (int i = 0; i < MiniGame_SelectObj.Instace.denominator - 1; i++)
            {
                if (right)
                {
                    posX++;
                    objPartPosition.x = posX;
                    Instantiate(MiniGame_SelectObj.Instace.miniGamePrefabs[0], objPartPosition, Quaternion.identity);
                    right = false;
                }
                else
                {
                    posX *= -1;
                    objPartPosition.x = posX;
                    Instantiate(MiniGame_SelectObj.Instace.miniGamePrefabs[0], objPartPosition, Quaternion.identity);
                    posX *= -1;
                    right = true;
                }
            }
        }

    }
}
