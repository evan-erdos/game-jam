using UnityEngine;
using System.Collections;
using EventArgs=System.EventArgs;

public class PlatformController : MonoBehaviour {

	Vector3 initial;

	public bool movingToTarget;
	public bool on;
	Vector3 speed;
	public float delay = 2f;
	public float endDelay = 0.1f;
	bool wait;
	//Vector3 currentTarget;

	public Lever[] pieces;

	public Vector3 Target {
		get { return (movingToTarget)?(target.localPosition):(initial); }
	}
	public Transform target;

	void Awake() {
		initial = transform.localPosition;
		foreach (var piece in pieces)
			piece.SolveEvent += OnSolve;
		if (!target) {
			throw new System.Exception ("Holy shit. Please assign target");
		}


	}

	// Use this for initialization
	void Start () {


	}

	public int OnSolve(
				IPiece<int> sender,
				EventArgs e,
				bool solved) {
		on = solved;
		return 0;
	}



	IEnumerator SwitchingDirection() {

		if (wait)
			yield break;

		wait = true;

		yield return new WaitForSeconds (endDelay);

		Debug.Log ("In coroutine after delay");

		movingToTarget = !movingToTarget;

		wait = false;

	}

	void FixedUpdate() {

		if (!on) return;

		if ((transform.localPosition - Target).magnitude < 1f) {
			Debug.Log ("Switching directions");
			StartCoroutine (SwitchingDirection ());
		}
		transform.localPosition = Vector3.SmoothDamp (transform.localPosition, Target, ref speed, delay);
	}


	// Update is called once per frame
	void Update () {

	}
}
