using Core;
using Library.Scripts.Core;
using UnityEngine;

namespace Modules.Actor.Weapon
{
    public class TurretController : WeaponComponentBase
    {
        [SerializeField] private Transform _turretTop;
        [SerializeField] private float rotationSpeed = 100f;
        [SerializeField] private float maxAngle = 90f;

        private Vector2 input;
        private float currentYRotation = 0f;
        private float baseYRotation;
        
        private TouchInputController _inputController;
        public override void Init(WeaponBase weapon)
        {
            base.Init(weapon);
            _inputController = CommonComponents.TouchInputController;
            _inputController.OnJoystickPerformed += OnRotate;
            baseYRotation = transform.eulerAngles.y;
            currentYRotation = 0f;
        }

        public override void SetEnabled(bool state)
        {
            base.SetEnabled(state);
            
        }

        public override void UpdateExecute()
        {
            base.UpdateExecute();
            UpdateRotation();
        }

        public void OnRotate(Vector2 vector2)
        {
            input = vector2;
        }

        private void UpdateRotation()
        {
            float deltaRotation = input.x * rotationSpeed * Time.deltaTime;
            currentYRotation += deltaRotation;
            currentYRotation = Mathf.Clamp(currentYRotation, -maxAngle, maxAngle);

            float clampedY = baseYRotation + currentYRotation;
            _turretTop.rotation = Quaternion.Euler(0f, clampedY, 0f);
        }

        public override void Destruct()
        {
            base.Destruct();
            _inputController.OnJoystickPerformed -= OnRotate;
        }
    }
}
