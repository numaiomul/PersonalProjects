using UnityEngine;
using System.Collections;
using SimpleJSON;

public class Spawner {

	TextAsset levelInfoText;
	JSONNode levelInfo;
	public float timeElasped {get; private set;}

	int eventId;
	JSONNode currentEvent {
		get {
			if (levelInfo["events"].Count <= eventId) {
				return null;
			}
			return levelInfo["events"][eventId];
		}
	}

	const string LEVEL_PATH = "Database/level";
	const string ENEMY_PREFAB = "Prefabs/Enemy";
	const string NEUTRAL_PREFAB = "Prefabs/Neutral";

	public void PrepareLevel(int levelId) {
		levelInfoText = (TextAsset) Resources.Load(LEVEL_PATH + levelId);
		levelInfo = JSONNode.Parse(levelInfoText.text);
		eventId = 0; 
	}

	public void Update (float deltaTime) {
		timeElasped += deltaTime;
		while (currentEvent != null && currentEvent["time"].AsFloat < timeElasped) {
			GameObject tmpGO;
			switch (currentEvent["type"]) {
				case "enemy" ://enemy
				tmpGO = (GameObject)GameObject.Instantiate(Resources.Load(ENEMY_PREFAB),
															AsteroidGetPos(currentEvent["animation"].AsInt),
															Quaternion.identity);
				Enemy tmpE = tmpGO.GetComponent<Enemy>();
				tmpE.Init(currentEvent["enemyType"].AsInt,currentEvent["animation"].AsInt);
					break;
				case "neutral" ://neutral
					tmpGO = (GameObject)GameObject.Instantiate(Resources.Load(NEUTRAL_PREFAB),
					                                           AsteroidGetPos(currentEvent["position"].AsInt),
					                                           Quaternion.identity);
					Neutral tmpN = tmpGO.GetComponent<Neutral>();
					tmpN.Init(currentEvent["texture"].AsInt);
					break;
				case "story" ://story
					Overlord.instance.gameAppRef.storyRef.AddStory(new StoryElem(currentEvent));
					break;
				case "endGame"://endgame
					Overlord.instance.gameAppRef.EndGameWin();
					break;
				default:
					Debug.Log ("I will kill someone " + currentEvent["type"].ToString());
					break;
			}
			eventId++;
		}
	}
	private Vector3 AsteroidGetPos(int _pos) {
		switch (_pos) {
			case 0: return new Vector3(9,4f   ,0);
			case 1: return new Vector3(9,2.5f ,0);
			case 2: return new Vector3(9,1f   ,0);
			case 3: return new Vector3(9,-1f  ,0);
			case 4: return new Vector3(9,-2.5f,0);
			case 5: return new Vector3(9,-4f  ,0);
			default: return Vector3.zero;
		}
	}

}
