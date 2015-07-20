using UnityEngine;
using System.Collections;

public class TrapEvent : MonoBehaviour {

    public GameObject target;

	void StartTrapMovingAnimation() {
        // シーンの中からTrapMovingオブジェクトにアクセスし、デフォルトのアニメーションを実行させる
        //myAnimation.Play();

        target.GetComponent<Animation>().Play();
    }
}
