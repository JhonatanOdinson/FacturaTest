using System;
using Core;
using Library.Scripts.Core;
using Modules.Character;
using UnityEngine;

namespace Modules.Actor.ActorComponent
{
    public class PlayerMover : ActorComponentBase
    {  
        [SerializeField] private float _frequency = 0.1f;
        [SerializeField] private float _amplitude = 2f;
        [SerializeField] private float _moveSpeed = 5f;
        [SerializeField] private float _turnSpeed = 5f;

        private float _distanceTraveled = 0f;
        private Vector3 _previousPosition;
     
        public override void Init(ActorBase actorBase)
        {
            base.Init(actorBase);
            _previousPosition = transform.position;
        }

        public override void UpdateExecute()
        {
            if (!IsEnable) return;
            _distanceTraveled += _moveSpeed * Time.deltaTime;

            float x = Mathf.Sin(_distanceTraveled * _frequency) * _amplitude;
            Vector3 newPosition = new Vector3(x, transform.position.y, _distanceTraveled);
            
            transform.position = newPosition;
            
            Vector3 moveDirection = (newPosition - _previousPosition).normalized;
            if (moveDirection.sqrMagnitude > 0.0001f)
            {
                Quaternion targetRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, _turnSpeed * Time.deltaTime);
            }

            _previousPosition = newPosition;
        }

    }
}

