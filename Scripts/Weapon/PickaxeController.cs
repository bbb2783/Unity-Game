using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickaxeController : CloseWeaponController {

    // 활성화 여부.
    public static bool isActivate = false;

    /*private void Start()
    {
        WeaponManager.currentWeapon = currentCloseWeapon.GetComponent<Transform>();
        WeaponManager.currentWeaponAnim = currentCloseWeapon.anim;
    }*/

    // Update is called once per frame
    void Update()
    {
        if (isActivate)
            TryAttack();
    }

    protected override IEnumerator HitCoroutine()
    {
        while (isSwing)
        {
            if (CheckObject())
            {
                if(hitInfo.transform.tag == "Rock")
                {
                    hitInfo.transform.GetComponent<Rock>().Mining();
                }
                else if(hitInfo.transform.tag == "Twig")
                {
                    hitInfo.transform.GetComponent<Twig>().Damage(this.transform);
                }
                else if (hitInfo.transform.tag == "NPC")
                {
                    SoundManager.instance.PlaySE("Animal_Hit");
                    hitInfo.transform.GetComponent<WeakAnimal>().Damage(1, transform.position);
                }
                else if (hitInfo.transform.tag == "Lion")
                {
                    SoundManager.instance.PlaySE("Animal_Hit");
                    hitInfo.transform.GetComponent<Lion>().Damage(1, transform.position);
                }
                else if (hitInfo.transform.tag == "Penguin")
                {
                    SoundManager.instance.PlaySE("Animal_Hit");
                    hitInfo.transform.GetComponent<Penguin>().Damage(1, transform.position);
                }
                else if (hitInfo.transform.tag == "WeakAnimal")
                {
                    SoundManager.instance.PlaySE("Animal_Hit");
                    hitInfo.transform.GetComponent<WeakAnimal>().Damage(30, transform.position);
                }
                else if (hitInfo.transform.tag=="Lionwild")
                {
                    SoundManager.instance.PlaySE("Animal_Hit");
                    hitInfo.transform.GetComponent<Lionwild>().Damage(30, transform.position);
                }
                isSwing = false;
                Debug.Log(hitInfo.transform.name);
            }
            yield return null;
        }
    }

    public override void CloseWeaponChange(CloseWeapon _closeWeapon)
    {
        base.CloseWeaponChange(_closeWeapon);
        isActivate = true;
    }
}