using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class InputNumber : MonoBehaviour
{
    private bool activated = false;
    [SerializeField]
    private Text text_Preview;
    [SerializeField]
    private Text text_Input;

    [SerializeField]
    private GameObject go_Base;
    [SerializeField]
    private InputField if_text;
    [SerializeField]
    private ActionController thePlayer;

    void Update()
    {
        if(activated)
        {
            if(Input.GetKeyDown(KeyCode.Return))
                OK();
            else if (Input.GetKeyDown(KeyCode.Escape))
                Cancel();
        }
    }
    public void Call() //입력창 호출 
    {
        go_Base.SetActive(true);
        activated = true;
        text_Input.text ="";
        text_Preview.text = DragSlot.instance.dragSlot.itemCount.ToString(); // 호출하자마자 숫자생성
    }

    public void Cancel()
    {
        activated = false;
        go_Base.SetActive(false);
        DragSlot.instance.SetColor(0);
        DragSlot.instance.dragSlot = null;
    }

    public void OK()
    {
        DragSlot.instance.SetColor(0); //이미지가 바로사라지게함
        int num;
        if(text_Input.text !="")
        {
            if(CheckNumber(text_Input.text))
            {
                num = int.Parse(text_Input.text);
                if(num > DragSlot.instance.dragSlot.itemCount)
                    num = DragSlot.instance.dragSlot.itemCount;
            }
            else //숫자가 아닐떄
            {
                num = 1;
            }
        }
        else//아무것도 안적었을떄 
        {
            num = int.Parse(text_Preview.text);
        }
        StartCoroutine(DropItemCoroutine(num));
    }

    IEnumerator DropItemCoroutine(int _num)
    {
        for(int i = 0; i < _num; i++)
        {
            Instantiate(DragSlot.instance.dragSlot.item.itemPrefab, thePlayer.transform.position + thePlayer.transform.forward, Quaternion.identity); // quaternion은 회전없이 생성
            DragSlot.instance.dragSlot.SetSlotCount(-1);
            yield return new WaitForSeconds(0.05f); // 실행하고 대기
        }
        DragSlot.instance.SetColor(0); //
        DragSlot.instance.dragSlot = null;
        go_Base.SetActive(false);
        activated = false;
    }


    private bool CheckNumber(string _argString) // 숫자인지 확인하는기능
    {
        char[] _tempCharArray = _argString.ToCharArray();
        bool isNumber = true;
        for(int i =0; i < _tempCharArray.Length; i++)
        {
            if(_tempCharArray[i] >= 48 && _tempCharArray[i] <=57)
            {
                continue;
            }
            isNumber = false;
        }

        return isNumber;
    }
}
