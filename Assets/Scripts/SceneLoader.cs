using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneLoader : MonoBehaviour {

	void Awake() {
		GameObject.FindWithTag("Player").transform.position =
			UnityStandardAssets._2D.PlatformerCharacter2D.storePosition;
	}
}