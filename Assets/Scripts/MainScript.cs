using UnityEngine;
using System.Collections;

public class MainScript : MonoBehaviour {

	public int[,] board = new int[7, 6];
	public bool cooldown = false;
	public bool win = false;
	public int color = 1;

	public Transform redCoinPrefab;
	public Transform blueCoinPrefab;

	public Texture2D redCursorTexture;
	public Texture2D blueCursorTexture;

	void Start () {
		for (int i = 0; i < 7; i++){
			for (int j = 0; j < 6; j++){
				board[i, j] = 0;
			}
		}
	}

	void Update () {
		if (Input.GetMouseButtonDown (0) && !cooldown && Camera.main.ScreenToWorldPoint (Input.mousePosition).y > 1.5 && !win) {
			if (color == 1) {
				var redCoinTransform = Instantiate (redCoinPrefab) as Transform;
				Vector3 v = Camera.main.ScreenToWorldPoint (Input.mousePosition);
				v.z = 10;
				redCoinTransform.position = v;

				cooldown = true;
			} else if (color == 2) {
				var blueCoinTransform = Instantiate (blueCoinPrefab) as Transform;
				Vector3 v = Camera.main.ScreenToWorldPoint (Input.mousePosition);
				v.z = 10;
				blueCoinTransform.position = v;
				
				cooldown = true;
			}
		}
	}

	void OnGUI () {
		if (!cooldown && !win) {
			if (color == 1) {
				GUI.DrawTexture (new Rect (Event.current.mousePosition.x - 32.5f, Event.current.mousePosition.y - 32.5f, 65, 65), redCursorTexture);
			} else if (color == 2) {
				GUI.DrawTexture (new Rect (Event.current.mousePosition.x - 32.5f, Event.current.mousePosition.y - 32.5f, 65, 65), blueCursorTexture);
			}
		}
	}

	public bool Check () {
		//Horizontal
		for (int x = 0; x <= 3; x++) {
			for (int y = 0; y <= 5; y++) {
				if (board [x, y] != 0 &&
					board [x, y] == board [x + 1, y] &&
					board [x + 1, y] == board [x + 2, y] &&
					board [x + 2, y] == board [x + 3, y]) {
					return true;
				}
			}
		}
		//Vertical
		for (int x = 0; x <= 6; x++) {
			for (int y = 0; y <= 2; y++) {
				if (board [x, y] != 0 &&
				    board [x, y] == board [x, y + 1] &&
				    board [x, y + 1] == board [x, y + 2] &&
				    board [x, y + 2] == board [x, y + 3]) {
					return true;
				}
			}
		}
		//Positive diagonal
		for (int x = 0; x <= 3; x++) {
			for (int y = 0; y <= 2; y++) {
				if (board [x, y] != 0 &&
				    board [x, y] == board [x + 1, y + 1] &&
				    board [x + 1, y + 1] == board [x + 2, y + 2] &&
				    board [x + 2, y + 2] == board [x + 3, y + 3]) {
					return true;
				}
			}
		}
		//Negative diagonal
		for (int x = 0; x <= 3; x++) {
			for (int y = 3; y <= 5; y++) {
				if (board [x, y] != 0 &&
				    board [x, y] == board [x + 1, y - 1] &&
				    board [x + 1, y - 1] == board [x + 2, y - 2] &&
				    board [x + 2, y - 2] == board [x + 3, y - 3]) {
					return true;
				}
			}
		}
		return false;
	}
}