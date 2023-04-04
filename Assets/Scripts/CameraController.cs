using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraController : MonoBehaviour
{


    Transform player;
    [SerializeField]
    Vector3 offset;
    Transform[] obstructions;

    private int oldHitsNumber;

    void Start()
    {
        Player pl = FindObjectOfType<Player>();
        player = pl.transform;
        oldHitsNumber = 0;
    }

    private void LateUpdate()
    {
        if (player != null){
            viewObstructed();
        }
    }

    void Update()
    {
        if (player != null){       
             transform.position = player.position + offset;
            transform.LookAt(player);
        }
        if(Input.GetKeyDown(KeyCode.Escape)){
            SceneManager.LoadScene("StartScene");
        }
    }

    void viewObstructed()
    {
        float characterDistance = Vector3.Distance(transform.position, player.position);
        
        int layerMask = LayerMask.GetMask("Wall");

        RaycastHit[] hits = Physics.RaycastAll(transform.position, player.position - transform.position, characterDistance, layerMask);
        if (hits.Length > 0)
        {   
            // Means that some stuff is blocking the view
            int newHits = hits.Length - oldHitsNumber;

            if (obstructions != null && obstructions.Length > 0 && newHits<oldHitsNumber)
            {
                // Repaint all the previous obstructions. Because some of the stuff might be not blocking anymore
                for (int i = 0; i < obstructions.Length; i++)
                {
                   var newColor =  obstructions[i].gameObject.GetComponent<MeshRenderer>().material.color;
                   newColor.a = 1f;

                   obstructions[i].gameObject.GetComponent<MeshRenderer>().material.color = newColor;
                }
            }
            
            
            obstructions = new Transform[hits.Length];
            // Hide the current obstructions 
            for (int i = 0; i < hits.Length; i++)
            {
                Transform obstruction = hits[i].transform;

                var newColor =  obstruction.gameObject.GetComponent<MeshRenderer>().material.color;
                newColor.a = 0.5f;
                obstruction.gameObject.GetComponent<MeshRenderer>().material.color = newColor;
                obstructions[i] = obstruction;
            }
            oldHitsNumber = hits.Length;

            
        }
        else
        {   // Mean that no more stuff is blocking the view and sometimes all the stuff is not blocking as the same time
            if (obstructions != null && obstructions.Length > 0)
            {
                for (int i = 0; i < obstructions.Length; i++)
                {
                    var newColor =  obstructions[i].gameObject.GetComponent<MeshRenderer>().material.color;
                    newColor.a = 1f;
                   
                    obstructions[i].gameObject.GetComponent<MeshRenderer>().material.color = newColor;
                }
                oldHitsNumber = 0;
                obstructions = null;
            }
        }
    }
}

