using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    //Script responsável por determinar o funcionamento de um projétil

    public float speed;
    public float lifetime;
    public float distance;
    public LayerMask whatIsSolid;

    void Start()
    {
        Invoke("DestroyProjectile", lifetime);
    }

    void Update()
    {
        //Calcula um raycast na direção especificada
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.up, distance);

        //Se bater em algo, se esse algo for o jogador, dá dano, e então destrói o projétil
        if (hitInfo.collider != null){
            if (hitInfo.collider.tag == "Player"){
                Debug.Log("the player took damage!");
            }
            DestroyProjectile();
        }
        //Move o projétil
        transform.Translate(transform.up * speed * Time.deltaTime);
    }

    //Destrói o projétil
    void DestroyProjectile(){
        //Instantiate particle
        Destroy(gameObject);
    }

}
