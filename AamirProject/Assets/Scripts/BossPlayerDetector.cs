using UnityEngine;
using System.Collections;

public class BossPlayerDetector : MonoBehaviour {

    private Boss boss;

    private void Start()
    {
        boss = GetComponentInParent<Boss>();
    }

    private void OnTriggerEnter(Collider target)
    {
        if(target.tag == "Player")
        {
            boss.playerInside = true;
        }
    }

    private void OnTriggerExit(Collider target)
    {
        if (target.tag == "Player")
        {
            boss.playerInside = false;
        }
    }

}