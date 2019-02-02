using UnityEngine;
using System.Collections;

public class CoinScript : MonoBehaviour {

	public int color = 1;

	private bool stopped = true;

	void Update () {
		if (stopped && ((GetComponent<Rigidbody2D> ().velocity.y >= 0 && transform.position.y < 1.5) || transform.position.y < -2.5)) {
			MainScript main = GameObject.Find ("Scripts").GetComponent<MainScript> ();
			stopped = false;
			main.cooldown = false;
		}

		if (transform.position.y < -2.5) {
			Destroy (gameObject);
		}
	}

	void OnTriggerEnter2D (Collider2D otherCollider) {
		TriggerScript trigger = otherCollider.gameObject.GetComponent<TriggerScript> ();
		MainScript main = GameObject.Find ("Scripts").GetComponent<MainScript> ();
		if ((int)trigger.location.y == 0 || main.board [(int)trigger.location.x, (int)trigger.location.y-1] != 0) {
			main.board [(int)trigger.location.x, (int)trigger.location.y] = color;
			if (color == 1) {
				main.color = 2;
				Debug.Log (trigger.location.x + ", " + trigger.location.y + " = Red");
				if (main.Check()) {
					main.win = true;
					Debug.Log ("Red wins!");
				}
			} else if (color == 2) {
				main.color = 1;
				Debug.Log (trigger.location.x + ", " + trigger.location.y + " = Blue");
				if (main.Check()) {
					main.win = true;
					Debug.Log ("Blue wins!");
				}
			}
		}
	}
}
