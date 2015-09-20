using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

	public RectTransform scoreScreenRef;
	public Text scoreScreenLabel;
	public GameManager gameRef;

	public void SetEndScreen(bool _state) {
		scoreScreenRef.gameObject.SetActive(_state);
		scoreScreenLabel.text = ScoreSystem.instance.score.ToString("#");
	}
	
	public void OnResetButton() {
		SetEndScreen(false);
		gameRef.Restart();
	}
}
