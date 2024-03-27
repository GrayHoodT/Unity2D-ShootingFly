using UnityEngine;
using UnityEngine.InputSystem;

namespace ShootingFly
{
    public class PlayerMovable : MonoBehaviour
    {
        [SerializeField]
        private Transform trans;
        public Transform Trans => trans;

        [SerializeField]
        private Vector2 direction;
        public Vector2 Direction => direction;

        [SerializeField]
        private float speed;
        public float Speed => speed;

        [Space(5), Header("Trigger State")]
        [ReadOnly, SerializeField]
        private bool isTopBorderTriggered;
        [ReadOnly, SerializeField]
        private bool isBottomBorderTriggered;
        [ReadOnly, SerializeField]
        private bool isLeftBorderTriggered;
        [ReadOnly, SerializeField]
        private bool isRightBorderTriggered;

        private void OnEnable()
        {
            var moveAction = GetComponent<PlayerInput>().actions["Move"];
            moveAction.started += OnMoveInputed;
            moveAction.performed += OnMoveInputed;
            moveAction.canceled += OnMoveInputed;

            var collision = GetComponent<PlayerCollisionable>();
            collision.TriggerEntered += OnTriggerEntered;
            collision.TriggerExited += OnTriggerExited;
        }

        private void Update()
        {
            var direction = CalculateDirection(this.direction);
            trans.Translate(direction * speed * Time.deltaTime);
        }

        private void OnDisable()
        {
            var moveAction = GetComponent<PlayerInput>().actions["Move"];
            moveAction.started -= OnMoveInputed;
            moveAction.performed -= OnMoveInputed;
            moveAction.canceled -= OnMoveInputed;

            var collision = GetComponent<PlayerCollisionable>();
            collision.TriggerEntered -= OnTriggerEntered;
            collision.TriggerExited -= OnTriggerExited;
        }

        #region Setter

        public void SetTransform(Transform trans) => this.trans = trans;
        public void SetDirection(Vector2 direction) => this.direction = direction;
        public void SetSpeed(float speed) => this.speed = speed;
        public void SetBorderTriggerState(Enums.BorderType borderType, bool isTriggered)
        {
            switch (borderType)
            {
                case Enums.BorderType.Top:
                    isTopBorderTriggered = isTriggered;
                    break;
                case Enums.BorderType.Bottom:
                    isBottomBorderTriggered = isTriggered;
                    break;
                case Enums.BorderType.Left:
                    isLeftBorderTriggered = isTriggered;
                    break;
                case Enums.BorderType.Right:
                    isRightBorderTriggered = isTriggered;
                    break;
            }
        }

        #endregion

        private Vector2 CalculateDirection(Vector2 originDirection)
        {
            var calculatedDirection = originDirection;
            if (isTopBorderTriggered == true && originDirection.y > 0 == true)
                calculatedDirection.y = 0;
            if (isBottomBorderTriggered == true && originDirection.y < 0 == true)
                calculatedDirection.y = 0;
            if (isLeftBorderTriggered == true && originDirection.x < 0 == true)
                calculatedDirection.x = 0;
            if (isRightBorderTriggered == true && originDirection.x > 0 == true)
                calculatedDirection.x = 0;

            var isDiagonal = originDirection.x != 0 && originDirection.y != 0;
            if (isDiagonal == true)
                calculatedDirection = calculatedDirection.normalized;

            return calculatedDirection;
        }

        #region Callback Methods

        public void OnMoveInputed(InputAction.CallbackContext context) => SetDirection(context.ReadValue<Vector2>());
        public void OnTriggerEntered(Collider2D other)
        {
            if (other.CompareTag(Defines.PLAYER_BORDER_TAG) == false)
                return;

            switch (other.name)
            {
                case Defines.TOP_BORDER_NAME:
                    SetBorderTriggerState(Enums.BorderType.Top, true);
                    break;
                case Defines.BOTTOM_BORDER_NAME:
                    SetBorderTriggerState(Enums.BorderType.Bottom, true);
                    break;
                case Defines.LEFT_BORDER_NAME:
                    SetBorderTriggerState(Enums.BorderType.Left, true);
                    break;
                case Defines.RIGHT_BORDER_NAME:
                    SetBorderTriggerState(Enums.BorderType.Right, true);
                    break;
            }
        }
        public void OnTriggerExited(Collider2D other) 
        {
            if (other.CompareTag(Defines.PLAYER_BORDER_TAG) == false)
                return;

            switch (other.name)
            {
                case Defines.TOP_BORDER_NAME:
                    SetBorderTriggerState(Enums.BorderType.Top, false);
                    break;
                case Defines.BOTTOM_BORDER_NAME:
                    SetBorderTriggerState(Enums.BorderType.Bottom, false);
                    break;
                case Defines.LEFT_BORDER_NAME:
                    SetBorderTriggerState(Enums.BorderType.Left, false);
                    break;
                case Defines.RIGHT_BORDER_NAME:
                    SetBorderTriggerState(Enums.BorderType.Right, false);
                    break;
            }
        }

        #endregion
    }
}
