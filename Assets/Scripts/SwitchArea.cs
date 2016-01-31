using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

[RequireComponent(typeof(Collider2D))]
public class SwitchArea : MonoBehaviour {

	bool wait, isPlayerNear;
	float delay = 0.1f;
	int n = 0;
	public string level = "Forest";
	GameObject player;

	void Awake() {
		GetComponent<Collider2D>().isTrigger = true;
	}


	IEnumerator Switching() {
		if (wait) yield break;
		wait = true;
		UnityStandardAssets._2D.PlatformerCharacter2D.storePosition = player.transform.position;
		var scene = SceneManager.GetSceneByName(level+" "+n);
		SceneManager.LoadSceneAsync(level+" "+n);
		yield return new WaitForSeconds(delay);
		SceneManager.MoveGameObjectToScene(player,scene);
		print("AAAAA"+player.transform.position);
		//player.transform.position = position;
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