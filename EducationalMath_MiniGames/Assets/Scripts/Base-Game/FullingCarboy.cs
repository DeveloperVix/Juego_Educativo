using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FullingCarboy : MonoBehaviour
{
    public Transform water;
    public float valueTap = 0.1f;
    public int maxTaps = 0;
    public int curTaps = 0;
    public bool full = false;
    
    [Header("Taps")]
    public GameObject tapToFill;
    public GameObject tapToEmpty;

    [Header("Marker")]
    public GameObject markerJug;

    public void SetCarboy(int denominator, bool showTapFill, bool showTapEmpty)
    {
        valueTap = 0.95f / (float)denominator;
        maxTaps = denominator;
        for (int i = 0; i < maxTaps; i++)
        {
            GameObject newMarker = Instantiate(markerJug, transform.position, Quaternion.identity);
            newMarker.transform.SetParent(transform);
            newMarker.transform.localScale = new Vector3(2.049798f,0f,0f);
            var newScale = newMarker.transform.localScale;
            newScale.y += valueTap*2;
            newMarker.transform.localScale = newScale;
            newMarker.transform.localPosition = new Vector3(0f,valueTap * i,0f);
        }

        if(showTapFill)
        {
            tapToFill.SetActive(true);
        }
        
        if(showTapEmpty)
        {
            tapToEmpty.SetActive(true);
        }
        StartCoroutine(HideInstruction());
    }

    IEnumerator HideInstruction()
    {
        yield return new WaitForSeconds(4f);
        tapToFill.SetActive(false);
        tapToEmpty.SetActive(false);
    }

    public void OnMouseDown()
    {
        if (MiniGame_Manager.Instance.minigameState != MiniGameState.Playing)
            return;

        if (!full)
        {
            var newScale = water.localScale;
            newScale.y += valueTap;
            water.localScale = newScale;
            UI_Controller.Instance.UpdateGameCondition();
            curTaps++;
            if (curTaps == maxTaps)
            {
                full = true;
            }
        }
    }

    public void EmptyJug()
    {
        if (MiniGame_Manager.Instance.minigameState != MiniGameState.Playing)
            return;

        if (curTaps > 0)
        {
            var newScale = water.localScale;
            newScale.y -= valueTap;
            UI_Controller.Instance.ChangeAnswerMiniGame();
            curTaps--;
            full = false;
            if (curTaps <= 0)
                curTaps = 0;
            if (newScale.y <= 0)
            {
                newScale.y = 0f;
            }
            water.localScale = newScale;
        }
    }
}
