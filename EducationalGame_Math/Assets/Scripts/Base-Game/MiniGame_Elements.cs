using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MiniGameState { Playing, Correct, Failed}

public class MiniGame_Elements : MonoBehaviour
{
    public MiniGameState minigameState;
    [TextArea]
    public string gameInstruction;

    public UnitElementsScriptable curUnit;

    public GameObject[] miniGamePrefabs;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void InitGameElements()
    {
        //Initialize the mini game elements, UI, etc
    }

    public virtual void GenerateGameElements()
    {
        //Create all the elements for the mini game
    }
}
