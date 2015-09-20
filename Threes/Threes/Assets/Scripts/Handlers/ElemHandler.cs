using UnityEngine;
using UnityEngine.UI;
using BorderTools;
using System.Collections.Generic;

public class ElemHandler : MonoBehaviour {
	
	public const int JOKER = -2;
	public const int INACTIVE = -1;
	Color ACTIVE_COLOR = new Color(1f,1f,1f,1f);
	Color INACTIVE_COLOR = new Color(1f,1f,1f,0f);
	
	//Elem ref
	public Text textRef;
	public Image imgRef;
	public Dictionary<byte,ElemHandler> neighbours;
	//public ElemHandler leftElem,rightElem,upElem,downElem;
	public bool isOccupied = false;
	public Border border {get;private set;}
	
	int value;//0-joker,-1 = inactive
	int nextValue;
	
	public bool isActive {
		get {
			if (value != INACTIVE) return true;
			return false;
		}
	}
	
	public void Init(int _value, Border _border,float widthScale,float heightScale) {
		SetValue(_value);
		nextValue = INACTIVE;
		border = _border;
		Vector3 scale = new Vector3(widthScale,heightScale,1);
		this.GetComponent<RectTransform>().localScale = scale;
		
		neighbours = new Dictionary<byte,ElemHandler>(4);
		neighbours.Add(DirectionConstants.LEFT,null);
		neighbours.Add(DirectionConstants.RIGHT,null);
		neighbours.Add(DirectionConstants.UP,null);
		neighbours.Add(DirectionConstants.DOWN,null);
	}
	public bool CanMove(byte direction) {
		if (border.Has(direction)) return false; //we're can't move into border
		if (!neighbours[direction].isOccupied) return true; //we can move on empty
		
		if (value + neighbours[direction].value == 3) return true; //we can combine to make 3
		if (neighbours[direction].value == value && neighbours[direction].value >= 3) return true;	//3+ can combine with itself
		if ((value == JOKER && neighbours[direction].value >= 3) ||
			(neighbours[direction].value == JOKER && value >= 3))
			return true; //we have joker combination
		return false;
	}
	public void Move(byte direction) {
		if (!isActive) return;
		if(!CanMove(direction)) {
			nextValue = value;//we keep value because we stand still
			return;
		}
		neighbours[direction].AddValue(value);
		nextValue = INACTIVE;//we empty value because we leave cell
		isOccupied = false;
		//we start animation toward direction
	}
	
	public void AddValue(int _value) {
		if (nextValue == INACTIVE) {
			nextValue = _value;//move to empty
			return;
		}
		
		if (_value == 1 || _value == 2)	nextValue = 3;
		else if (value == JOKER) nextValue = _value * 2;
		else nextValue = value * 2;
		ScoreSystem.instance.Add(GetScoreFrom(nextValue));
	}
	
	public void UpdateInfo() {
		SetValue(nextValue);
		nextValue = INACTIVE;
	}
	
	public void SetValue (int _value) {
		value = _value;
		if (value == INACTIVE) {
			isOccupied = false;
			imgRef.color = INACTIVE_COLOR;
			textRef.text = "";
		}
		else if (value == JOKER) {
			isOccupied = true;
			imgRef.color = ACTIVE_COLOR;
			textRef.text = "J";
		}
		else {
			isOccupied = true;
			imgRef.color = ACTIVE_COLOR;
			textRef.text = value.ToString();
		}
	}
	
	int GetScoreFrom(int value) {
		if (value < 3) return 0;
		return (int)Mathf.Pow(3f,(Mathf.Log((float)value/3f,2f)+1f));
	}
	
}