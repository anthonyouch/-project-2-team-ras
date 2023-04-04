using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGun : MonoBehaviour
{
    [SerializeField] Transform firingPoint;

    [SerializeField] GameObject projectilePrefab;

    PlayerController player;

    [SerializeField] AudioClip shootingAudio;


    //[SerializeField] float firingSpeed;

    [SerializeField] float msBetweenShots;
    
    
    float nextShotTime;


    public static PlayerGun Instance;


    void Awake ()
    {
        Instance = GetComponent<PlayerGun>();
        
    }
    void Start ()
    {
        player = FindObjectOfType<PlayerController>();
    }


    public void Shoot()
    {
        if (Time.time > nextShotTime) {
            nextShotTime = Time.time + msBetweenShots / 1000;
            player.SetShooting();

            AudioManager.Instance.PlaySound(shootingAudio, player.transform.position);


            Instantiate(projectilePrefab, firingPoint.position, firingPoint.rotation);
        }
    }
}
