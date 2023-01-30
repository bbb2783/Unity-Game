    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionController : MonoBehaviour {

    [SerializeField]
    private float range; // 습득 가능한 최대 거리.

    private bool pickupActivated = false; // 습득 가능할 시 true.
    private bool dissolveActivated = false;
    private bool isDissolving = false; //중복으로 해체 막음
    private RaycastHit hitInfo; // 충돌체 정보 저장.


    // 아이템 레이어에만 반응하도록 레이어 마스크를 설정.
    [SerializeField]
    private LayerMask layerMask;

    // 필요한 컴포넌트.
    [SerializeField]
    private Text actionText;
    [SerializeField]
    private Inventory theInventory;
    [SerializeField]
    private WeaponManager theWeaponManager;
    [SerializeField]
    private Transform tf_MeatDissolveTool; 

	// Update is called once per frame
	void Update () {
        CheckAction();
        TryAction();
	}

    private void TryAction()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            CheckAction();
            CanPickUp();
            CanMeat();
        }
    }

    private void CanPickUp()
    {
        if (pickupActivated)
        {
            if(hitInfo.transform != null)
            {
                Debug.Log(hitInfo.transform.GetComponent<ItemPickUp>().item.itemName + " 획득했습니다");
                theInventory.AcquireItem(hitInfo.transform.GetComponent<ItemPickUp>().item);
                Destroy(hitInfo.transform.gameObject);
                InfoDisappear();
            }
        }
    }

    private void CanMeat()
    {
        if(dissolveActivated)
        {
            if((hitInfo.transform.tag == "WeakAnimal" || hitInfo.transform.tag == "Lionwild") && hitInfo.transform.GetComponent<Animal>().isDead && !isDissolving)
            {
                isDissolving = true;
                InfoDisappear();
                StartCoroutine(MeatCoroutine());
            }
        }
    }

    IEnumerator MeatCoroutine()
    {
        WeaponManager.isChangeWeapon = true;
        WeaponManager.currentWeaponAnim.SetTrigger("Weapon_Out");
        yield return new WaitForSeconds(0.2f); //해체 딜레이

        WeaponManager.currentWeapon.gameObject.SetActive(false);
        tf_MeatDissolveTool.gameObject.SetActive(true);

        yield return new WaitForSeconds(2f);

        WeaponManager.currentWeapon.gameObject.SetActive(true);
        tf_MeatDissolveTool.gameObject.SetActive(false);

        WeaponManager.isChangeWeapon = false;
        isDissolving = false;
    }

    private void CheckAction()
    {
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hitInfo, range, layerMask))
        {
            if (hitInfo.transform.tag == "Item")
            {
                ItemInfoAppear();
            }
            else if(hitInfo.transform.tag == "WeakAnimal" || hitInfo.transform.tag == "Lionwild")
            {
                MeatInfoAppear();
            }
        }
        else
            InfoDisappear();
    }

    private void ItemInfoAppear()
    {
        pickupActivated = true;
        actionText.gameObject.SetActive(true);
        actionText.text = hitInfo.transform.GetComponent<ItemPickUp>().item.itemName + " 획득 " + "<color=yellow>" + "(E)" + "</color>";
    }

    private void MeatInfoAppear()
    {
        if(hitInfo.transform.GetComponent<Animal>().isDead)
        {
            dissolveActivated = true;
            actionText.gameObject.SetActive(true);
            actionText.text = hitInfo.transform.GetComponent<Animal>().animalName + " 해체하기 " + "<color=yellow>" + "(E)" + "</color>";
        }
    }

    private void InfoDisappear()
    {
        pickupActivated = false;
        dissolveActivated = false;
        actionText.gameObject.SetActive(false);
    }
}
