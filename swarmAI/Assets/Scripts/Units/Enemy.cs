using UnityEngine;
using System.Collections;

public class Enemy : Unit {

	public Weapon weapon;
	public int enemyType {get;private set;} //0 - normal
	public int animationType{get;private set;}

	public SpriteRenderer spriteRef;
	public PolygonCollider2D colRef;

	const string ANIMATION_PATH = "Animations/Movement/enemy";
	const string ANIMATION_NAME = "move";

	const string TEXTURE_PATH = "Textures/enemy";
	const string COLLIDER_PATH = "Colliders/enemyCollider";

	const float speed = 6f;

	public void Init(int _eType,int _aType) {
		enemyType = _eType;
		animationType = _aType;
		/*
		GetComponent<Animation>().AddClip((AnimationClip)Resources.Load(ANIMATION_PATH + animationType),ANIMATION_NAME);
		GetComponent<Animation>().Play(ANIMATION_NAME);
		*/
		Init();
	}

	protected override void Init () {
		base.Init ();
		iff = IFF.FOE;
		switch (enemyType) {
			case 0:
				weapon = new LaserGun(this,GetComponent<AudioSource>());
				Debug.Log (TEXTURE_PATH + enemyType);
				spriteRef.sprite = Resources.Load<Sprite> (TEXTURE_PATH + enemyType);
				GameObject tmpGO = (GameObject)Instantiate(Resources.Load(COLLIDER_PATH + enemyType));
				colRef.points = tmpGO.GetComponent<PolygonCollider2D>().points;
				Destroy(tmpGO);
				break;
			default:
				throw new System.NotImplementedException ();
		}
	}

	protected void Update() {
		if (GameApp.isPause) return;
		transform.Translate(Vector3.left * speed * Time.deltaTime,Space.World);
		/*
		if (GameApp.isPause && GetComponent<Animation>()[ANIMATION_NAME].speed != 0) {
			GetComponent<Animation>()[ANIMATION_NAME].speed = 0;
		}
		else if (!GameApp.isPause && GetComponent<Animation>()[ANIMATION_NAME].speed != 1) {
			GetComponent<Animation>()[ANIMATION_NAME].speed = 1;
		}*/
		weapon.Update(Time.deltaTime);
		if (Random.Range(0,10) == 0) {
			weapon.AltShoot();
		}
		else {
			weapon.Shoot();
		}
	}

	public override void TakeDeath ()
	{
		base.TakeDeath ();
		GetComponent<Animation>().Stop();
		Destroy (this.gameObject);
	}

	public void AnimShootWeapon() {
		weapon.Shoot();
	}

}
