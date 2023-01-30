using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAnimal : Animal
{ //strong animal

    [SerializeField]
    protected int attackDamage; //공격 데미지
    [SerializeField]
    protected float attackDelay; //공격딜레이
    [SerializeField]
    protected LayerMask targetMask; //타켓

    [SerializeField]
    protected float ChaseTime; //추격시간
    protected float currentChaseTime; // 계산
    [SerializeField]
    protected float ChaseDelayTime; // 추격딜레이
    public void Chase(Vector3 _targetPos)
    {
        isChasing = true;
        destination = _targetPos;
        nav.speed = runSpeed;
        isRunning = true;
        anim.SetBool("Running", isRunning);
        nav.SetDestination(destination);
    }

    public override void Damage(int _dmg, Vector3 _targetPos)
    {
        base.Damage(_dmg, _targetPos);
        if(!isDead)
            Chase(_targetPos);
    }

    protected IEnumerator ChaseTargetCoroutine()
    {
        currentChaseTime = 0;

        while(currentChaseTime < ChaseTime)
        {
            Chase(theViewAngle.GetTargetPos());
            //가까이 있고 눈앞에있을경우
            
            if(Vector3.Distance(transform.position, theViewAngle.GetTargetPos()) <=3f)
            {
                if(theViewAngle.View())
                {
                    Debug.Log("공격 시도");
                    StartCoroutine(AttackCoroutine());
                }
            }
            yield return new WaitForSeconds(ChaseDelayTime);
            currentChaseTime += ChaseDelayTime;
        }
        isChasing = false;
        isRunning = false;
        nav.ResetPath();
    }

    protected IEnumerator AttackCoroutine()
    {
        isAttacking = true;
        nav.ResetPath();
        currentChaseTime = ChaseTime;
        yield return new WaitForSeconds(0.5f);
        transform.LookAt(theViewAngle.GetTargetPos());
        anim.SetTrigger("jump");
        yield return new WaitForSeconds(0.5f);
        RaycastHit _hit;
        if(Physics.Raycast(transform.position + Vector3.up, transform.forward, out _hit, 3, targetMask))
        {
            Debug.Log("플레이어 적중");
            thePlayerStatus.DecreaseHP(attackDamage);
        }
        else
        {
            Debug.Log("플레이어 적중실패");
        }
        yield return new WaitForSeconds(attackDelay);
        isAttacking = false;
        StartCoroutine(ChaseTargetCoroutine());
    }
}
