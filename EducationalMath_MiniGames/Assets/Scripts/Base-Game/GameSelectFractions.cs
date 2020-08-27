
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Mini Games/Select Fraction", fileName = "Select Fraction_MiniGame")]
public class GameSelectFractions : SO_BaseMiniGames
{
    int totalFractionsToSelect = 0;
    public bool[] posFraction;

    public override void InitGame(TypeUnitFractions curUnit)
    {
        //Debug.Log("Set Conditions");
        totalFractionsToSelect = Random.Range(1, 7);
        MiniGame_Manager.Instance.totalHits = totalFractionsToSelect;
        UI_Controller.Instance.inputfraction.SetActive(false);
        for (int i = 0; i < UI_Controller.Instance.fraction.Length; i++)
        {
            UI_Controller.Instance.fraction[i].gameObject.transform.parent.gameObject.SetActive(false);
        }
    }
    public override void GenerateGameElement(TypeUnitFractions curUnit)
    {
        //Debug.Log("Elements mini game");
        int[] fractionRand = new int[3];
        GameObject curFractionG;
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

        float posX = MiniGame_Manager.Instance.width - 2f;
        //E.J. if widht is 10, on coordinates is -5 to 5, divide, -4 is the offset 
        float posY = (MiniGame_Manager.Instance.height / 2f) - 3.5f;

        Vector3 fractionPos = new Vector3(posX, posY, 0);
        int indexPosFraction = 0;
        
        TypeUnitFractions otherFraction1 = TypeUnitFractions.ImproperFractions;
        TypeUnitFractions otherFraction2 = TypeUnitFractions.MixedFractions;

        if (curUnit == TypeUnitFractions.ImproperFractions)
        {
            otherFraction1 = TypeUnitFractions.ProperFractions;
        }
        else if(curUnit == TypeUnitFractions.MixedFractions)
        {
            otherFraction2 = TypeUnitFractions.ProperFractions;
        }

        for (int i = 0; i < 2; i++)
        {
            for (int y = 0; y < 3; y++)
            {
                curFractionG = Instantiate(objPrefab[0], fractionPos, Quaternion.identity);
                if (posFraction[indexPosFraction])
                {
                    fractionRand = GenerateFraction(curUnit);
                    curFractionG.GetComponent<FractionInteractable>().typeFractionToSelect = curUnit;
                }
                else
                {
                    Debug.Log("Fracción aleatoria");
                    int setRandF = Random.Range(1, 20);
                    if (setRandF <= 10)
                    {
                        fractionRand = GenerateFraction(otherFraction1);
                        curFractionG.GetComponent<FractionInteractable>().typeFractionToSelect = otherFraction1;
                    }
                    else
                    {
                        fractionRand = GenerateFraction(otherFraction2);
                        curFractionG.GetComponent<FractionInteractable>().typeFractionToSelect = otherFraction2;
                    }
                }

                curFractionG.GetComponent<FractionInteractable>().SetFractionTxt(fractionRand[2], fractionRand[0], fractionRand[1]);
                posX -= 5;
                fractionPos.x = posX;
                indexPosFraction++;
            }
            posX = MiniGame_Manager.Instance.width - 2f;
            fractionPos.x = posX;
            posY -= 3f;
            fractionPos.y = posY;
        }
        curFractionG = null;
    }
}
