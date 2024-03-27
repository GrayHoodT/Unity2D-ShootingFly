using UnityEngine;

namespace ShootingFly
{
    public class BulletMovable : MonoBehaviour
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

        private void OnEnable()
        {
            var collision = GetComponent<BulletCollisionable>();
            collision.TriggerEntered += OnBulletBorderTriggered;
        }

        private void Update()
        {
            trans.Translate(direction.normalized * speed * Time.deltaTime);
        }

        private void OnDisable()
        {
            var collision = GetComponent<BulletCollisionable>();
            collision.TriggerEntered -= OnBulletBorderTriggered;
        }

        #region Setter

        public void SetTransform(Transform trans) => this.trans = trans;
        public void SetDirection(Vector2 direction) => this.direction = direction;
        public void SetSpeed(float speed) => this.speed = speed;

        #endregion

        #region Callback Method

        private void OnBulletBorderTriggered(Collider2D other)
        {
            if (other.CompareTag(Defines.BULLET_BORDER_TAG) == false)
                return;

            GetComponent<Poolable>().ReturnToPool();
        }

        #endregion
    }
}
