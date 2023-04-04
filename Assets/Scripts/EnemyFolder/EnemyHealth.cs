using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{

    [SerializeField] int health;
    private int currentHealth;
    private Animator animator;
    private EnemyController enemy;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        currentHealth = health;
        enemy = GetComponent<EnemyController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHealth <= 0) {
            enemy.stopRunningAudio();
            animator.Play("dead");
            enemy.setDeath();
            Destroy(this.gameObject, 3f);
        }
    }

    public void HurtEnemy(int damageTaken) {
        currentHealth -= damageTaken;
    }
}
