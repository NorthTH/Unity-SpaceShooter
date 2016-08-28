using UnityEngine;
using System.Collections;

public class TouchScript : MonoBehaviour {

	private Vector2 touchDeltaPosition;
	private float dragXPos;
	private float dragYPos;

	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI()
	{
		foreach (Touch touch in Input.touches) 
		{
			string message = "";
			message += "ID: " + touch.fingerId + "\n";
			message += "Phase: " + touch.phase.ToString () + "\n";
			message += "TabCount: " + touch.tapCount + "\n";
			message += "Pos X: " + touch.position.x + "\n";
			message += "Pos Y: " + touch.position.y + "\n";

			int num = touch.fingerId;
			GUI.Label (new Rect (0 + 130 * num, 0, 120, 100), message);

			//if (touch.phase == TouchPhase.Began)
			/*{
				
				touchDeltaPosition = Input.GetTouch (0).deltaPosition;
				GUI.Label (new Rect (0 + 130 * num, 0, 120, 100), Input.GetTouch (0).deltaTime + "\n" + touchDeltaPosition.y.ToString ());
			}

			Vector2 touchMovedPos = Input.GetTouch (0).position;
			dragXPos = touchMovedPos.x - touchBeginPos.x;
			dragYPos = touchMovedPos.y - touchBeginPos.y;
			GUI.Label (new Rect (0 + 130 * num, 0, 120, 100), dragXPos + "\n" + dragYPos);
*/

		}
	}
}
