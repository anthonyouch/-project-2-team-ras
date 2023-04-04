using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(UnityEngine.AI.NavMeshAgent))]    


public class EnemyController : MonoBehaviour
{

    private Rigidbody myRigidbody;
    [SerializeField] float movementSpeed;
    [SerializeField] public PlayerController player;
    [SerializeField] PlayerHealth playerHealth;

    private Animator animator;
    float attackDistanceThreshold = 1.5f;
    float timeBetweenAttacks = 1;
    float nextAttackTime;

    Transform target;
    float myCollisionRadius;
    float targetCollisionRadius;

    public enum State {Running, Attacking, Dead};
    State currentState;

    UnityEngine.AI.NavMeshAgent pathfinder;

    public AudioSource runningAudio;


    // Start is called before the first frame update
    void Start()
    {
        runningAudio = GetComponent<AudioSource>();
        runningAudio.enabled = false;
        pathfinder = GetComponent<UnityEngine.AI.NavMeshAgent>();
        myRigidbody = GetComponent<Rigidbody>();
        player = FindObjectOfType<PlayerController>();
        playerHealth = FindObjectOfType<PlayerHealth>();
        animator = GetComponent<Animator>();
        currentState = State.Running;
        
        if (GameObject.FindWithTag("Player")!= null){
        target = GameObject.FindWithTag("Player").transform;
        } else {return;}
    
        setRunning();

        myCollisionRadius = GetComponent<CapsuleCollider> ().radius;
        targetCollisionRadius = target.GetComponent<CapsuleCollider> ().radius;


        if (target != null){
            StartCoroutine(UpdatePath());
        }

    }

    void FixedUpdate() {

        //if (currentState == State.Running) {
            //Vector3 forwards = new Vector3(transform.forward.x, 0, transform.forward.z);
            //transform.Translate(forwards * movementSpeed * Time.deltaTime, Space.World);
       // }
    }


    // Update is called once per frame
    void Update()
    {
        // Look at player

        // add if condition for also player not dead
        // if (currentState != State.Dead) {
        //     //Vector3 dirToTarget = (target.position - transform.position).normalized;
        //     transform.LookAt(new Vector3(target.position.x, 0, target.position.z));
        //     Debug.Log(new Vector3(target.position.x, 0, target.position.z));
        //     //transform.LookAt(target.position - dirToTarget * (myCollisionRadius + targetCollisionRadius + attackDistanceThreshold/2));
        // }

        if (currentState != State.Dead) {
            if (target != null){

                float sqrDstToTarget = (target.position - transform.position).sqrMagnitude;
            
                if (sqrDstToTarget < myCollisionRadius * 300) {
                runningAudio.volume = AudioManager.Instance.getVolume();
                runningAudio.enabled = true;
                } else {
                runningAudio.enabled = false;
                }
            } else {runningAudio.enabled = false;}
        }


        // Attack
        if (currentState != State.Dead && Time.time > nextAttackTime && target != null) {
            float sqrDstToTarget = (target.position - transform.position).sqrMagnitude;

            if (sqrDstToTarget < Mathf.Pow(attackDistanceThreshold + myCollisionRadius + targetCollisionRadius, 2)) {
                nextAttackTime = Time.time + timeBetweenAttacks;
                StartCoroutine(Attack());
            }
        }
        
    }

    IEnumerator UpdatePath() {
        float refreshRate = 0.10f;
        while (target != null) {
            //Vector3 targetPosition = new Vector3(target.position.x, 0, target.position.z);
            Vector3 dirToTarget = (target.position - transform.position).normalized;
			Vector3 targetPosition = target.position - dirToTarget * (myCollisionRadius + targetCollisionRadius + attackDistanceThreshold/2);
            //Debug.Log(targetPosition);
            if (currentState == State.Dead) {
                targetPosition = Vector3.zero;
            }
            if (currentState == State.Running && targetPosition != null) {
                pathfinder.destination = targetPosition;
            }
            yield return new WaitForSeconds(refreshRate);

        }
    }
    

    IEnumerator Attack() {
        
        currentState = State.Attacking;
        animator.Play("attack");
        Vector3 originalPosition = transform.position;
        pathfinder.enabled = false;

        Vector3 dirToTarget = (target.position - transform.position).normalized;
        //Vector3 attackPosition = target.position;
        Vector3 attackPosition = target.position - dirToTarget * (myCollisionRadius);

        float percent = 0;
        float attackSpeed = 3;
 
        bool appliedDamage = false;

        while (percent <= 1) {
            
            if (percent >= 0.5f && !appliedDamage) {
                appliedDamage = true;
                playerHealth.HurtPlayer(20);
            }

            percent += Time.deltaTime * attackSpeed;
            float interpolation = (-Mathf.Pow(percent, 2) + percent)*4;
            transform.position = Vector3.Lerp(originalPosition, attackPosition, interpolation);
            yield return null;
        }
        //animator.Play("run");
        currentState = State.Running;
        pathfinder.enabled = true;
    }

    // animations

    public void setDeath() 
    {   
        //myRigidbody.isKinematic = true;
        //myRigidbody.detectCollisions = false;
        currentState = State.Dead;
    }

    public bool isDead()
    {
        return currentState == State.Dead;
    }

    public void setAttacking()
    {
        animator.SetBool("isRunning", false);
        animator.SetBool("isAttacking", true);
    }
    public void setRunning()
    {

        //Debug.Log("started running");
        animator.SetBool("isAttacking", false);
        animator.SetBool("isRunning", true);
    }

    public bool getIsAttacking() 
    {
        return animator.GetBool("isAttacking");
    }
    /* TODO 
      FIX iSATTCKING
    
    */
    
    public void stopRunningAudio() {
        runningAudio.enabled = false;
    }


    public PlayerController GetPlayer(){return player;}
}
