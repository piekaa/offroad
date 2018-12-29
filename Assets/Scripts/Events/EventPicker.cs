using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Pieka/Event")]
public class EventPicker : ScriptableObject
{
    public int selectedIndex;
    public string Event;
    public EventNames EventNames;
}
