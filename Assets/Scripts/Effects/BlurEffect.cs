using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pieka.Effects
{
    public class BlurEffect : MonoBehaviour
    {
        public Material Material;

        private int iterations = 0;

        public int Iterations = 2;

        public float Ratio = 0.65f;

        private float ratio = 1;

        private State state = State.Off;

        void OnRenderImage(RenderTexture src, RenderTexture dst)
        {
            if (state == State.Off)
            {
                Graphics.Blit(src, dst);
                return;
            }

            RenderTexture temp = RenderTexture.GetTemporary(src.width, src.height);
            Graphics.Blit(src, temp);

            for (int i = 0; i < iterations; i++)
            {
                RenderTexture temp2 = RenderTexture.GetTemporary((int)((float)temp.width * ratio), (int)((float)temp.height * ratio));
                Graphics.Blit(temp, temp2, Material);
                RenderTexture.ReleaseTemporary(temp);
                temp = temp2;
            }

            Graphics.Blit(temp, dst, Material);
            RenderTexture.ReleaseTemporary(temp);
        }
        public void TurnOn()
        {
            state = State.TurningOn;
            iterations = Iterations;
        }

        public void TurnOff()
        {
            state = State.TurningOff;
        }

        void Update()
        {
            switch (state)
            {
                case State.TurningOn:
                    {
                        ratio -= Time.deltaTime;
                        if (ratio <= Ratio)
                        {
                            ratio = Ratio;
                            state = State.On;
                        }
                        break;
                    }
                case State.TurningOff:
                    {
                        ratio += Time.deltaTime;
                        if (ratio >= 1)
                        {
                            ratio = 1;
                            state = State.Off;
                            iterations = 0;
                        }
                        break;
                    }
            }
        }

        private enum State
        {
            On,
            Off,
            TurningOn,
            TurningOff
        }

    }


}