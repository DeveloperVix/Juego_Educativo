using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotDetections : MonoBehaviour
{
    public Animator ctrlAnimator;
    public int numberObjs = 0;

    private void Start() 
    {
        ctrlAnimator = GetComponent<Animator>();
        StartCoroutine(HideIndication());
    }

    IEnumerator HideIndication()
    {
        yield return new WaitForSeconds(4f);
        ctrlAnimator.Play("Slot_TapHideIndication");
    }

    private void OnTriggerEnter2D(Collider2D obj)
    {
        if (obj.CompareTag("ObjInteractable"))
        {
            numberObjs++;
            if (obj.GetComponent<FractionInteractable>().correctAnswer)
            {
                UI_Controller.Instance.UpdateGameCondition();
            }
            else
            {
                if (!UI_Controller.Instance.btnsFeedback[0].gameObject.activeInHierarchy)
                {
                    UI_Controller.Instance.btnsFeedback[0].gameObject.SetActive(true);
                }
            }
            if (numberObjs > 1)
            {
                UI_Controller.Instance.btnsFeedback[0].gameObject.SetActive(false);
            }
        }
    }

    private void OnTriggerStay2D(Collider2D obj)
    {
        if (obj.CompareTag("ObjInteractable"))
        {
            if (numberObjs == 1)
            {
                if (obj.GetComponent<FractionInteractable>().correctAnswer)
                {
                    MiniGame_Manager.Instance.curHits = 1;
                }
                if (!UI_Controller.Instance.btnsFeedback[0].gameObject.activeInHierarchy)
                {
                    UI_Controller.Instance.btnsFeedback[0].gameObject.SetActive(true);
                }
            }
            else if (numberObjs > 1)
            {
                UI_Controller.Instance.btnsFeedback[0].gameObject.SetActive(false);
            }
        }
    }


    private void OnTriggerExit2D(Collider2D obj)
    {
        if (obj.CompareTag("ObjInteractable"))
        {
            UI_Controller.Instance.ChangeAnswerMiniGame();
            numberObjs--;
            if (numberObjs <= 0)
            {
                numberObjs = 0;
                UI_Controller.Instance.btnsFeedback[0].gameObject.SetActive(false);
            }
        }
    }
}
