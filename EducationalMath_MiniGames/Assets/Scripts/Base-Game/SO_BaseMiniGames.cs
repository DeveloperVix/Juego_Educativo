using UnityEngine;

public abstract class SO_BaseMiniGames : ScriptableObject
{
    [TextArea(3, 5)]
    public string[] goalGame;
    public GameObject[] objPrefab;
    public abstract void InitGame(TypeUnitFractions curUnit);

    public abstract void GenerateGameElement(TypeUnitFractions curUnit);

    public abstract void UpdateGameCondition(TypeUnitFractions curUnit);

    public abstract void ChangeAnswer(TypeUnitFractions curUnit);
}
