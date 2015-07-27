using UnityEngine;
using System.Collections;

public class Unit : MonoBehaviour{
#region fields
	public enum IFF {
		FRIEND,
		FOE,
		NEUTRAL
	}
	public IFF iff;

	public float totalHp;
	public float currentHp {get; protected set;}


	protected bool isInvuln = false;

	public bool isAlive {
		get {
			if (currentHp > 0) {
				return true;
			}
			else {
				return false;
			}
		}
	}
	public bool isStun {get; protected set;}
	public bool IsStun {
		get {
			if (isStun || (!canFire && !canMove)) 
				return true;
			else
				return false;
		}
	}
	public bool canFire {get; protected set;}
	public bool CanFire {
		get {
			if (!isStun && canFire) 
				return true;
			else
				return false;
		}
	}
	public bool canMove {get; protected set;}
	public bool CanMove {
		get {
			if (!isStun && canMove) 
				return true;
			else
				return false;
		}
	}

	public GameObject weaponBarrel;
#endregion

	protected virtual void Init() {
		canFire = true;
		canMove = true;
		isStun = false;
		isInvuln = false;
		currentHp = totalHp;
	}

#region basicHP

	public virtual void HpGive(float dmg) {
		currentHp += dmg;
		if (currentHp > totalHp) {
			currentHp = totalHp;
		}
	}

	public virtual void GiveDmg(float dmg) {
		if(isInvuln) return;
		HpTake(dmg);
	}
	public virtual void HpTake(float dmg) {
		currentHp -= dmg;
		if (currentHp < 0) {
			currentHp = 0;
			TakeDeath();
		}
	}
	public virtual void TakeDeath() {
		isInvuln = true;
		isStun = true;
	}
#endregion

#region modifiers
	public virtual void SetCanFire(bool e) {
		canFire = e;
	}
	public virtual void SetCanMove(bool e) {
		canMove = e;
	}
	public virtual void SetIsStun(bool e) {
		isStun = e;
	}
#endregion

	public virtual void OnCollisionEnter2D(Collision2D _col) {
		Projectile proj = _col.gameObject.GetComponent<Projectile>();
		if (proj == null || proj.iff == this.iff) return;
		proj.Hit(_col.contacts[0].point);
		GiveDmg(proj.dmg);
	}

}
