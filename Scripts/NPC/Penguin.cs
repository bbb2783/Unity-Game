using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Penguin : MonoBehaviour {

    [SerializeField] private string animalName; // 동물의 이름
    [SerializeField] private int hp; // 동물의 체력.

    [SerializeField] private float walkSpeed; // 걷기 스피드.

    private Vector3 direction; // 방향.
    private float applySpeed;
    public float jumpForce = 300;
    // 상태변수
    private bool isAction; // 행동중인지 아닌지 판별.
    private bool isWalking; // 걷는지 안 걷는지 판별.
    private bool isRunning; // 뛰는지 판별.
    private bool isDead; // 죽었는지 판별.
    [SerializeField] private float walkTime; // 걷기 시간
    [SerializeField] private float waitTime; // 대기 시간.
    [SerializeField] private float runTime; // 뛰기 시간.
    [SerializeField] private float runSpeed; // 뛰기 스피드.
    private float currentTime;


    // 필요한 컴포넌트
    [SerializeField] private Animator anim;
    [SerializeField] private Rigidbody rigid;
    [SerializeField] private BoxCollider boxCol;
     private AudioSource theAudio;

    [SerializeField] private AudioClip[] sound_pig_Normal;
    [SerializeField] private AudioClip sound_pig_Hurt;
    [SerializeField] private AudioClip sound_pig_Dead;

	// Use this for initialization
	void Start () {
        theAudio = GetComponent<AudioSource>();
        currentTime = waitTime;
        isAction = true;
	}
	
	// Update is called once per frame
	void Update () {
         if (!isDead)
        {
            Move();
            Rotation();
            ElapseTime();
        }
    }


    private void Move()
    {
        if (isWalking || isRunning)
            rigid.MovePosition(transform.position + (transform.forward * applySpeed * Time.deltaTime));
    }


    private void Rotation()
    {
        if (isWalking || isRunning)
        {
            Vector3 _rotation = Vector3.Lerp(transform.eulerAngles, new Vector3(0f, direction.y, 0f), 0.01f);
            rigid.MoveRotation(Quaternion.Euler(_rotation));
        }
    }

    private void ElapseTime()
    {
        if (isAction)
        {
            currentTime -= Time.deltaTime;
            if (currentTime <= 0)
                ReSet();
        }
    }

    private void ReSet()
    {
        isWalking = false; isRunning = false; isAction = true;
        applySpeed = walkSpeed;
        anim.SetBool("Walk", isWalking); anim.SetBool("Running", isRunning);
        direction.Set(0f, Random.Range(0f, 360f), 0f);
        RandomAction();
    }

    private void RandomAction()
    {
        RandomSound();
        int _random = Random.Range(0, 2); // 대기, 풀뜯기, 두리번, 걷기.

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
    private void TryWalk()
    {
        isWalking = true;
        anim.SetBool("Walk", isWalking);
        currentTime = walkTime;
       // Debug.Log("걷기");
    }

    private void Run(Vector3 _targetPos)
    {
        direction = Quaternion.LookRotation(transform.position - _targetPos).eulerAngles;
        currentTime = runTime;
        isWalking = false;
        isRunning = true;
        applySpeed = runSpeed;
        anim.SetBool("Running", isRunning);
    }
    public void Damage(int _dmg, Vector3 _targetPos)
    {
        if (!isDead)
        {
            hp -= _dmg;

            if (hp <= 0)
            {
                Dead();
                return;
            }

            PlaySE(sound_pig_Hurt);
            
            Run(_targetPos);
        }
    }
    private void Dead()
    {
        PlaySE(sound_pig_Dead);
        isWalking = false;
        isRunning = false;
        isDead = true;
        Debug.Log("Penguin 사망");
        
    }

    private void RandomSound()
    {
        int _random = Random.Range(0, 2); // 일상 사운드 3개.
        PlaySE(sound_pig_Normal[_random]);
    }

    private void PlaySE(AudioClip _clip)
    {
        theAudio.clip = _clip;
        theAudio.Play();
    }
}
