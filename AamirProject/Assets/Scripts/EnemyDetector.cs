using UnityEngine;
using System.Collections;

public class EnemyDetector : MonoBehaviour {

    public GameObject playerParent;
    private Player player;

    private void Start()
    {
        // Getting "Player" script off of "Player Parent" Object
        player = playerParent.GetComponent<Player>();
    }

    private void OnTriggerEnter(Collider target)
    {
        if(target.tag == "Enemy")
        {
            player.enemy = target.GetComponent<Enemy>();
        }
        else if(target.tag == "Boss")
        {
            player.boss = target.GetComponent<Boss>();
        }
    }

    private void OnTriggerExit(Collider target)
    {
        if(target.tag == "Enemy")
        {
            player.enemy = null;
        }
        else if(target.tag == "Boss")
        {
            player.boss = null;
        }
    }
}