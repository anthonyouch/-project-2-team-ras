using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    // private Vector3 firingPoint;

    [SerializeField] float projectileSpeed;
    [SerializeField] float maxProjectileDistance;
    [SerializeField] int damage;

    public LayerMask collisionMask;
    // lifetime if needed

    // float lifetime = 3;

    // Start is called before the first frame update
    void Start()
    {
        // Destroy(this.gameObject, lifetime);
        // firingPoint =  transform.position;

        Collider[] initialCollisions = Physics.OverlapSphere(transform.position, .1f, collisionMask);
        if (initialCollisions.Length > 0) {
            OnHitObject(initialCollisions[0]);
        }      
        
    }

    // Update is called once per frame
    void Update()
    {
        
        MoveProjectile();
        
    }

    void MoveProjectile() 
    {
        // if (Vector3.Distance(firingPoint, transform.position) > maxProjectileDistance)
        // {
        //     Destroy(this.gameObject);
        // }

        float moveDistance = projectileSpeed * Time.deltaTime;
        CheckCollisions (moveDistance);
        transform.Translate(Vector3.forward * projectileSpeed * Time.deltaTime);

    }

    void CheckCollisions(float moveDistance) {
		Ray ray = new Ray (transform.position, transform.forward);
		RaycastHit hit;

		if (Physics.Raycast(ray, out hit, moveDistance, collisionMask, QueryTriggerInteraction.Collide)) {
			OnHitObject(hit.collider);
		}
	}
    
    void OnHitObject(Collider other) {
        
        //Debug.Log("HIIIII");

        // if it's already not dead 
        if (other.gameObject.tag == "Enemy" ) {
            if (!other.gameObject.GetComponent<EnemyController>().isDead()) {

                other.gameObject.GetComponent<EnemyHealth>().HurtEnemy(damage);   
                Destroy(this.gameObject);
                
            }
            
        }
        
    }
    void OnCollisionEnter(Collision other) {
        Destroy(this.gameObject);
    }
}
