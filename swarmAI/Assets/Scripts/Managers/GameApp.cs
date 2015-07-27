using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameApp : MonoBehaviour {

	public static bool isPause {get;private set;}

	public GameObject pauseRef;
	public GameObject endGameRefWin,endGameRefLose;
	public ParticleSystem backGroundRef;

	const float HP_OVER_WIDTH = 250f;
	const float HP_OVER_HEIGHT = 16f;

	public Player playerRef;
	public Text hpRef;
	public Image hpOver;

	public GameObject laserProj,rocketProj;
	public GameObject playerPrefab;
	public GameObject storyCanvas;
	public WeaponManager weaponManRef;
	public StoryMan storyRef;
	public AudioListener audioRef;


	Spawner spawnRef;


	void Awake() {
		Overlord.instance.gameAppRef = this;
		isPause = false;
		if (playerRef == null) {
			GameObject tmp = Instantiate(playerPrefab) as GameObject;
			playerRef = tmp.GetComponent<Player>();
		}
		weaponManRef.Init(playerRef);
		audioRef.enabled = !Overlord.instance.isMute;
		spawnRef = new Spawner();
		spawnRef.PrepareLevel(0);
		storyRef = new StoryMan();
		storyRef.Init(storyCanvas);
	}
	
	void Update() {
		if (Input.GetKeyDown(KeyCode.M)) {
			Overlord.instance.ToggleMute();
			audioRef.enabled = !Overlord.instance.isMute;
		}
		if (!playerRef.isAlive) return;
		if (Input.GetKeyDown(KeyCode.P)) {
			PauseGame();
		}
		if (isPause) return;
		if (Input.GetKeyDown(KeyCode.Q)) {
			SwitchWeapon();
		}
		hpRef.text = playerRef.currentHp.ToString() + "/" + playerRef.totalHp.ToString();
		hpOver.rectTransform.sizeDelta = new Vector2( 250f * (playerRef.currentHp/playerRef.totalHp), HP_OVER_HEIGHT);
		spawnRef.Update(Time.deltaTime);
		storyRef.Update(spawnRef.timeElasped);
	
	}
	void PauseGame() {
		isPause =! isPause;
		pauseRef.SetActive(isPause);
		if (isPause) {
			backGroundRef.Pause();
		}
		else {
			backGroundRef.Play ();
		}
	}
	void SwitchWeapon() {
		playerRef.SwitchWeapon();
	}

	public void ReturnToMenu() {
		Application.LoadLevel("main");
		Overlord.instance.curState = Overlord.CurrentState.MAIN;
	}
	public void EndGameWin() {
		isPause = true;
		endGameRefWin.SetActive(true);

	}
	public void EndGameLose() {
		isPause = true;
		endGameRefLose.SetActive(true);
	}


}
