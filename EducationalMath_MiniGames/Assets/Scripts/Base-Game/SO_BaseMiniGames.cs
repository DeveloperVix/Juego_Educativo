using UnityEngine;

public abstract class SO_BaseMiniGames : ScriptableObject
{
    public GameObject[] objPrefab;
    public abstract void InitGame(TypeUnitFractions curUnit);

    public abstract void GenerateGameElement(TypeUnitFractions curUnit);

    public abstract void UpdateGameCondition(TypeUnitFractions curUnit);
}
