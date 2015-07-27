using UnityEngine;
using System.Collections;

public class MainApp : MonoBehaviour {

	public bool muteRef {
		get {
			return Overlord.instance.isMute;
		}
		set {
			Overlord.instance.isMute = value;
			audioRef.enabled = !value;
		}
	}
	
	public AudioListener audioRef;
	
	void Update() {
		if (Input.GetKeyDown(KeyCode.M)) {
			Overlord.instance.ToggleMute();
			audioRef.enabled = !Overlord.instance.isMute;
		}
	}
	
	#region MainMenu
	public void OnNewGame() {
		Application.LoadLevel("game");
		Overlord.instance.curState = Overlord.CurrentState.GAME;
	}
	#endregion
	
	#region Settings
	#endregion
	
	#region Credits
	#endregion
}
