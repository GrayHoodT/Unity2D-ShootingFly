using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ShootingFly
{
    public class PlayerAnimable : MonoBehaviour
    {
        [SerializeField]
        private Animator anim;
        public Animator Anim => anim;

        private void OnEnable()
        {
            var moveAction = GetComponent<PlayerInput>().actions["Move"];
            moveAction.started += OnMoveInputed;
            moveAction.performed += OnMoveInputed;
            moveAction.canceled += OnMoveInputed;
        }

        private void OnDisable()
        {
            var moveAction = GetComponent<PlayerInput>().actions["Move"];
            moveAction.started -= OnMoveInputed;
            moveAction.performed -= OnMoveInputed;
            moveAction.canceled -= OnMoveInputed;
        }

        public void SetParameterAxisX(int x) => anim.SetInteger(Defines.ANIM_PARAMETER_AXIS_X, x);
        public void OnMoveInputed(InputAction.CallbackContext context)
        {
            var x = context.ReadValue<Vector2>().x;
            var calculatedX = 0;
            if (x < 0)
                calculatedX = Mathf.FloorToInt(x);
            else if (x > 0)
                calculatedX = Mathf.CeilToInt(x); 

            SetParameterAxisX(calculatedX);
        }
    }
}
