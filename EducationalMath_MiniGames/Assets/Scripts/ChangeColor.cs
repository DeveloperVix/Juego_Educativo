using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ChangeColor : MonoBehaviour
{
    public GameObject checkedUI;
    public Image theImage;
    public float timeLerp = 1f;
    public Color noPlayed;
    public Color noPlayedLerp;
    public Color played;
    public Color playerLerp;

    public UnitElementsScriptable unitStatus;

    private void Start()
    {
        StartCoroutine(WaitStatus());
    }

    IEnumerator WaitStatus()
    {
        yield return new WaitForSeconds(1f);
        if (unitStatus.unitComplete)
            checkedUI.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        //Se necesita saber el estado de la unidad, si ya se jugo
        if (!unitStatus.unitComplete)
            theImage.color = Color.Lerp(noPlayed, noPlayedLerp, Mathf.PingPong(Time.time, timeLerp));
        else
            theImage.color = Color.Lerp(played, playerLerp, Mathf.PingPong(Time.time, timeLerp));

    }
}
