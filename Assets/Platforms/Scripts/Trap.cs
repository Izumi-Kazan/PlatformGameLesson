using UnityEngine;
using System.Collections;

public class Trap : MonoBehaviour {
	
	GameObject gameController;
	
	void Start() {
		gameController = GameObject.Find("GameController");
	}
	
	void OnTriggerEnter(Collider outer) {
		Destroy(outer.gameObject);
		gameController.SendMessage("TrapHit");
	}
}
