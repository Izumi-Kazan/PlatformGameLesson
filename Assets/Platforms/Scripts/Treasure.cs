using UnityEngine;
using System.Collections;

public class Treasure : MonoBehaviour {

	GameObject gameController;
	
	void Start() {
		gameController = GameObject.Find("GameController");
	}
	
	void OnTriggerEnter(Collider outer) {
		gameController.SendMessage("TreasureHit");
		Destroy(this.gameObject);
	}
}
