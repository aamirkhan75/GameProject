using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Boss : MonoBehaviour {

    public float health = 100f;
    private float healthStart;
    private bool isDead = false;

    public Image healthSlider;

    public GameObject[] drops;

    private Animator anim;
    private Transform playerTransform;
    private Player player;

    public float rotationDamping = 5f;

    public float distanceToPlayer = 0f;
    public float distanceToAttack = 15f;

    public bool isHostile = false;

    [HideInInspector]
    public bool playerInside = false;

    public GameObject bossHealthCanvas;

    [Header("Attack Damage")]

    public float attackDamageMin = 25f;
    public float attackDamageMax = 50f;

    [Header("Attack Timer")]

    public float attackTimerMin = 1f;
    public float attackTimerMax = 5f;
    public float attackTimerCurrent;

    public LevelScript level;

    private void Start()
    {
        // Get Animator off self
        anim = GetComponent<Animator>();

        // Find Player Transform & Script
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        player = playerTransform.GetComponent<Player>();

        // Set healthStart to Health
        healthStart = health;

        // Reset "attackTimerCurrent" to Random Value
        attackTimerCurrent = Random.Range(attackTimerMin, attackTimerMax);
        level.AddEnemy();
    }

    private void Update()
    {
        // Set Health Slider Value
        if (health > 0)
        {
            healthSlider.fillAmount = health / healthStart;
        }
        else
        {
            healthSlider.fillAmount = 0;
        }

        if (isDead == false)
        {
            healthSlider.fillAmount = health / healthStart;

            // Set "distanceToPlayer"
            distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);

            if (distanceToPlayer <= distanceToAttack)
            {
                bossHealthCanvas.SetActive(true);
                isHostile = true;
            }
            else
            {
                bossHealthCanvas.SetActive(false);
                isHostile = false;
            }

            if (isHostile == true)
            {
                var lookPosition = playerTransform.position - transform.position;
                lookPosition.y = 0;
                var rot = Quaternion.LookRotation(lookPosition);
                transform.rotation = Quaternion.Slerp(transform.rotation, rot, Time.deltaTime * rotationDamping);

                attackTimerCurrent -= Time.deltaTime;

                if (attackTimerCurrent <= 0)
                {
                    attackTimerCurrent = Random.Range(attackTimerMin, attackTimerMax);
                    Attack();
                }
            }
        }
    }

    public void TakeDamage(float damage)
    {
        if (isDead == false)
        {
            // Subtract Health
            health -= damage;
            // Call "Hit" Animation
            anim.SetTrigger("getHit");

            if (health <= 0)
            {
                Death();
            }
        }
    }

    private void Attack()
    {
        // Play Animation
        anim.SetTrigger("punch");

        if(playerInside == true)
        {
            // Damage The Player

            float damage = Random.Range(attackDamageMin, attackDamageMax);
            player.TakeDamage(damage);
        }
    }

    private void Death()
    {
        level.RemoveEnemy();

        anim.SetTrigger("death");
        isDead = true;

        //GameObject itemToDrop = drops[Random.Range(0, drops.Length)];

        //Vector3 itemSpawnOffset = new Vector3(transform.position.x, transform.position.y + 3, transform.position.z);

        //Instantiate(itemToDrop, itemSpawnOffset, Quaternion.identity);
    }
}