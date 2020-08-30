using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TypeUnitFractions { FractionElements, FractionIdentification, ProperFractions, ImproperFractions, MixedFractions}

[CreateAssetMenu(menuName = "Unit", fileName ="UnitName")]
public class UnitElementsScriptable : ScriptableObject
{
    public TypeUnitFractions unitFractionName;
    public int unitPoints = 10;

    public bool unitComplete = false;
    public int indexUnit = 0;

    /*
    The sprites that will show the instructions for each mini game, 
    what the player must do
    0 - Before Start the game
    1 - Against the clock
    2 - Select objects
    3 - Enter numbers
    4 - Drag objects
    5 - Tap
         */
    public Sprite[] spritesInstructionsMiniGame;
    /* 
    0 - Before Start the game
    1 - Against the clock
    2 - Select objects
    3 - Enter numbers
    4 - Drag objects
    5 - Tap
     */
    [TextArea]
    public string[] textInstructionsMiniGames;

    [Range(1, 2)]
    public int difficultyLevel;
}
