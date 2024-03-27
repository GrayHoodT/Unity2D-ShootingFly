using System;
using UnityEngine;

namespace ShootingFly
{
    public class PlayerCollision : MonoBehaviour
    {
        public event Action<Collider2D> TriggerEntered;
        public event Action<Collider2D> TriggerExited;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            TriggerEntered?.Invoke(collision);
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            TriggerExited?.Invoke(collision);
        }
    }
}
