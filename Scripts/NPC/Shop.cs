using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public RectTransform uiGroup;

    PlayerController enterPlayer;

    // Start is called before the first frame update
    public void Enter(PlayerController player) 
    {
        enterPlayer = player; 
        uiGroup.anchoredPosition = Vector3.zero; // uiȭ�� �߾�����
    }

    // Update is called once per frame
    public void Exit() 
    {
        uiGroup.anchoredPosition = Vector3.down * 1000;
        PlayerController.ShopActivated = false;
    }
}