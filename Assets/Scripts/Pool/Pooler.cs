using UnityEngine;
using UnityEngine.Pool;

namespace ShootingFly
{
    public class Pooler : MonoBehaviour
    {
        [SerializeField]
        private Poolable prefab;
        [SerializeField]
        private bool collectionCheck;
        [SerializeField]
        private int defaultCapacity = 10;
        [SerializeField]
        private int maxSize = 10000;

        private IObjectPool<Poolable> pool;

        private void Awake()
        {
            pool = new ObjectPool<Poolable>(Create, OnGot, OnReleased, OnDestroyed, collectionCheck, defaultCapacity, maxSize);
        }

        public Poolable Get() => pool.Get();
        public void Release(Poolable poolable) => pool.Release(poolable);
        public void Clear() => pool.Clear();

        private Poolable Create()
        {
            var obj = GameObject.Instantiate(prefab);
            var poolable = obj.GetComponent<Poolable>();
            poolable.SetPool(pool);

            return poolable;
        }

        private void OnGot(Poolable poolable)
        {
            poolable.gameObject.SetActive(true);
        }

        private void OnReleased(Poolable poolable)
        {
            poolable.gameObject.SetActive(false);
        }

        private void OnDestroyed(Poolable poolable)
        {
            Destroy(poolable);
        }
    }
}
