using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

	public Unit.IFF iff;

	public float speed = 4f;
	public float lifeTime = 1.5f;
	public float dmg;
	private Vector3 direction = Vector3.right;

	public ParticleSystem[] partSysList;

	protected virtual void Update() {
		if (!GameApp.isPause) {
			this.transform.Translate(direction * speed * Time.deltaTime);
			lifeTime -= Time.deltaTime;
			if (lifeTime < 0f) {
				Destroy(this.gameObject);
			}
		}
		foreach (ParticleSystem particleRef in partSysList) { 
			if (GameApp.isPause && particleRef.isPlaying) {
				particleRef.Pause();
			}
			else if (!GameApp.isPause && particleRef.isPaused) {
				particleRef.Play();
			}
		}
	}

	public virtual void Hit(Vector2 _pos) {
		Destroy(this.gameObject);
	}

	public void SetDirection(Vector3 _dir) {
		direction = _dir;
	}
}
