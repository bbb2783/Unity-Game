using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grass : MonoBehaviour
{
    [SerializeField]
    private int hp;

    [SerializeField]
    private GameObject go_hit_effect_prefab;

    [SerializeField] //이펙트 제거시간
    private float destroyTime;

    [SerializeField]
    private float force;

    private Rigidbody[] rigidbodys;
    private BoxCollider[] boxColiiders;

    [SerializeField]
    private string hit_sound;
    void Start()
    {
        rigidbodys = this.transform.GetComponentsInChildren<Rigidbody>();
        boxColiiders = transform.GetComponentsInChildren<BoxCollider>();
    }

    public void Damage()
    {
        hp--;
        Hit();

        if(hp<=0)
        {
            Destruction();
        }
    }

    private void Hit()
    {
        SoundManager.instance.PlaySE(hit_sound);
        var clone = Instantiate(go_hit_effect_prefab, transform.position + Vector3.up, Quaternion.identity);
        Destroy(clone, destroyTime);
    }

    private void Destruction()
    {
        for(int i =0; i< rigidbodys.Length; i++)
        {
            rigidbodys[i].useGravity = true;
            rigidbodys[i].AddExplosionForce(1f, transform.position, 1f);
            boxColiiders[i].enabled = true;
        }
        Destroy(this.gameObject, destroyTime);
    }
}