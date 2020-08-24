using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void LoadThisScene(int index)
    {
        StartCoroutine(AppManager.Instance.LoadTheScene(index));
    }

    public void SelectUnitToPlay(int indexUnit)
    {
        AppManager.Instance.unitSelected = AppManager.Instance.theUnits[indexUnit];
    }
}
