using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof (PlayerController))]

public class Player : MonoBehaviour
{

    [SerializeField]
    Tilemap floorTilemap;


    [SerializeField] private float movementSpeed;
    PlayerController controller;
    Camera viewCamera;
    

    // Start is called before the first frame update
    void Start()
    {
        
        controller = GetComponent<PlayerController>();
        SpawnOnTile.SetRandomPosition(gameObject, floorTilemap);
        viewCamera = Camera.main;
        
    }

    // Update is called once per frame
    void Update()
    {

        //Movement
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 movementInput = new Vector3(horizontal, 0, vertical);
        Vector3 movementVelocity = movementInput.normalized * movementSpeed;
        controller.Move(movementVelocity);


        // Look 
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            Vector3 rotation = new Vector3(hit.point.x, transform.position.y, hit.point.z);
            controller.LookAt(rotation);
        }

        // Weapon
        if (Input.GetButton("Fire1")) {
            PlayerGun.Instance.Shoot();
        }


    }
}
