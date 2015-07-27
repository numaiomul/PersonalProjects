using UnityEngine;
using System.Collections;

public class Neutral : Unit {

	public SpriteRenderer spriteRef;
	public PolygonCollider2D colRef;

	const string TEXTURE_PATH = "Textures/neutral";
	const string COLLIDER_PATH = "Colliders/neutralCollider";

	const float speed = 4f;


	public void Init(int _tType) {
		Init();
		spriteRef.sprite = Resources.Load<Sprite> (TEXTURE_PATH + _tType);
		GameObject tmpGO = (GameObject)Instantiate(Resources.Load(COLLIDER_PATH + _tType));
		colRef.points = tmpGO.GetComponent<PolygonCollider2D>().points;
		Destroy(tmpGO);
	}
	
	protected override void Init () {
		base.Init ();
		iff = IFF.NEUTRAL;
	}
	
	protected void Update() {
		if (GameApp.isPause) return;
		transform.Translate(Vector3.left * speed * Time.deltaTime,Space.World);
		transform.Rotate(Vector3.forward * speed * 12 * Time.deltaTime);
	}
}
