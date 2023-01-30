using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class FieldOfViewAngle : MonoBehaviour {

    [SerializeField] private float viewAngle; // 시야각 (120도);
    [SerializeField] private float viewDistance; // 시야거리 (10미터);
    [SerializeField] private LayerMask targetMask; // 타겟 마스크 (플레이어)

    private PlayerController thePlayer;
    private NavMeshAgent nav;

    void Start()
    {
        thePlayer = FindObjectOfType<PlayerController>();
        nav = GetComponent<NavMeshAgent>();
    }

    public Vector3 GetTargetPos()
    {
        return thePlayer.transform.position; 
    }
	// Update is called once per frame

    public bool View()
    {
        Collider[] _target = Physics.OverlapSphere(transform.position, viewDistance, targetMask);

        for (int i = 0; i < _target.Length; i++)
        {
            Transform _targetTf = _target[i].transform;
            if(_targetTf.name == "Player")
            {

                Vector3 _direction = (_targetTf.position - transform.position).normalized;
                float _angle = Vector3.Angle(_direction, transform.forward);

                if(_angle < viewAngle * 0.5f)
                {
                    RaycastHit _hit;
                    if (Physics.Raycast(transform.position + transform.up, _direction, out _hit, viewDistance))
                    {
                        if(_hit.transform.name == "Player")
                        {
                            //Debug.Log("플레이어가 닭 시야 내에 있습니다");
                            Debug.DrawRay(transform.position + transform.up, _direction, Color.blue);
                            return true;
                        }
                    }
                }

            }
            if(thePlayer.GetRun())
            {
                if(CalcPathLength(thePlayer.transform.position) <= viewDistance)
                {
                    Debug.Log("동물이 주변에서 뛰고있는 플레이어 움직임을 파악함");
                    return true;
                }
            }
        }
        return false;
    }
    private float CalcPathLength(Vector3 _targetPos)
    {
        NavMeshPath _path = new NavMeshPath(); //경로
        nav.CalculatePath(_targetPos, _path);

        Vector3[] _wayPoint = new Vector3[_path.corners.Length + 2];
        
        _wayPoint[0] = transform.position; //자기 자신위치
        _wayPoint[_path.corners.Length + 1] = _targetPos;

        float _pathLength = 0;
        for(int i = 0; i < _path.corners.Length; i++)
        {
            _wayPoint[i+1] = _path.corners[i]; //waypoint에 경로를 넣음
            _pathLength += Vector3.Distance(_wayPoint[i], _wayPoint[i+1]); //경로길이 계산
        }

        return _pathLength;
    }
}