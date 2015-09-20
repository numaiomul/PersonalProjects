using UnityEngine;

public class GameManager : MonoBehaviour {
	
	public BoardManager boardRef;
	public UIManager uiRef;
	InputManager inputRef;
	
	bool isEndGame = false;
	
	void Awake() {
		boardRef.Init(this);
		GameObject tmp = new GameObject("InputManager",typeof(InputManager));
		inputRef = tmp.GetComponent<InputManager>();
		inputRef.Init(this);
	}
	
	public void Move(byte direction) {
		if (!isEndGame) boardRef.Move(direction);
	}
	
	public void Restart() {
		boardRef.Restart();
		isEndGame = false;
	}
	
	public void EndGame() {
		//we show endScreen
		uiRef.SetEndScreen(true);
		isEndGame = true;
	}
}
