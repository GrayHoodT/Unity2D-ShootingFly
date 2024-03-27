using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShootingFly
{
    public class AppSetting : MonoBehaviour
    {
        [SerializeField]
        private int frameRate;

        private void Awake()
        {
            Application.targetFrameRate = frameRate;
        }
    }
}
