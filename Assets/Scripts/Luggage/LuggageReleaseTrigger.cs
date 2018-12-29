using UnityEngine;

public class LuggageReleaseTrigger : MonoBehaviour
{
    [SerializeField] private LuggageHolder LuggageHolder;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(Consts.FrontWheelTag))
        {
            LuggageHolder.Luggage.Release();       
        }
    }
}