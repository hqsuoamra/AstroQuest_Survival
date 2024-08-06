using UnityEngine;

public class AnkleGrabber : MonoBehaviour {
    private Animator anim;
    public Transform player;
    public float detectionRange = 5f;
    public HealthBarFollow healthBarFollow;
    public Vector3 healthBarOffset = new Vector3(0, 2, 0); // Adjust the offset as needed

    int IdleOne;
    int IdleAlert;
    int Sleeps;
    int AngryReaction;
    int Hit;
    int AnkleBite;
    int CrochBite;
    int Dies;
    int HushLittleBaby;
    int Run;
    int AggressiveBite;
    int AggressiveGrab;
    

    void Start () {
        anim = GetComponent<Animator> ();
        IdleOne = Animator.StringToHash("IdleOne");
        IdleAlert = Animator.StringToHash("IdleAlert");
        Sleeps = Animator.StringToHash("Sleeps");
        AngryReaction = Animator.StringToHash("AngryReaction");
        Hit = Animator.StringToHash("Hit");
        AnkleBite = Animator.StringToHash("AnkleBite");
        CrochBite = Animator.StringToHash("CrochBite");
        Dies = Animator.StringToHash("Dies");
        HushLittleBaby = Animator.StringToHash("HushLittleBaby");
        Run = Animator.StringToHash("Run");
        AggressiveBite = Animator.StringToHash("AggressiveBite");
        AggressiveGrab = Animator.StringToHash("AggressiveGrab");

        // Set the health bar target and offset
        if (healthBarFollow != null) {
            healthBarFollow.offset = healthBarOffset;
        }
    }

    void Update () {
        float distanceToPlayer = Vector3.Distance(player.position, transform.position);
        
        if (distanceToPlayer <= detectionRange) {
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("IdleOne")) {
                anim.SetBool(IdleAlert, true);
                anim.SetBool(IdleOne, false);
            }
            
            if (Input.GetKeyDown(KeyCode.A)) {
                if (anim.GetCurrentAnimatorStateInfo(0).IsName("IdleAlert")) {
                    anim.SetBool(IdleAlert, false);
                    anim.SetBool(AggressiveBite, true);
                }
            } else if (Input.GetKeyUp(KeyCode.A)) {
                if (anim.GetCurrentAnimatorStateInfo(0).IsName("AggressiveBite")) {
                    anim.SetBool(AggressiveBite, false);
                    anim.SetBool(IdleAlert, true);
                }
            }

            if (Input.GetKeyDown(KeyCode.S)) {
                if (anim.GetCurrentAnimatorStateInfo(0).IsName("IdleAlert")) {
                    anim.SetBool(IdleAlert, false);
                    anim.SetBool(AggressiveGrab, true);
                }
            } else if (Input.GetKeyUp(KeyCode.S)) {
                if (anim.GetCurrentAnimatorStateInfo(0).IsName("AggressiveGrab")) {
                    anim.SetBool(AggressiveGrab, false);
                    anim.SetBool(IdleAlert, true);
                }
            }

            if (Input.GetKeyDown(KeyCode.Q)) {
                if (anim.GetCurrentAnimatorStateInfo(0).IsName("IdleAlert")) {
                    anim.SetBool(IdleAlert, false);
                    anim.SetBool(AnkleBite, true);
                }
            } else if (Input.GetKeyUp(KeyCode.Q)) {
                if (anim.GetCurrentAnimatorStateInfo(0).IsName("AnkleBite")) {
                    anim.SetBool(AnkleBite, false);
                    anim.SetBool(IdleAlert, true);
                }
            }

            if (Input.GetKeyDown(KeyCode.W)) {
                if (anim.GetCurrentAnimatorStateInfo(0).IsName("IdleAlert")) {
                    anim.SetBool(IdleAlert, false);
                    anim.SetBool(CrochBite, true);
                }
            } else if (Input.GetKeyUp(KeyCode.W)) {
                if (anim.GetCurrentAnimatorStateInfo(0).IsName("CrochBite")) {
                    anim.SetBool(CrochBite, false);
                    anim.SetBool(IdleAlert, true);
                }
            }
        } else {
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("IdleAlert")) {
                anim.SetBool(IdleAlert, false);
                anim.SetBool(IdleOne, true);
            }
        }
    }
}
