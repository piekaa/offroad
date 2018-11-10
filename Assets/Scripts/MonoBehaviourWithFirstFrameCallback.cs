using UnityEngine;

public class MonoBehaviourWithFirstFrameCallback : MonoBehaviour
{
    private bool first = true;

    protected virtual void Update()
    {
        if (first)
        {
            OnFirstFrame();
            first = false;
        }
    }

    protected virtual void OnFirstFrame()
    {

    }

}