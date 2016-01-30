using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

[RequireComponent(typeof(Collider2D))]
public class SwitchArea : MonoBehaviour {

	bool wait, isPlayerNear;
	float delay = 3f;
	int n = 0;
	public string level = "Forest";
	GameObject player;

	void Awake() {
		GetComponent<Collider2D>().isTrigger = true;
	}


	IEnumerator Switching() {
		if (wait) yield break;
		wait = true;
		var scene = SceneManager.GetSceneByName(level+" "+n);
		SceneManager.LoadSceneAsync(level+" "+n);
		yield return new WaitForSeconds(delay);
		SceneManager.MoveGameObjectToScene(player,scene);
		SceneManager.SetActiveScene(scene);
		SceneManager.UnloadScene(
			SceneManager.GetActiveScene().buildIndex);
		wait = false;
	}

	void Update() {
		if (Input.GetButtonDown("Special") && isPlayerNear)
			StartCoroutine(Switching());
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (!other.attachedRigidbody) return;
		if (other.attachedRigidbody.gameObject.tag!="Player") return;
		player = other.attachedRigidbody.gameObject;
		isPlayerNear = true;
	}

	void OnTriggerExit2D(Collider2D other) {
		if (!other.attachedRigidbody) return;
		if (other.attachedRigidbody.gameObject.tag!="Player") return;
		player = null;
		isPlayerNear = false;
	}

}
