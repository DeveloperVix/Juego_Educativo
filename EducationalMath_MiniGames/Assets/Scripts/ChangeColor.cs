using UnityEngine;
using UnityEngine.UI;

public class ChangeColor : MonoBehaviour
{
    
    public Image theImage;
    public float timeLerp = 1f;
    public Color noPlayed;
    public Color noPlayedLerp;
    public Color played;
    public Color playerLerp;

    // Update is called once per frame
    void Update()
    {
        //Se necesita saber el estado de la unidad, si ya se jugo
        theImage.color = Color.Lerp(noPlayed, noPlayedLerp, Mathf.PingPong(Time.time, timeLerp));
    }
}
