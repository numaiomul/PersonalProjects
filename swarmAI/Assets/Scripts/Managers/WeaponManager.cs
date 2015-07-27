using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WeaponManager : MonoBehaviour {

	private LaserGun laserRef;
	public RectTransform laserOverLay;
	public RectTransform laserUnderLay;
	
	private RocketGun rocketRef;
	public RectTransform rocketOverLay;
	public RectTransform rocketUnderLay;
	
	
	const byte UNDER_WIDTH = 120;
	const byte UNDER_HEIGHT = 30;
	
	public void Init(Player _playerRef) {
		rocketRef = _playerRef.rocketRef;
		laserRef = _playerRef.laserRef;
		UiUpdate();
	}
	
	void Update() {
		UiUpdate();
	}
	
	void UiUpdate() {
		rocketOverLay.sizeDelta = new Vector2( (rocketRef.rateCounter*UNDER_WIDTH)/((rocketRef.isAltFire)?rocketRef.altRateOfFire:rocketRef.rateOfFire),
			UNDER_HEIGHT);
		rocketUnderLay.gameObject.SetActive(rocketRef.isActive);
		
		laserOverLay.sizeDelta = new Vector2( (laserRef.rateCounter*UNDER_WIDTH)/((laserRef.isAltFire)?laserRef.altRateOfFire:laserRef.rateOfFire), 
			UNDER_HEIGHT);
		laserUnderLay.gameObject.SetActive(laserRef.isActive);
	}
}