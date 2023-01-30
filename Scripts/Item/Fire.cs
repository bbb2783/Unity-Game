using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    [SerializeField] private string fireName;

    [SerializeField] private int damage; //불데미지
 
    [SerializeField] private float damageTime; //지속데미지
    private float currentDamageTime;

    [SerializeField] private float durationTime; //불의 지속시간
    private float currentDurationTime;

    [SerializeField]
    private ParticleSystem ps_Flame;

    private StatusController thePlayerStatus;


    private bool isFire = true;

    void Start()
    {
        thePlayerStatus = FindObjectOfType<StatusController>();
        currentDurationTime = durationTime;
    }
    void Update()
    {
        if(isFire)
        {
            ElapseTime();
        }
    }

    private void ElapseTime()
    {
        currentDurationTime -= Time.deltaTime;

        if(currentDamageTime > 0)
            currentDamageTime -=Time.deltaTime;
        
        if(currentDurationTime <=0)
        {
            Off();
        }
    }

    private void Off()
    {
        ps_Flame.Stop();
        isFire = false;
    }

    private void OnTriggerStay(Collider other)
    {
        if(isFire && other.transform.tag == "Player")
        {
            if(currentDamageTime <= 0)
            {
                thePlayerStatus.DecreaseHP(damage);
                currentDamageTime = damageTime;
            }
        }
    }
}
