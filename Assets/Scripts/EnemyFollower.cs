using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollower : MonoBehaviour
{
    
    //Script utilizado nos espectros, faz cm que o inimigo siga o personagem
    //Seria interessante fazer outro algorítmo pra esse, algo envolvendo pathfinding?

    public float minspeed;
    public float maxspeed;
    public float lineOfSight;
    public float rangeOfAttack;
    Transform player;
    Rigidbody2D mybody;
    RotateToTarget rotate;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        mybody = GetComponent<Rigidbody2D>();
        rotate = GetComponent<RotateToTarget>();
    }

    void Update()
    {
        if (rotate != null){
            rotate.Rotate(25,player);
        }
        
        float distanceToPlayer = Vector2.Distance(player.position, transform.position);
        if (distanceToPlayer < lineOfSight && distanceToPlayer < rangeOfAttack){
            transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, 
                                                minspeed*Time.deltaTime);
            mybody.gravityScale = 1;
        }
        else if (distanceToPlayer < lineOfSight && !(distanceToPlayer < rangeOfAttack)){
            transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, 
                                                maxspeed*Time.deltaTime);
            mybody.gravityScale = 0;
        }
    }

    void OnDrawGizmosSelected(){
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position,lineOfSight);
        Gizmos.DrawWireSphere(transform.position,rangeOfAttack);
    }
}
