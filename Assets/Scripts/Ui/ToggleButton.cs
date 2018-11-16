using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Pieka.Ui
{
    public class ToggleButton : MonoBehaviour, IPointerClickHandler, IToggleButton
    {
        private Image image;
        private OnToggle onToggle;
        void Awake()
        {
            image = GetComponent<Image>();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (onToggle != null)
            {
                var state = onToggle();
                setColor(state);
            }
        }

        public void SetOnToggle(OnToggle onToggle)
        {
            this.onToggle = onToggle;
        }

        private void setColor(bool state)
        {
            if (state)
            {
                image.color = Color.green;
            }
            else
            {
                image.color = Color.white;
            }
        }

        public void SetInitialState(bool state)
        {
            setColor(state);
        }
    }
}