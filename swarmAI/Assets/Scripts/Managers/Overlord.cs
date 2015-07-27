using UnityEngine;
using System.Collections;

public class Overlord {

	private static Overlord _instance;
	public static Overlord instance {
		get {
			if (_instance == null) {
				_instance = new Overlord();
			}
			return _instance;
		}
	}

	public enum CurrentState {
		MAIN,
		GAME
	}

	public CurrentState curState = CurrentState.MAIN;
	public GameApp gameAppRef;
	public bool isMute = false;
	
	public void ToggleMute() {
		isMute = !isMute;
	}
}
