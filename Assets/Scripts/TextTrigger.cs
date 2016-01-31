using UnityEngine;
using System.Collections;

public class TextTrigger : MonoBehaviour {

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

        TextController.DisplayText(text);
    }

    public void OnTriggerExit2D(Collider2D other) {
        if (other.attachedRigidbody.gameObject.tag!="Player") return;
        TextController.HideText();
    }
}
