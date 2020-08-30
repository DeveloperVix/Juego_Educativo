using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataToSave_Load : MonoBehaviour
{
    private static DataToSave_Load instance;
    public static DataToSave_Load Instance{get => instance;}
    public bool[] unitsStatus;

    private void Awake() 
    {
        instance = this;    
    }
}
