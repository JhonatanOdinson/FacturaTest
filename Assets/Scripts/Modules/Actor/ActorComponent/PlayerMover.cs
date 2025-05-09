using System;
using Core;
using Library.Scripts.Core;
using Modules.Character;
using UnityEngine;

namespace Modules.Actor.ActorComponent
{
    public class PlayerMover : ActorComponentBase
    {
        [SerializeField] private Rigidbody _rigidbody;

        [SerializeField] private float _frequency = 0.1f;
        [SerializeField] private float _amplitude = 2f;
        [SerializeField] private float _moveSpeed = 5f;

        private float _distanceTraveled = 0f;
        private bool _isMove;
        public override void Init(ActorBase actorBase)
        {
            CommonComponents.GameStateController.OnStateChange += CheckMoveAccess;
            base.Init(actorBase);
        }

        private void CheckMoveAccess(GameStateController.GameStateE gameStateE)
        {
            _isMove = gameStateE == GameStateController.GameStateE.Play;
        }

        public override void FixedUpdateExecute(float deltaTime)
        {
            if (!_isMove) return;
            _distanceTraveled += _moveSpeed * Time.fixedDeltaTime;
            
            float currentX = Mathf.Sin(_distanceTraveled * _frequency) * _amplitude;
            float nextX = Mathf.Sin((_distanceTraveled + 0.1f) * _frequency) * _amplitude;

            Vector3 currentPosition = new Vector3(currentX, transform.position.y, _distanceTraveled);
            Vector3 nextPosition = new Vector3(nextX, transform.position.y, _distanceTraveled + 0.1f);
            
            Vector3 direction = (nextPosition - currentPosition).normalized;
            Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);
            
            _rigidbody.MovePosition(currentPosition);
            _rigidbody.MoveRotation(Quaternion.Slerp(transform.rotation, targetRotation, 0.2f));
        }

    }
}

