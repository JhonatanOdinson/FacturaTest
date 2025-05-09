using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AI;

public class AITEST : MonoBehaviour
{
    [SerializeField] private GameObject _target;
    [SerializeField] private NavMeshAgent _agent;

    private void Update ()
    {
        if (!_target) return;
            _agent.SetDestination(_target.transform.position);
    }
}
