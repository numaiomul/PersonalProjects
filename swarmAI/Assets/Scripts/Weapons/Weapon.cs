using UnityEngine;
using System.Collections;

public abstract class Weapon {

	public enum WeaponType {
		LASER,
		ROCKET
	}

	public abstract WeaponType weaponType {get;}
	public bool isActive = false;
	public bool isAltFire {get;protected set;}
	public GameObject bullerRef {get;protected set;}
	public Vector3 direction {get;protected set;}
	public Unit owner {get;protected set;}
	public AudioSource audio {get;protected set;}
	public abstract float baseDmg {get;}
	public abstract float rateOfFire {get;}
	public abstract float altRateOfFire{get;}
	public abstract AudioClip audioFile {get;}

	protected void Init(Unit _owner, AudioSource _audio) {
		owner = _owner;
		audio = _audio;
		isAltFire = false;
		
		if (owner.iff == Unit.IFF.FRIEND) {
			direction = Vector3.right;
		}
		else {
			direction = Vector3.left;
		}
	}

	public bool CanFire {
		get {
			if (rateCounter <= 0)
				return true;
			else 
				return false;
		}
	}

	public float rateCounter;

	public virtual void Update (float delta)
	{	
		if (rateCounter > 0) {
			rateCounter -= delta;
		}
		if (rateCounter < 0) {
			rateCounter = 0;
		}
	}
	public abstract void Shoot();
	public abstract void AltShoot();
}
