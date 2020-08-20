
using UnityEngine;

[CreateAssetMenu(menuName = "Mini Games/Select Fraction", fileName = "Select Fraction_MiniGame")]
public class GameSelectFractions : SO_BaseMiniGames
{
    int totalFractions = 0;
    public override void InitGame(TypeUnitFractions curUnit)
    {
        Debug.Log("Set Conditions");
        if (curUnit == TypeUnitFractions.ProperFractions)
        {
            totalFractions = Random.Range(1, 7);
            MiniGame_Manager.Instace.totalHits = totalFractions;
        }
    }
    public override void GenerateGameElement(TypeUnitFractions curUnit)
    {
        Debug.Log("Elements mini game");
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
}
