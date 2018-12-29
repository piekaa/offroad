using UnityEngine;

public class LuggageTakeTrigger : MonoBehaviour
{
    [SerializeField] private LuggageHolder LuggageHolder;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.IsTouchingLayers(Consts.CarLayerMask))
        {
            LuggageHolder.Luggage.Fly();
        }
    }
}