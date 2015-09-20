using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

using BorderTools;
using Random = UnityEngine.Random;

public class BoardManager : MonoBehaviour {

	//Elem prefab const field
	public const int NR_ROWS = 4;
	public const int NR_COL = 4;
	const string ELEM_PREFAB_PATH = "Prefabs/ElemPrefab";
	const float ELEM_PREFAB_WIDTH = 250f;
	const float ELEM_PREFAB_HEIGHT = 250f;
	
	//World Ref
	public RectTransform panelRef;
	GameManager gameRef;
	int rows,cols; //we store current setup in case the default values are not used
	ElemHandler[,] fullBoard; //here we store the list of board cells
	List<ElemHandler> activeElem; //here we store the list of active tiles
	
	public void Init(GameManager _gameRef, int _rows = NR_ROWS, int _cols = NR_COL) {
		gameRef = _gameRef;
		rows = _rows;
		cols = _cols;
		
		//we scale the elements instead of resizing so the text remains sharp
		float offSetX = 0f,offSetY = 0f;
		//Rect panelRect = RectTransformUtility.PixelAdjustRect(_panelRef,_panelRef.root.GetComponent<Canvas>());
		float elemWidth = panelRef.rect.width/cols;
		float elemWidthScale = elemWidth/ELEM_PREFAB_WIDTH;
		float elemHeight = panelRef.rect.height/rows;
		float elemHeightScale = elemHeight/ELEM_PREFAB_HEIGHT;
		
		//we create the board
		GameObject tmpElem;
		fullBoard = new ElemHandler[rows,cols];
		for( int x = 0; x < rows; x++) {
			for (int y = 0; y < cols; y++) {
				tmpElem = GameObject.Instantiate<GameObject> (Resources.Load<GameObject> (ELEM_PREFAB_PATH));
				tmpElem.transform.SetParent(panelRef,true);
				tmpElem.transform.localPosition = new Vector3(offSetX,offSetY,0);
				offSetX += elemWidth;
				fullBoard[x,y] = tmpElem.GetComponent<ElemHandler>();
				fullBoard[x,y].Init(ElemHandler.INACTIVE,HasBorder(x,y,rows,cols),elemWidthScale,elemHeightScale);
			}
			offSetX = 0;
			offSetY -= elemHeight;
		}
		ElemHandler elemRef;
		for (int x = 0; x < rows; x++) {
			for (int y = 0; y < cols; y++) {
				elemRef = fullBoard[x,y];
				if (!elemRef.border.Has(DirectionConstants.LEFT)) elemRef.neighbours[DirectionConstants.LEFT] = fullBoard[x,y-1];
				if (!elemRef.border.Has(DirectionConstants.RIGHT)) elemRef.neighbours[DirectionConstants.RIGHT] = fullBoard[x,y+1];
				if (!elemRef.border.Has(DirectionConstants.UP)) elemRef.neighbours[DirectionConstants.UP] = fullBoard[x-1,y];
				if (!elemRef.border.Has(DirectionConstants.DOWN)) elemRef.neighbours[DirectionConstants.DOWN] = fullBoard[x+1,y];
			}
		}
		Populate();
	}
	
	void Populate() {
		//we create the ones with values
		activeElem = new List<ElemHandler>();
		int activElemCount = Random.Range(1,7);
		Vector2 toAdd;
		for (int i = 0; i < activElemCount; i++) {
			toAdd = GetRandomEmpty();
			fullBoard[(int)toAdd.x,(int)toAdd.y].SetValue(Random.Range(1,4));
			activeElem.Add(fullBoard[(int)toAdd.x,(int)toAdd.y]);
		}
	}
	
	public void Move(byte direction) {
		//first we check if it's a legal move
		bool canMove = false;
		foreach (ElemHandler elem in activeElem) {
			if (elem.CanMove(direction)) {
				canMove = true;
				break;
			}
		}
		if (!canMove) return; //not a legal move so we ignore it
		
		//row/col from where we start
		//int x = 0,intY = 0,operation = 1;
		switch (direction) {
			case DirectionConstants.LEFT:
				for (int y = 0; y < cols; y++) {
					for (int x = 0; x <rows; x++) {
						fullBoard[x,y].Move(direction);
					}
				}
			break;
			case DirectionConstants.RIGHT:
				for (int y = cols - 1; y >= 0; y--) {
					for (int x = rows - 1; x >= 0; x--) {
						fullBoard[x,y].Move(direction);
					}
				}
				break;
			case DirectionConstants.UP:
				for (int x = 0; x <rows; x++) {
					for (int y = 0; y < cols; y++) {
						fullBoard[x,y].Move(direction);
					}
				}
				break;
			case DirectionConstants.DOWN:
				for (int x = rows - 1; x >= 0; x--) {
					for (int y = cols - 1; y >= 0; y--) {
						fullBoard[x,y].Move(direction);
					}
				}
				break;
		}
		OnMoveEnd();
	}
	public void OnMoveEnd() {
		//we add the numers + send score
		activeElem.Clear();
		for (int x = 0; x < rows; x++) {
			for (int y = 0; y < cols; y++) {
				fullBoard[x,y].UpdateInfo();
				if (fullBoard[x,y].isActive) activeElem.Add(fullBoard[x,y]);
			}
		}
		
		//we add the extra tile
		Vector2 newTile = GetRandomEmpty();
		int newValue = 0;
		if (Random.Range(0,19) == 0) {
			newValue = ElemHandler.JOKER;
		}
		else if (Random.Range(0,9) == 0) {
			newValue = 2;
		}
		else {
			newValue = 1;
		}
		fullBoard[(int)newTile.x,(int)newTile.y].SetValue(newValue);
		activeElem.Add(fullBoard[(int)newTile.x,(int)newTile.y]);
		
		//we check if any legal moves can be done
		if(!IsBoardFull()) return;
		
		bool hasLegalMove = false;
		foreach (ElemHandler elem in activeElem) {
			if (elem.CanMove(DirectionConstants.LEFT)) {
				hasLegalMove = true;
				break;
			}
			if (elem.CanMove(DirectionConstants.RIGHT)) {
				hasLegalMove = true;
				break;
			}
			if (elem.CanMove(DirectionConstants.UP)) {
				hasLegalMove = true;
				break;
			}
			if (elem.CanMove(DirectionConstants.DOWN)) {
				hasLegalMove = true;
				break;
			}
		}
		if (!hasLegalMove) gameRef.EndGame();
	}
	
	public void Restart() {
		Clean();
		Populate();
		ScoreSystem.instance.Reset();
	}
	public void Clean() {
		activeElem.Clear ();
		foreach (ElemHandler elem in fullBoard) {
			elem.SetValue(ElemHandler.INACTIVE);
		}
	}
	
	Border HasBorder(int x, int y, int rows, int cols) {
		Border border = new Border();
		if (x == 0) border.Add(DirectionConstants.UP);
		if (y == 0) border.Add(DirectionConstants.LEFT);
		if (x == rows-1) border.Add(DirectionConstants.DOWN);
		if (y == cols-1) border.Add(DirectionConstants.RIGHT);
		return border;
	}
	bool IsBoardFull() {
		if (activeElem.Count == fullBoard.Length) return true;
		return false;
	}
	//normally here we call a difficulty system to provide us with a good place (and value) for the next tile but for this we're going with random
	Vector2 GetRandomEmpty() {
		int x,y;
		if (IsBoardFull()) {
			Debug.LogError("getting empty square but we found none");
			return new Vector2 (-1,-1);
		}
		while(true) {
			x = Random.Range (0,rows);
			y = Random.Range (0,cols);
			if (fullBoard[x,y].isOccupied) continue;
			fullBoard[x,y].isOccupied = true;
			return new Vector2(x,y);  
		}
	}
}
