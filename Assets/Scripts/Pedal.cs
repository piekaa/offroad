using UnityEngine; 
using UnityEngine.EventSystems;

public enum Direction {
	up, right, down, left
}

public class Pedal : MonoBehaviour, IPointerDownHandler, IPointerUpHandler {
 
	int pedalPositionX;
	int pedalPositionY;
	
	int pedalWidth;
	int pedalHeight;

	[SerializeField]
	private Direction Direction;
 

	public bool Pushed { get; private set;}
	public float Value { get; private set;}

	public void Start() {
		
		Pushed = false;

		pedalHeight = (int)((RectTransform)transform).rect.height;
		pedalWidth = (int)((RectTransform)transform).rect.width;

		pedalPositionY = (int) transform.position.y - pedalHeight/2;
		pedalPositionX =  (int) transform.position.x - pedalWidth/2;
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
			int mx = (int)Input.mousePosition.x;
			int my = (int)Input.mousePosition.y;

			switch (Direction)
			{
			case Direction.up:
				Value = Mathf.Clamp((my - pedalPositionY) / (float)pedalHeight, 0 ,1);
				break;
			case Direction.right:
				Value = Mathf.Clamp ((mx - pedalPositionX) / (float)pedalWidth, 0 , 1);
				break;
			case Direction.down:
				Value = Mathf.Clamp((my - pedalPositionY) / (float)pedalHeight, 0 ,1);
				Value = (1-Value);
				break;
			case Direction.left:
				Value = Mathf.Clamp ((mx - pedalPositionX) / (float)pedalWidth, 0 , 1);
				Value = 1 - Value;
				break;
			} 
		}
	}
}
