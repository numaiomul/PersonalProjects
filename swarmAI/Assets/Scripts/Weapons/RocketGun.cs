using UnityEngine;
using System.Collections;

public class RocketGun : Weapon {

	public override WeaponType weaponType {
		get {
			return WeaponType.ROCKET;
		}
	}
	private const float ROCKET_DAMAGE = 20f;
	public override float baseDmg {
		get {
			return ROCKET_DAMAGE;
		}
	}

	private const float RATE_OF_FIRE = 0.8f;
	public override float rateOfFire {
		get {
			return RATE_OF_FIRE;
		}
	}
	public override float altRateOfFire {
		get {
			return RATE_OF_FIRE * 2;
		}
	}

	public override AudioClip audioFile {
		get {
			throw new System.NotImplementedException ();
		}
	}

	public RocketGun (Unit _owner, AudioSource _audio) {
		bullerRef = Overlord.instance.gameAppRef.rocketProj;
		base.Init (_owner,_audio);
	}

	public override void Shoot () {
		if(!CanFire) return;
		isAltFire = false;
		rateCounter = rateOfFire;
		
		RocketProjectile rocketTmp = (GameObject.Instantiate(bullerRef,owner.weaponBarrel.transform.position,Quaternion.identity) 
		                    as GameObject).GetComponent<RocketProjectile>();
		rocketTmp.dmg = baseDmg;
		rocketTmp.SetDirection(direction);
		rocketTmp.iff = owner.iff;
		
		throw new System.NotImplementedException ("audio not impletemted");
	}
	public override void AltShoot () {	
		if(!CanFire) return;
		isAltFire = true;
		rateCounter = altRateOfFire;
		//fire 3 rockets
		Vector3 pos = owner.weaponBarrel.transform.position;
		RocketProjectile rocketTmp = (GameObject.Instantiate(bullerRef,pos,Quaternion.identity) 
		                        as GameObject).GetComponent<RocketProjectile>();
		rocketTmp.dmg = baseDmg;
		rocketTmp.SetDirection(direction);
		rocketTmp.iff = owner.iff;
		
		pos.y += 0.2f;
		pos.x -= 0.2f;
		rocketTmp = (GameObject.Instantiate(bullerRef,pos,Quaternion.identity) 
		                        as GameObject).GetComponent<RocketProjectile>();
		rocketTmp.dmg = baseDmg;
		rocketTmp.SetDirection(direction);
		rocketTmp.iff = owner.iff;
		
		pos.y -= 0.4f;
		rocketTmp = (GameObject.Instantiate(bullerRef,pos,Quaternion.identity) 
		                        as GameObject).GetComponent<RocketProjectile>();
		rocketTmp.dmg = baseDmg;
		rocketTmp.SetDirection(direction);
		rocketTmp.iff = owner.iff;
		throw new System.NotImplementedException ("audio not impletemted");
	}
}
