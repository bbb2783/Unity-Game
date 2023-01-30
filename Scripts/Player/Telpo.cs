using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Telpo : MonoBehaviour
{
    public Transform Target;

    void OnTriggerEnter(Collider _col)  // Ʈ���ſ� �浹�� �Ǿ��� ���� �� �Լ��� �����Ѵ�.
    {
        if (_col.gameObject.name == "Player")
        {
            _col.transform.position = Target.position; // �ε��� ��ü�� Ÿ���� ��ġ�� ������.

        }
    }
}
