using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree2 : MonoBehaviour {

    [SerializeField]
    private int hp;

    [SerializeField]
    private float destroyTime; // 파편 제거 시간

    [SerializeField]
    private CapsuleCollider col; // 구체 컬라이더.


    // 필요한 게임 오브젝트.
    [SerializeField]
    private GameObject go_tree;

    [SerializeField]
    private GameObject go_tree_item_prefab;
    [SerializeField]
    private GameObject go_tree_item_prefab2;

    [SerializeField]
    private int MaxCount;

    [SerializeField]
    private int MinCount;

    [SerializeField]
    private int DiaMaxCount;

    [SerializeField]
    private int DiaMinCount;

    // 필요한 사운드 이름.
    [SerializeField]
    private string strike_Sound;
    [SerializeField]
    private string destroy_Sound;


    public void Mining()
    {
        SoundManager.instance.PlaySE(strike_Sound);


        hp--;

        if (hp <= 0)
            Destruction();
    }

    private void Destruction()
    {
        SoundManager.instance.PlaySE(destroy_Sound);

        col.enabled = false;
        for (int i = 0; i < Mathf.Round(Random.Range(MinCount, MaxCount)); i++)
            Instantiate(go_tree_item_prefab, go_tree.transform.position, Quaternion.identity);
        for (int i = 0; i < Mathf.Round(Random.Range(DiaMinCount, DiaMaxCount)); i++)
            Instantiate(go_tree_item_prefab2, go_tree.transform.position, Quaternion.identity);
        Destroy(go_tree);

    }

}