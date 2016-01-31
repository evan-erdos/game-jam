/* Ben Scott * bescott@andrew.cmu.edu * 2015-11-23 * Lever */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using EventArgs=System.EventArgs;

public class Lever : MonoBehaviour, IPiece<int> {
    bool wait;
    public bool isPlayerNear;
    public float time = 2f, dist = 2f, delay = 4f;
    float tgt, speed, handle_tgt, handle_speed;

    public Vector2 armRange = new Vector2(-20,20);
    public Vector2 handleRange = new Vector2(0,-15);

    [SerializeField] public AudioClip soundLever;
    [SerializeField] public AudioClip soundHandle;

    AudioSource _audio;
    GameObject arm, handle;


    public event OnSolve<int> SolveEvent {
        add { solveEvent += value; }
        remove { solveEvent -= value; }
    } event OnSolve<int> solveEvent;


    public bool IsSolved {
        get { return (Condition==Solution); } }


    public bool IsInitSolved {
        get { return isInitSolved; }
        set { isInitSolved = value; }
    } [SerializeField] bool isInitSolved;


    public bool IsLocked {
        get { return isLocked; }
        set { if (isLocked==value) return;
            isLocked = value;
            if (IsLocked)
                tgt = arm.transform.rotation.z;
        }
    } [SerializeField] bool isLocked = false;


    public int Condition {
        get { return condition; }
        set { condition = value; }
    } int condition;


    public int Solution {
        get { return solution; }
        set { solution = value; }
    } int solution;


    public int Selections { get { return 6; } }


    public float Theta {
        get { return theta; }
        set {
            theta = (theta<armRange.x)?(armRange.x):theta;
            theta = (theta>armRange.y)?(armRange.y):theta;
        }
    } float theta = -20f;


    public void Awake() {
        arm = transform.FindChild("arm").gameObject;
        handle = arm.transform.FindChild("handle").gameObject;
        _audio = GetComponent<AudioSource>();
        if (armRange.x>armRange.y)
            armRange = new Vector2(armRange.y,armRange.x);
        SolveEvent += this.OnSolve;
        //Solve(-1);

    }

    //public override void Start() { base.Start(); }

    void Update() {
        if (Input.GetButtonDown("Action") && isPlayerNear)
            StartCoroutine(Pulling(!IsSolved));
    }

    void FixedUpdate() {
        if (IsLocked) return;
        var target = Quaternion.Euler(0f,0f,tgt);
        arm.transform.localRotation = Quaternion.Slerp(
            arm.transform.localRotation,
            target, Time.deltaTime*5f);

        var angle = Quaternion.Euler(0f,0f,handle_tgt);
        handle.transform.localRotation = Quaternion.Slerp(
            handle.transform.localRotation,
            angle, Time.deltaTime*8f);
    }



    public int OnSolve(
                    IPiece<int> sender,
                    EventArgs e,
                    bool solved) {
        if (!IsSolved) return 0;
        //print("asdbvaslvkas");
        return Condition;
    }

    public virtual bool Push() {
        _audio.PlayOneShot(soundLever,0.2f);
        return Solve(Condition+1);
    }


    public virtual bool Pull() {
        _audio.PlayOneShot(soundLever,0.2f);
        return Solve(Condition-1);
    }

    IEnumerator Solving(bool t) {
        yield break;
        //while (arm.transform.rotation.y!=tgt) {
        //    yield return new WaitForEndOfFrame();
        //}
    }


    /** `Pulling` : **`coroutine`**
     *
     * Called with a boolean argument, specifies if the
     * lever should be pulled or pushed over a period of
     * `delay`. Also issues a `Terminal.Log()` message to
     * inform the player of the action taken. This is here
     * because once this is called, it is certain that the
     * event of pulling the lever is going to take place.
     **/
    IEnumerator Pulling(bool t) {
        if (wait) yield break;
        wait = true;
        //print("asdfasd");
        if (t) Pull();
        else Push();
        yield return new WaitForSeconds(delay);
        wait = false;
    }


    public bool Solve(int condition) {
        var wasSolved = IsSolved;
        Condition = condition;
        if (IsSolved!=wasSolved) {
            tgt = (IsSolved)?armRange.y:armRange.x;
            solveEvent(this,EventArgs.Empty,IsSolved);
        }
        return IsSolved;
    }

    public void OnTriggerEnter2D(Collider2D other) {
        if (!other.attachedRigidbody) return;
        if (other.attachedRigidbody.gameObject.tag!="Player") return;
        isPlayerNear = true;
    }

    public void OnTriggerExit2D(Collider2D other) {
        if (other.attachedRigidbody.gameObject.tag!="Player") return;
        isPlayerNear = false;
    }


    public void OnMouseExit() {
        handle_tgt = handleRange.x;
    }
}