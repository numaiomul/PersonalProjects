using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HexagonManager : MonoBehaviour {

	public Transform up,upLeft,left,downLeft,down,downRight,right,upRight;
	public GameObject obstaclePrefab;

	List<GameObject> colliderList = new List<GameObject>();

	bool cameraIsClosing = false;
	public float cameraSpeed = 1f;
	
	void Awake() {
		CreateObstacle();
	}

	void Update() {
		if (colliderList.Count > 0)
		{
			foreach(GameObject tmp in colliderList) {
				tmp.transform.Translate(Vector3.forward * Time.deltaTime * 10f);
			}
		}
		
		GetComponent<Camera>().transform.Translate(Time.deltaTime * cameraSpeed * ((cameraIsClosing)?Vector3.forward:Vector3.back));
		if (GetComponent<Camera>().transform.position.z > 7f) {
			cameraIsClosing = false;	
		}
		if (GetComponent<Camera>().transform.position.z < -13f) {
			cameraIsClosing = true;	
		}
	}
	public void CreateObstacle() {
		GameObject tmp = (GameObject)Instantiate(obstaclePrefab);
		Transform destination = this.gameObject.transform;
		switch (Random.Range(0,8)) {
			case 0: destination = up;
				break;
			case 1: destination = upLeft;
				break;
			case 2: destination = left;
				break;
			case 3: destination = downLeft;
				break;
			case 4: destination = down;
				break;
			case 5: destination = downRight;
				break;
			case 6: destination = right;
				break;
			case 7: destination = upRight;
				break;
			default: Debug.LogException(new System.Exception("Something went VERY wrong here"));
				break;
		}
		tmp.transform.position = destination.position;
		tmp.transform.rotation = destination.rotation;
		colliderList.Add(tmp);
	}


}
