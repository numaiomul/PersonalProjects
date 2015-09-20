using UnityEngine;
using BorderTools;

public class InputManager : MonoBehaviour {
	
	const float SWIPE_MIN_LENGTH = 20f;

	GameManager gameRef;

	public void Init(GameManager _gameRef) {
		gameRef = _gameRef;
	}
	
	Vector3 initialPos, finalPos;
	void Update () {
#if UNITY_EDITOR
		if (Input.GetKeyDown(KeyCode.D)) {
			gameRef.Move(DirectionConstants.RIGHT);
		}
		if (Input.GetKeyDown(KeyCode.A)) {
			gameRef.Move(DirectionConstants.LEFT);
		}
		if (Input.GetKeyDown(KeyCode.W)) {
			gameRef.Move(DirectionConstants.UP);
		}
		if (Input.GetKeyDown(KeyCode.S)) {
			gameRef.Move(DirectionConstants.DOWN);
		}
#endif
		if (Input.GetMouseButtonDown(0)) {
			initialPos = Input.mousePosition;
		}
		if (Input.GetMouseButtonUp(0)) {
			finalPos = Input.mousePosition;
			if (Vector3.Distance(initialPos,finalPos) > SWIPE_MIN_LENGTH) {
				float angle = Vector3.Angle(finalPos - initialPos,Vector3.right);
				if (angle < 45f) {
					gameRef.Move(DirectionConstants.RIGHT);
				}
				else if (angle >= 45f && angle <= 135f) {
					Debug.Log ("final x:"+finalPos.x + "  initial x:"+initialPos.x);
					if (finalPos.y > initialPos.y) {
						gameRef.Move(DirectionConstants.UP);
					}
					else {
						gameRef.Move(DirectionConstants.DOWN);
					}
				}
				else {
					gameRef.Move(DirectionConstants.LEFT);
				}
			}
		}
	}
}
