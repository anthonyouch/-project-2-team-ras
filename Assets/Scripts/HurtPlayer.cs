using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtPlayer : MonoBehaviour
{
    [SerializeField] int damageTaken;
    [SerializeField] EnemyController enemy;


    public void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player") {
           Debug.Log("got hit");
           other.gameObject.GetComponent<PlayerHealth>().HurtPlayer(damageTaken);
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
