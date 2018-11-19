using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

namespace Pieka.Effects
{
    public class CameraShake : MonoBehaviour
    {

        private Stopwatch stopwatch = new Stopwatch();
        private int durationMillis;

        private float power;

        private bool shakeing = false;

        /// <summary>
        /// Shakes camera
        /// </summary>
        /// <param name="power">0-1</param>
        public void Shake(float power)
        {
            this.power = power;
            stopwatch.Reset();
            stopwatch.Start();
            durationMillis = (int)(100.0 * power);
            shakeing = true;
        }

        void LateUpdate()
        {
            if (shakeing)
            {
                transform.position += new Vector3(Mathf.Sin(stopwatch.ElapsedMilliseconds) * power, Mathf.Cos(stopwatch.ElapsedMilliseconds) * power, 0);
                if (stopwatch.ElapsedMilliseconds >= durationMillis)
                {
                    stopwatch.Stop();
                    shakeing = false;
                }
            }
        }
    }
}