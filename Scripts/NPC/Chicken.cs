using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chicken : WeakAnimal {
    protected override void Update()
    {
        base.Update();
        if(theViewAngle.View() && !isDead)
        {
            Run(theViewAngle.GetTargetPos());
        }
    }
    protected override void ReSet()
    {
        base.ReSet();
        RandomAction();  
    }

    private void RandomAction()
    {
        RandomSound();
        int _random = Random.Range(0, 3); // 대기, 풀뜯기, 두리번, 걷기.

        if (_random == 0)
            Wait();
        else if (_random == 1)
            Jump();
        else if (_random == 2)
            TryWalk();
    }
    private void Wait()
    {
        currentTime = waitTime;
        //Debug.Log("대기");
    }
    private void Jump()
    {
        currentTime = waitTime;
        rigid.AddForce(0, jumpForce, 0);
        anim.SetTrigger("jump");
       // Debug.Log("점프");
    }
}
