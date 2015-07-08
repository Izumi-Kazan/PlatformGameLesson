using UnityEngine;
using System.Collections;

public class TrapEvent : MonoBehaviour {

	void StartTrapMovingAnimation() {
		// シーンの中からTrapMovingオブジェクトにアクセスし、デフォルトのアニメーションを実行させる
		GameObject.Find("TrapMoving").GetComponent<Animation>().Play();
	}
}
