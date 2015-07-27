using UnityEngine;
using System.Collections;

public class LaserGun : Weapon {

	public override WeaponType weaponType {
		get {
			return WeaponType.LASER;
		}
	}
	private const float LASER_DAMAGE = 2f;
	public override float baseDmg {
		get {
			return LASER_DAMAGE;
		}
	}

	private const float RATE_OF_FIRE = 0.2f;
	public override float rateOfFire {
		get {
			return RATE_OF_FIRE;
		}
	}
	public override float altRateOfFire {
		get {
			return RATE_OF_FIRE * 3;
		}
	}

	private const string AUDIO_FILE = "Audio/Laser";
	private AudioClip _audioFile;
	public override AudioClip audioFile {
		get {
			if (_audioFile == null) {
				_audioFile = Resources.Load<AudioClip>(AUDIO_FILE);
			}
			return _audioFile;
		}
	}



	public LaserGun (Unit _owner, AudioSource _audio)
	{
		bullerRef = Overlord.instance.gameAppRef.laserProj;
		base.Init (_owner,_audio);
	}

	public override void Shoot ()
	{
		if(!CanFire) return;
		isAltFire = false;
		rateCounter = rateOfFire;
		audio.PlayOneShot(audioFile);
		
		LaserProjectile laserTmp = (GameObject.Instantiate(bullerRef,owner.weaponBarrel.transform.position,Quaternion.identity) 
		                       as GameObject).GetComponent<LaserProjectile>();
		laserTmp.dmg = baseDmg;
		laserTmp.SetDirection(direction);
		laserTmp.iff = owner.iff;
		
	}
	public override void AltShoot ()
	{
		if(!CanFire) return;
		isAltFire = true;
		rateCounter = altRateOfFire;
		audio.PlayOneShot(audioFile);
		
		//fire 5 laser in a small degree cone
		Vector3 rotation = Quaternion.identity.eulerAngles;
		
		LaserProjectile laserTmp = (GameObject.Instantiate(bullerRef,owner.weaponBarrel.transform.position,Quaternion.Euler(rotation)) 
		                       as GameObject).GetComponent<LaserProjectile>();
		laserTmp.dmg = baseDmg;
		laserTmp.SetDirection(direction);
		laserTmp.iff = owner.iff;
		
		rotation.z += 5f;
		laserTmp = (GameObject.Instantiate(bullerRef,owner.weaponBarrel.transform.position,Quaternion.Euler(rotation)) 
		                       as GameObject).GetComponent<LaserProjectile>();
		laserTmp.dmg = baseDmg;
		laserTmp.SetDirection(direction);
		laserTmp.iff = owner.iff;
		
		rotation.z += 5f;
		laserTmp = (GameObject.Instantiate(bullerRef,owner.weaponBarrel.transform.position,Quaternion.Euler(rotation)) 
		                       as GameObject).GetComponent<LaserProjectile>();
		laserTmp.dmg = baseDmg;
		laserTmp.SetDirection(direction);
		laserTmp.iff = owner.iff;
		
		rotation.z -= 15f;
		laserTmp = (GameObject.Instantiate(bullerRef,owner.weaponBarrel.transform.position,Quaternion.Euler(rotation)) 
		                       as GameObject).GetComponent<LaserProjectile>();
		laserTmp.dmg = baseDmg;
		laserTmp.SetDirection(direction);
		laserTmp.iff = owner.iff;
		
		rotation.z -= 5f;
		laserTmp = (GameObject.Instantiate(bullerRef,owner.weaponBarrel.transform.position,Quaternion.Euler(rotation)) 
		                       as GameObject).GetComponent<LaserProjectile>();
		laserTmp.dmg = baseDmg;
		laserTmp.SetDirection(direction);
		laserTmp.iff = owner.iff;
	}
}
