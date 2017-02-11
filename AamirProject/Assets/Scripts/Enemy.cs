using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

    public float health = 100f;
    public bool isDead = false;

    private UnityEngine.AI.NavMeshAgent agent;

    private Animator anim;

    public float fistDamage = 10f;

    public float walkRangeMin = 5f;
    public float walkRangeMax = 10f;

    public float thinkTimerMin = 5f;
    public float thinkTimerMax = 10f;
    public float thinkTimer = 5f;

    public float attackTimerMin = 0.5f;
    public float attackTimerMax = 2f;
    public float attackTimer = 1f;

    public float agressionDistance = 5f;
    public float escapeDistance = 15f;
    public bool isChasing = false;
    public bool canPunch = false;

    private Transform playerTransform;
    private float distanceToPlayer;

    public Player player;
    public LevelScript level;

    private void Start()
    {
        // Get Animator off self
        anim = GetComponent<Animator>();

        // Get Navmesh Agent
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();

        // Find Player Transform
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        level.AddEnemy();

        Logic();

    }

    private void Update()
    {
        thinkTimer -= Time.deltaTime;
        distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);

        Logic();
       

        if(agent.remainingDistance - agent.stoppingDistance <= 0)
        {
            anim.SetBool("isWalking", false);
        }
        else
        {
            anim.SetBool("isWalking", true);
        }

        if(distanceToPlayer <= agressionDistance)
        {
            isChasing = true;
        }

        if(distanceToPlayer >= escapeDistance)
        {
            isChasing = false;
        }

        if(player != null)
        {
            canPunch = true;
        }
        else
        {
            canPunch = false;
        }
    }

    public void TakeDamage(float damage)
    {
        if(isDead == false)
        {
            // Subtract Health
            health -= damage;
            
            // Call "Hit" Animation
            anim.SetTrigger("getHit");

            if (health <= 0)
            {
                level.RemoveEnemy();
                anim.SetTrigger("death");
                isDead = true;
            }
        }
    }

    private void LogicOld()
    {
        if (isDead == false)
        {
            if (isChasing == true)
            {
                agent.SetDestination(playerTransform.position);

                if (canPunch == true)
                {
                    attackTimer -= Time.deltaTime;

                    if (attackTimer <= 0)
                    {
                        attackTimer = Random.Range(attackTimerMin, attackTimerMax);
                        anim.SetTrigger("punch");
                        player.TakeDamage(fistDamage);
                    }
                }
            }
            else if (isChasing == false)
            {
                
                if (thinkTimer <= 0)
                {
                    thinkTimer = Random.Range(thinkTimerMin, thinkTimerMax);

                    Vector2 currentTransformPosition = new Vector2(transform.position.x, transform.position.z);
                    Vector2 newPos = currentTransformPosition + Random.insideUnitCircle * Random.Range(walkRangeMin, walkRangeMax);
                    Vector3 setNewPos = new Vector3(newPos.x, transform.position.y, newPos.y);
                    //transform.position = setNewPos;
                    agent.SetDestination(setNewPos);
                }
            }
        }
    }

    private void Logic()
    {
        if(isDead == false)
        {
            if(isChasing == true)
            {
                agent.SetDestination(playerTransform.position);

                if(canPunch == true)
                {
                    attackTimer -= Time.deltaTime;

                    if(attackTimer <= 0)
                    {
                        attackTimer = Random.Range(attackTimerMin, attackTimerMax);
                        anim.SetTrigger("punch");
                        player.TakeDamage(fistDamage);
                    }
                }
            }
            else if(isChasing == false)
            {
                // Stand still if not chasing


                /*
                if (thinkTimer <= 0)
                {
                    thinkTimer = Random.Range(thinkTimerMin, thinkTimerMax);

                    Vector2 currentTransformPosition = new Vector2(transform.position.x, transform.position.z);
                    Vector2 newPos = currentTransformPosition + Random.insideUnitCircle * Random.Range(walkRangeMin, walkRangeMax);
                    Vector3 setNewPos = new Vector3(newPos.x, transform.position.y, newPos.y);
                    //transform.position = setNewPos;
                    agent.SetDestination(setNewPos);
                }
                */
            }
        }
    }
}