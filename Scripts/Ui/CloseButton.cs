using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseButton : MonoBehaviour
{
    public void Close()
    {
        StartCoroutine(CloseAfterDelay());

    }
    private IEnumerator CloseAfterDelay()
    {
        yield return new WaitForSeconds(0.5f);
        gameObject.SetActive(false);
    }
}
