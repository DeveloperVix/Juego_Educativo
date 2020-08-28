using UnityEngine;

public abstract class SO_BaseMiniGames : ScriptableObject
{
    public bool listed = false;
    public bool completed = false;
    [TextArea(3, 5)]
    public string[] goalGame;
    public GameObject[] objPrefab;

    public abstract void InitGame(TypeUnitFractions curUnit);

    public abstract void GenerateGameElement(TypeUnitFractions curUnit);

    public int[] GenerateFraction(TypeUnitFractions curUnit, int maxNumber)
    {
        bool ready = false;
        int[] fractionRand = new int[3];
        int numerator = 0;
        int denominator = 0;
        int integer = 0;
        switch (curUnit)
        {
            case TypeUnitFractions.ProperFractions:
                //Proper fraction if the numerator is < than the denominator
                while (!ready)
                {
                    numerator = UnityEngine.Random.Range(1, maxNumber);
                    denominator = UnityEngine.Random.Range(2, maxNumber);
                    if (numerator < denominator)
                    {
                        if(numerator != denominator)
                            ready = true;
                    }
                }
                fractionRand[0] = numerator;
                fractionRand[1] = denominator;
                fractionRand[2] = 0;
                break;
            case TypeUnitFractions.ImproperFractions:
                //Iroper fraction if the numerator is > than the denominator
                while (!ready)
                {
                    numerator = UnityEngine.Random.Range(3, maxNumber);
                    denominator = UnityEngine.Random.Range(2, maxNumber);
                    if (numerator > denominator)
                    {
                        if(numerator != denominator)
                            ready = true;
                    }
                }
                fractionRand[0] = numerator;
                fractionRand[1] = denominator;
                fractionRand[2] = 0;
                break;
            case TypeUnitFractions.MixedFractions:
            Debug.Log("coloco fraccion mixta");
                while (!ready)
                {
                    numerator = UnityEngine.Random.Range(1, maxNumber);
                    denominator = UnityEngine.Random.Range(2, maxNumber);
                    if (numerator < denominator)
                    {
                        if(numerator != denominator)
                            ready = true;
                    }
                }
                integer = UnityEngine.Random.Range(1, 6);
                fractionRand[0] = numerator;
                fractionRand[1] = denominator;
                fractionRand[2] = integer;
                break;
        }
        return fractionRand;
    }
}
