using UnityEngine;
using System.Collections;

public class TextTrigger : MonoBehaviour {

	bool isPlayerNear;

	public string text;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnTriggerEnter2D(Collider2D other) {
        if (!other.attachedRigidbody) return;
        if (other.attachedRigidbody.gameObject.tag!="Player") return;
        isPlayerNear = true;

        TextController.DisplayText(text);
    }

    public void OnTriggerExit2D(Collider2D other) {
        if (other.attachedRigidbody.gameObject.tag!="Player") return;
        isPlayerNear = false;

        TextController.HideText();
    }
}
