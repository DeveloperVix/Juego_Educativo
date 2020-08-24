using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Mini Games/Select Obj", fileName = "Select Obj")]
public class GameSelectObj_SO : SO_BaseMiniGames
{
    public override void InitGame(TypeUnitFractions curUnit)
    {
//        Debug.Log("Set Conditions");
        int[] fractionRand = new int[3];
        fractionRand = GenerateFraction(curUnit);
        if (curUnit == TypeUnitFractions.ProperFractions || curUnit == TypeUnitFractions.ImproperFractions)
        {
            MiniGame_Manager.Instance.numerator = fractionRand[0];
            MiniGame_Manager.Instance.denominator = fractionRand[1];

            UI_Controller.Instance.fraction[0].gameObject.transform.parent.gameObject.SetActive(true);
            UI_Controller.Instance.fraction[1].gameObject.transform.parent.gameObject.SetActive(true);
            UI_Controller.Instance.fraction[0].text = MiniGame_Manager.Instance.numerator.ToString();
            UI_Controller.Instance.fraction[1].text = MiniGame_Manager.Instance.denominator.ToString();
            UI_Controller.Instance.fraction[2].gameObject.transform.parent.gameObject.SetActive(false);

            //for the proper and improper fractions, the player need to select the number of objects based on the numerator
            MiniGame_Manager.Instance.totalHits = MiniGame_Manager.Instance.numerator;
        }
        else if(curUnit == TypeUnitFractions.MixedFractions)
        {
            MiniGame_Manager.Instance.numerator = fractionRand[0];
            MiniGame_Manager.Instance.denominator = fractionRand[1];
            MiniGame_Manager.Instance.integer = fractionRand[2];

            for (int i = 0; i < UI_Controller.Instance.fraction.Length; i++)
            {
                UI_Controller.Instance.fraction[i].gameObject.transform.parent.gameObject.SetActive(true);
            }
            UI_Controller.Instance.fraction[0].text = MiniGame_Manager.Instance.numerator.ToString();
            UI_Controller.Instance.fraction[1].text = MiniGame_Manager.Instance.denominator.ToString();
            UI_Controller.Instance.fraction[2].text = MiniGame_Manager.Instance.integer.ToString();

            MiniGame_Manager.Instance.totalHits = (MiniGame_Manager.Instance.integer * MiniGame_Manager.Instance.denominator) + MiniGame_Manager.Instance.numerator;
        }
    }

    public override void GenerateGameElement(TypeUnitFractions curUnit)
    {
//        Debug.Log("Elements mini game");

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
        else if (curUnit == TypeUnitFractions.ImproperFractions || curUnit == TypeUnitFractions.MixedFractions)
        {
            float posX = (MiniGame_Manager.Instance.width - 1) * -1;
            float posY = (MiniGame_Manager.Instance.height / 2f) - 4f;
            Vector3 objPartPosition = new Vector3(posX, posY, 0);

            GameObject tempFigure;
            float totalFigures = 0;
            if(curUnit == TypeUnitFractions.ImproperFractions)
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
            int row = 2;
            if (totalFigures == 3)
                row = 1;
            else if (totalFigures == 2)
                posX = 0;

            objPartPosition.x = posX;

            for (int y = 0; y < row; y++)
            {
                for (int x = 0; x < 3; x++)
                {
                    if (countFigures > 0)
                    {
                        tempFigure = Instantiate(objPrefab[1], objPartPosition, Quaternion.identity);
                        //Decirle al objeto cuantas piezas activas
                        tempFigure.GetComponent<FigureManager>().SetFigure(MiniGame_Manager.Instance.denominator);
                        countFigures--;
                        if (totalFigures == 2)
                            x = 3;
                        else
                        {
                            posX += 6;
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
