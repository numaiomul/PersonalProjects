using UnityEngine;
using System.Collections;

public class LaserProjectile : Projectile {
	public override void Hit(Vector2 _pos) {
		base.Hit(_pos);
		Destroy(this.gameObject);
	}
}
