using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainManager : MonoBehaviour
{
    public ChangeColor[] statusBtnsUnits;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < AppManager.Instance.theUnits.Length; i++)
        {
            AppManager.Instance.theUnits[i].unitComplete = DataToSave_Load.Instance.unitsStatus[i];
        }
    }

    public void LoadThisScene(int index)
    {
        StartCoroutine(AppManager.Instance.LoadTheScene(index));
    }

    public void SelectUnitToPlay(int indexUnit)
    {
        AppManager.Instance.unitSelected = AppManager.Instance.theUnits[indexUnit];
    }

    public void Reset()
    {
        for (int i = 0; i < DataToSave_Load.Instance.unitsStatus.Length; i++)
        {
            DataToSave_Load.Instance.unitsStatus[i] = false;
            AppManager.Instance.theUnits[i].unitComplete = false;
            statusBtnsUnits[i].checkedUI.SetActive(false);
        }
        LoadSave.Instance.SaveGame();
    }
}
