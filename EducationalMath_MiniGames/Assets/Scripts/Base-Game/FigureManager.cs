using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FigureManager : MonoBehaviour
{
    public GameObject[] pieces;
    int totalPieces;


    public void SetFigure(int activePieces, bool interactable, int totalPiecesSelected)
    {
        totalPieces = transform.childCount;
        pieces = new GameObject[totalPieces];
        for (int i = 0; i < pieces.Length; i++)
        {
            pieces[i] = transform.GetChild(i).gameObject;
        }

        int piecesToDisable = 0;
        if (activePieces < 10)
        {
            piecesToDisable = 10 - activePieces;
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

        if (!interactable)
        {
            for (int i = 0; i < pieces.Length; i++)
            {
                if (pieces[i].activeInHierarchy)
                {
                    if (totalPiecesSelected > 0)
                    {
                        pieces[i].GetComponent<BaseObjInteractable>().SetObjNotInteractable(true);
                        totalPiecesSelected--;
                    }
                    else
                    {
                        pieces[i].GetComponent<BaseObjInteractable>().SetObjNotInteractable(false);

                    }
                }
            }
        }
    }
}
