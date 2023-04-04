using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupMedKit : MonoBehaviour
{

    [SerializeField]
    UnityEngine.Tilemaps.Tilemap tilemap;

    [SerializeField] AudioClip pickUpMedAudio;


    private int healthAdded = 20;

    public void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player") {
            //Debug.Log("got midkit");
            other.gameObject.GetComponent<PlayerHealth>().AddHealth(healthAdded);
            AudioManager.Instance.PlaySound(pickUpMedAudio, transform.position);
            Destroy(this.gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        SpawnOnTile.SetRandomPosition(gameObject, tilemap);
        transform.Rotate(-90f, 0f, 180f);
        transform.Translate(0f, 0.125f, 0f, Space.World);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
