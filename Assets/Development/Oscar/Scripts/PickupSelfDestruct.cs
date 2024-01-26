using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupSelfDestruct : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //wanneer je bullet spawnt start de destroyself timer
        StartCoroutine(DestroySelf());
    }
    //als je iets raakt verniel het gameobject
    IEnumerator DestroySelf()
    {
        //wacht voor 5 seconde voordat je iets doet
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }
}
