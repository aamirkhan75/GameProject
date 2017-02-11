using UnityEngine;
using System.Collections;

public class PlayerDetector : MonoBehaviour {

    public GameObject enemyParent;
    private Enemy enemy;

    private void Start()
    {
        // Get Enemy script off Enemy Parent
        enemy = enemyParent.GetComponent<Enemy>();
    }

    private void OnTriggerEnter(Collider target)
    {
        if (target.tag == "Player")
        {
            enemy.player = target.GetComponent<Player>();
        }
    }

    private void OnTriggerExit(Collider target)
    {
        if (target.tag == "Player")
        {
            enemy.player = null;
        }
    }
}