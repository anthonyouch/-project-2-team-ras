using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[RequireComponent (typeof (Rigidbody))]

public class PlayerController : MonoBehaviour
{
    
    private Animator animator;
    Rigidbody myRigidbody;

    Vector3 velocity;

    public enum State {Idle, Running, Shooting, Dead};
    State currentState;


    public AudioSource footstepSound;

    //[SerializeField] AudioClip runningAudio;

    // Start is called before the first frame update
    void Start()
    { 
        
        animator = GetComponent<Animator>();
        myRigidbody = GetComponent<Rigidbody>();
        footstepSound = GetComponent<AudioSource>();
        footstepSound.enabled = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        //HandleMovementInput();
        //LookaAt();
        //HandleShootInput();
    }

    void FixedUpdate() 
    {

        if (currentState != State.Dead) {
            
            if (velocity != Vector3.zero) {
                
                 if (currentState != State.Running) {
                    footstepSound.volume = AudioManager.Instance.getVolume();;
                    footstepSound.enabled = true;
                     //AudioManager.Instance.PlaySound(runningAudio, transform.position);
                 }


                if (currentState != State.Shooting)  {
                    currentState = State.Running;
                    animator.Play("Run_guard_AR");
                    
                    
                } else {
                    // shooting animation whle moving
                    StartCoroutine(ShootRun());
                    //animator.Play("Run_gun_Middle_AR");
                    //currentState = State.Running;
                }
                
                
                myRigidbody.MovePosition(myRigidbody.position + velocity * Time.fixedDeltaTime);

            } else {
                 if (currentState == State.Running) {
                    footstepSound.enabled = false;
                //     AudioSource.Stop(runningAudio);
                }
                if (currentState != State.Shooting){
                    currentState = State.Idle;
                    animator.Play("Idle_Guard_AR");
                } else {
                    // shooting animation while idle
                    StartCoroutine(Shoot());
                    //animator.Play("Shoot_SingleShot_AR");
                    //currentState = State.Idle;
                }
                
            }
        }
    }

    IEnumerator ShootRun() {
    
        animator.Play("Run_gun_Middle_AR");
        float percent = 0;
        float attackSpeed = 5;
        while (percent <= 1) {
            percent += Time.deltaTime * attackSpeed;
            yield return null;
        }
        //animator.Play("run");
        currentState = State.Running;
    }

    IEnumerator Shoot() {
    
        animator.Play("Shoot_SingleShot_AR");
        float percent = 0;
        float attackSpeed = 5;
        while (percent <= 1) {
            percent += Time.deltaTime * attackSpeed;
            yield return null;
        }
        //animator.Play("run");
        currentState = State.Idle;
    }


    public void Move(Vector3 _velocity)
    {
        velocity = _velocity;

    }

    public void LookAt(Vector3 lookPoint) 
    {
        transform.LookAt(lookPoint);
        
    }

    public void SetShooting() 
    {
        currentState = State.Shooting;
    }
    public void SetDeath()
    {
        currentState = State.Dead;
    }
    public bool CheckDeath()
    {
        return currentState == State.Dead;
    }



}
