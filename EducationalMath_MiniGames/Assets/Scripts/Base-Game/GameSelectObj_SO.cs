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
            MiniGame_Manager.Instance.numerator = fractionRand[0];
            MiniGame_Manager.Instance.denominator = fractionRand[1];

            UI_Controller.Instance.fraction[0].gameObject.transform.parent.gameObject.SetActive(true);
            UI_Controller.Instance.fraction[1].gameObject.transform.parent.gameObject.SetActive(true);
            UI_Controller.Instance.fraction[0].text = MiniGame_Manager.Instance.numerator.ToString();
            UI_Controller.Instance.fraction[1].text = MiniGame_Manager.Instance.denominator.ToString();
            UI_Controller.Instance.fraction[2].gameObject.transform.parent.gameObject.SetActive(false);
            //for the proper fractions, the player need to select the number of objects based on the numerator
            MiniGame_Manager.Instance.totalHits = MiniGame_Manager.Instance.numerator;
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

            for (int i = 0; i < MiniGame_Manager.Instance.denominator - 1; i++)
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
