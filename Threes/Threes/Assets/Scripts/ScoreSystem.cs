
public class ScoreSystem {
	private static ScoreSystem _instance;
	public static ScoreSystem instance {
		get {
			if (_instance == null) {
				_instance = new ScoreSystem();
			}
			return _instance;
		}
	}
	
	public float score {get;private set;}
	
	public void Add(float _toAdd) {
		score+= _toAdd;
	}
	
	public void Reset() {
		score = 0f;
	}
	
}
