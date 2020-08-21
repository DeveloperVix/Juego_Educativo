using UnityEngine;

[CreateAssetMenu(menuName = "Mini Games/Select Obj", fileName = "Select Obj")]
public class GameSelectObj_SO : SO_BaseMiniGames
{
    public override void InitGame(TypeUnitFractions curUnit)
    {
        Debug.Log("Set Conditions");
        int[] fractionRand = new int[3];
        if (curUnit == TypeUnitFractions.ProperFractions)
        {   
            fractionRand = GenerateFraction(curUnit);
            MiniGame_Manager.Instace.numerator = fractionRand[0];
            MiniGame_Manager.Instace.denominator = fractionRand[1];

            //This is for proper fraction, at the begining, numerator = 1, denominator = 0
            /*while (MiniGame_Manager.Instace.minigameState == MiniGameState.Idle)
            {
                MiniGame_Manager.Instace.numerator = UnityEngine.Random.Range(1, 11);
                MiniGame_Manager.Instace.denominator = UnityEngine.Random.Range(2, 11);
                if (MiniGame_Manager.Instace.numerator < MiniGame_Manager.Instace.denominator)
                {
                    MiniGame_Manager.Instace.minigameState = MiniGameState.Playing;
                }
            }*/
            MiniGame_Manager.Instace.fraction[0].text = MiniGame_Manager.Instace.numerator.ToString();
            MiniGame_Manager.Instace.fraction[1].text = MiniGame_Manager.Instace.denominator.ToString();
            MiniGame_Manager.Instace.fraction[2].gameObject.transform.parent.gameObject.SetActive(false);
            //for the proper fractions, the player need to select the number of objects based on the numerator
            MiniGame_Manager.Instace.totalHits = MiniGame_Manager.Instace.numerator;
        }
    }

    public override void GenerateGameElement(TypeUnitFractions curUnit)
    {
        Debug.Log("Elements mini game");
        if (curUnit == TypeUnitFractions.ProperFractions)
        {
            int posX = 0;
            int posY = 0;

            Vector3 objPartPosition = new Vector3(posX, posY, 0);
            Instantiate(objPrefab[0], objPartPosition, Quaternion.identity);
            bool right = true;

            for (int i = 0; i < MiniGame_Manager.Instace.denominator - 1; i++)
            {
                if (right)
                {
                    posX++;
                    objPartPosition.x = posX;
                    Instantiate(objPrefab[0], objPartPosition, Quaternion.identity);
                    right = false;
                }
                else
                {
                    posX *= -1;
                    objPartPosition.x = posX;
                    Instantiate(objPrefab[0], objPartPosition, Quaternion.identity);
                    posX *= -1;
                    right = true;
                }
            }
        }
    }
}
