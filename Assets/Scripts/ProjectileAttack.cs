using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileAttack : MonoBehaviour
{
    //Script responsável por realizar a instância de um projétil

    //Ponto de origem do projétil
    public Transform attackPoint;
    
    //O projétil
    public GameObject projectile;
    ProjectileScript projectileHandler;

    void Start()
    {
        projectileHandler = gameObject.GetComponent<ProjectileScript>();
    }
    
    public void Shoot(){
        Instantiate(projectile, attackPoint.position, gameObject.transform.rotation);
    }

}
