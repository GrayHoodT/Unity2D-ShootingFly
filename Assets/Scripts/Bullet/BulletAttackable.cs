using UnityEngine;

namespace ShootingFly
{
    public class BulletAttackable : MonoBehaviour
    {
        [SerializeField]
        private string targetTag;
        public string TargetTag => targetTag;

        [SerializeField]
        private int attack;
        public int Attack => attack;

        private void OnEnable()
        {
            var collision = GetComponent<BulletCollisionable>();
            collision.TriggerEntered += OnTargetTriggered;
        }

        private void OnDisable()
        {
            var collision = GetComponent<BulletCollisionable>();
            collision.TriggerEntered -= OnTargetTriggered;
        }

        public void SetTargetTag(string targetTag) => this.targetTag = targetTag;
        public void SetAttack(int attack) => this.attack = attack;

        private void OnTargetTriggered(Collider2D other)
        {
            if (other.CompareTag(targetTag) == false)
                return;

            GetComponent<Poolable>().ReturnToPool();
        }
    }
}
