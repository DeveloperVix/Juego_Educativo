using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusObj : MonoBehaviour
{
    public bool disableAfterTime = false;
    public float timeActive = 1f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DisableObj());
    }

    IEnumerator DisableObj()
    {
        yield return new WaitForSeconds(timeActive);
        gameObject.SetActive(false);
    }
}
