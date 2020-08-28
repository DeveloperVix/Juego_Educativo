using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FractionPieces : MonoBehaviour
{
    public GameObject[] pieces;
    int totalPieces;

    public void SetCircle(int activePieces)
    {
        totalPieces = transform.childCount;
        pieces = new GameObject[totalPieces];
        for (int i = 0; i < pieces.Length; i++)
        {
            pieces[i] = transform.GetChild(i).gameObject;
        }

        int piecesToDisable = 0;
        if (activePieces < totalPieces)
        {
            piecesToDisable = totalPieces - activePieces;
            int rand = Random.Range(0, 21);
            if (rand < 11)
            {
                for (int i = 0; i < piecesToDisable; i++)
                {
                    pieces[i].SetActive(false);
                }
            }
            else
            {
                int count = 0;
                for (int i = pieces.Length - 1; count < piecesToDisable; i--)
                {
                    pieces[i].SetActive(false);
                    count++;
                }
            }
        }
    }
}
