using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HUD1 : MonoBehaviour
{
    [SerializeField]
    private GunController theGunController;
    private Gun currentGun;

    [SerializeField]
    private GameObject go_BulletHUD;

    [SerializeField] //총알 개수
    private Text[] text_Bullet;


    void Update()
    {
        CheckBullet();
    }

    private void CheckBullet()
    {
        currentGun = theGunController.GetGun();
        text_Bullet[0].text = currentGun.carryBulletCount.ToString();
        text_Bullet[1].text = currentGun.currentBulletCount.ToString();
    }
}
