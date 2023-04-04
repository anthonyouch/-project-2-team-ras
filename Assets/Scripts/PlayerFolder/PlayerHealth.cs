using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{

    [SerializeField] PlayerController player;
    [SerializeField] int playerHealth;
    private int currentPlayerHealth;
    private Animator animator;

    [SerializeField] HealthBar healthBar;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerController>();
        currentPlayerHealth = playerHealth;
        healthBar.SetMaxHealth(playerHealth);
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (currentPlayerHealth <= 0) {
            player.SetDeath();
            //animator.SetBool("isDead", true);
            animator.Play("Die");
            Destroy(this.gameObject,3);
        }
    }

    public void HurtPlayer(int damageTaken) {
        currentPlayerHealth -= damageTaken;

        healthBar.SetHealth(currentPlayerHealth);
    }
    public void AddHealth(int healthAdded) {
        currentPlayerHealth += healthAdded;
        healthBar.SetHealth(currentPlayerHealth);
    }

}
