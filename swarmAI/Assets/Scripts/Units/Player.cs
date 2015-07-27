using UnityEngine;
using System.Collections;

public class Player : Unit {

	const byte TOTAL_HP = 50;
	const float FULL_EMISSION = 500f;
	const float LOW_EMISSION = 100f;
	
	protected float speed = 5f;

	public LineRenderer lineRef; 
	public ParticleSystem engineRef;

	public LaserGun laserRef {get;private set;}
	public RocketGun rocketRef {get;private set;}
	public Weapon currentWeapon {get;private set;}

	void Awake() {
		Init();
	} 
	protected override void Init ()	{
		base.Init();
		iff = IFF.FRIEND;
		totalHp = TOTAL_HP;
		currentHp = TOTAL_HP;
		laserRef = new LaserGun(this,GetComponent<AudioSource>()); 
		rocketRef = new RocketGun(this,GetComponent<AudioSource>());
		currentWeapon = laserRef;
		currentWeapon.isActive = true;
	}

	protected void Update() {
		if (GameApp.isPause) return;
		if (CanMove) FollowMouse();
		currentWeapon.Update(Time.deltaTime);
		if (Input.GetMouseButton(0) && CanFire) currentWeapon.Shoot();
		if (Input.GetKey(KeyCode.E) && CanFire) currentWeapon.AltShoot();
	}

	public void FollowMouse() {
		Vector3 fromPos = this.transform.position;
		Vector3 mousePos = Input.mousePosition;
		if (mousePos.x < 0) {
			mousePos.x = 0;
		}
		else if (mousePos.x > Screen.width) {
			mousePos.x = Screen.width;
		}
		if (mousePos.y < 0) {
			mousePos.y = 0;
		}
		else if (mousePos.y > Screen.height) {
			mousePos.y = Screen.height;
		}

		Vector3 toPos = Camera.main.ScreenToWorldPoint(mousePos);
		toPos.z = 0f;
		float dist = Vector3.Distance (fromPos,toPos);
		if (dist != 0) {
			engineRef.emissionRate = FULL_EMISSION;
			if (dist < speed * Time.deltaTime) {
				this.transform.position = toPos;
			}
			else {
				this.transform.position = Vector3.Lerp(fromPos,toPos,speed * Time.deltaTime / dist);
			}
			lineRef.SetPosition(0,this.transform.position);
			lineRef.SetPosition(1,toPos);
		}
		else {
			engineRef.emissionRate = LOW_EMISSION;
		}

	}

	public void SwitchWeapon() {
		currentWeapon.isActive = false;
		if (currentWeapon == laserRef) {
			currentWeapon = rocketRef;
		}
		else {
			currentWeapon = laserRef;
		}
		currentWeapon.isActive = true;
	}

	public override void TakeDeath() {
		base.TakeDeath();
		Overlord.instance.gameAppRef.EndGameLose();
	}
}
