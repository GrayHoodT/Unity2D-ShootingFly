using UnityEngine;
using UnityEngine.Pool;

namespace ShootingFly
{
    public class Poolable : MonoBehaviour
    {
        private IObjectPool<Poolable> pool;
        public IObjectPool<Poolable> Pool => pool;

        public void SetPool(IObjectPool<Poolable> pool) => this.pool = pool;
        public void ReturnToPool() => this.pool.Release(this);
    }
}
