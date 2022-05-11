using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropHandler : MonoBehaviour
{
    //Esse script lida com objetos que deixam items no mapa ao serem destruídos
    public GameObject[] itemsToDrop;
    
    public void DropItem(){
        for (int i=0; i < itemsToDrop.Length; i++){
            Debug.Log("DROP!");
            Instantiate(itemsToDrop[i], transform.position, transform.rotation);
        }
    }
}
