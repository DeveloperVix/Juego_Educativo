
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Mini Games/Select Fraction", fileName = "Select Fraction_MiniGame")]
public class GameSelectFractions : SO_BaseMiniGames
{
    int totalFractionsToSelect = 0;
    public bool[] posFraction;
    public override void InitGame(TypeUnitFractions curUnit)
    {
        Debug.Log("Set Conditions");
        if (curUnit == TypeUnitFractions.ProperFractions)
        {
            totalFractionsToSelect = Random.Range(1, 7);
            MiniGame_Manager.Instace.totalHits = totalFractionsToSelect;
            for (int i = 0; i < MiniGame_Manager.Instace.fraction.Length; i++)
            {
                MiniGame_Manager.Instace.fraction[i].gameObject.transform.parent.gameObject.SetActive(false);
            }
        }
    }
    public override void GenerateGameElement(TypeUnitFractions curUnit)
    {
        Debug.Log("Elements mini game");
        posFraction = new bool[6];
        int totalRand = totalFractionsToSelect;

        for (int i = 0; i < 6; i++)
        {
            posFraction[i] = false;
        }

        while (totalRand > 0)
        {
            int randPos = Random.Range(0, posFraction.Length);
            if (posFraction[randPos] == false)
            {
                posFraction[randPos] = true;
                totalRand--;
            }
        }

        if (curUnit == TypeUnitFractions.ProperFractions)
        {
            float posX = MiniGame_Manager.Instace.width - 2f;
            //E.J. if widht is 10, on coordinates is -5 to 5, divide, -4 is the offset 
            float posY = (MiniGame_Manager.Instace.height / 2f) - 3.5f;

            Vector3 fractionPos = new Vector3(posX, posY, 0);
            Instantiate(objPrefab[0], fractionPos, Quaternion.identity);

            for (int i = 0; i < 2; i++)
            {
                for (int y = 0; y < 3; y++)
                {
                    Instantiate(objPrefab[0], fractionPos, Quaternion.identity);
                    posX -= 5;
                    fractionPos.x = posX;
                }
                posX = MiniGame_Manager.Instace.width - 2f;
                fractionPos.x = posX;
                posY -= 3f;
                fractionPos.y = posY;
            }
        }
    }

    public override void UpdateGameCondition(TypeUnitFractions curUnit)
    {
        
    }

    public override void ChangeAnswer(TypeUnitFractions curUnit)
    {
    }
}
