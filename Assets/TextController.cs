using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class TextController : MonoBehaviour {

	static Text text;

	// Use this for initialization
	void Start () {
		text = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public static void DisplayText (string s) {
		text.text = s;
		text.enabled = true;

	}

	public static void HideText() {
		text.enabled = false;
	}
}
