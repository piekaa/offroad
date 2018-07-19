using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public enum Direction {
	up, right, down, left
}

public class PedalButtonController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler {
 
	int y;
	int x;

	int w;
	int h;

	int mx;
	int my;

	public Direction Direction;

	public bool Pushed;
	public float Px;
	public float Py;


	public void Start() {

		Pushed = false;

		h = (int)((RectTransform)transform).rect.height;
		w = (int)((RectTransform)transform).rect.width;

		y = (int) transform.position.y - h/2;
		x =  (int) transform.position.x - w/2;
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		Pushed = true;
	}

	public void OnPointerUp (PointerEventData eventData)
	{
		Pushed = false;
	}


	public void Update() {
		if (Pushed)
		{
			mx = (int)Input.mousePosition.x;
			my = (int)Input.mousePosition.y;

			Px = Mathf.Clamp ((mx - x) / (float)w, 0 , 1);
			Py = Mathf.Clamp((my - y) / (float)h, 0 ,1);


			switch (Direction)
			{
			case Direction.up:
				Debug.Log (Py);
				break;
			case Direction.right:
				Debug.Log (Px);
				break;
			case Direction.down:
				Debug.Log (1-Py);
				break;
			case Direction.left:
				Debug.Log (1-Px);
				break;
			}
		}
	}
}
