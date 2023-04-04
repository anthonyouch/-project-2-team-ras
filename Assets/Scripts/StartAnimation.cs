using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartAnimation : MonoBehaviour
{

    [SerializeField] EnemyController enemy;

    public void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player") {
           Debug.Log("started attacking");
           enemy.setAttacking();
        }
    }
    public void OnTriggerExit(Collider other) {
        
        if (other.gameObject.tag == "Player") {
            Debug.Log("exited zone");
            enemy.setRunning();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
