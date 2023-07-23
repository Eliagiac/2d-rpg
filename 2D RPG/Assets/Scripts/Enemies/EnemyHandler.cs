using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyHandler : MonoBehaviour
{
    public GameObject rock;

    GameObject instantiatedRock;

    Transform player;

    AIDestinationSetter AIDestinationSetter;

    int n;

    public int delay = 60;

    public float range = 10f;

    public int closeToPlayerDistance = 6;

    RockHandler rockHandler;

    IAstarAI IAstarAI;

    Vector3 enemyDirection;

    Animator animator;

    SpriteRenderer[] spriteRenderers;

    GameObject downCharacter;
    GameObject upCharacter;
    GameObject leftCharacter;
    GameObject rightCharacter;

    Vector2 movement;
    float tempMovementX;
    float tempMovementY;

    string directionX = "";
    string directionY = "";

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        AIDestinationSetter = gameObject.GetComponent<Pathfinding.AIDestinationSetter>();

        IAstarAI = gameObject.GetComponent<Pathfinding.IAstarAI>();

        IAstarAI.canSearch = false;

        animator = gameObject.GetComponent<Animator>();

        spriteRenderers = gameObject.GetComponentsInChildren<SpriteRenderer>();

        downCharacter = transform.Find("Enemy Character (facing down)").gameObject;
        upCharacter = transform.Find("Enemy Character (facing up)").gameObject;
        leftCharacter = transform.Find("Enemy Character (facing left)").gameObject;
        rightCharacter = transform.Find("Enemy Character (facing right)").gameObject;
    }

    private void Update()
    {
        //Debug.Log(1 / Time.deltaTime);

        if (!AIDestinationSetter.isInTribe || !AIDestinationSetter.followPlayer || (new Vector3(player.position.x, player.position.y + 1.065028f, player.position.z) - transform.position).sqrMagnitude <= closeToPlayerDistance * closeToPlayerDistance)
        {
            IAstarAI.isStopped = true;

            gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;

            //IAstarAI.hasPath = false;

            //IAstarAI.maxSpeed = 0;

            animator.SetFloat("Speed", 0f);

            animator.SetFloat("Horizontal", 0f);
            animator.SetFloat("Vertical", 0f);
            animator.SetFloat("Idle Horizontal", 0f);
            animator.SetFloat("Idle Vertical", 0f);
        }

        else 
        {
            IAstarAI.isStopped = false;

            gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;

            //IAstarAI.hasPath = true;

            //IAstarAI.maxSpeed = 3;

            animator.SetFloat("Speed", 1f);

            Animate();
        }

        if (!AIDestinationSetter.followPlayer)
        {
            IAstarAI.canSearch = false;
        }

        else IAstarAI.canSearch = true;

        RenderEnemy();
    }

    private void FixedUpdate()
    {
        if (AIDestinationSetter.followPlayer && AIDestinationSetter.isInTribe && (transform.position - player.position).sqrMagnitude <= range * range && n == delay)
        {
            Attack(player);

            n = 0;
        }

        if (AIDestinationSetter.followPlayer && AIDestinationSetter.isInTribe && (transform.position - player.position).sqrMagnitude <= range * range)
        {
            n += 1;
        }

        if (!AIDestinationSetter.followPlayer && !AIDestinationSetter.isInTribe && (transform.position - player.position).sqrMagnitude > range * range)
        {
            n = 0;
        }

        enemyDirection = transform.position - IAstarAI.steeringTarget;

        movement = -enemyDirection;

        if (enemyDirection.x > 0 || enemyDirection.y > 0)
        {
            //Debug.Log(enemyDirection.x + " " + enemyDirection.y);
        }
    }

    public void Attack(Transform target)
    {
        // Attack animation

        instantiatedRock = Instantiate(rock, new Vector3(transform.position.x, transform.position.y, -6.1f), Quaternion.identity);

        rockHandler = instantiatedRock.GetComponent<RockHandler>();
        rockHandler.isLaunching = true;
        rockHandler.target = target;
    }

    void RenderEnemy()
    {
        for (int i = 0; i < spriteRenderers.Length; i++)
        {
            if (spriteRenderers[i].isVisible)
            {
                animator.enabled = true;

                return;
            }
        }

        animator.enabled = false;
    }
    
    void SetCharacter(string dir)
    {
        switch (dir)
        {
            case "down":

                downCharacter.SetActive(true);
                upCharacter.SetActive(false);
                leftCharacter.SetActive(false);
                rightCharacter.SetActive(false);

                animator.SetFloat("Idle Vertical", -1f);

                break;

            case "up":

                downCharacter.SetActive(false);
                upCharacter.SetActive(true);
                leftCharacter.SetActive(false);
                rightCharacter.SetActive(false);

                animator.SetFloat("Idle Vertical", 1f);

                break;

            case "left":

                downCharacter.SetActive(false);
                upCharacter.SetActive(false);
                leftCharacter.SetActive(true);
                rightCharacter.SetActive(false);

                animator.SetFloat("Idle Horizontal", -1f);

                break;

            case "right":

                downCharacter.SetActive(false);
                upCharacter.SetActive(false);
                leftCharacter.SetActive(false);
                rightCharacter.SetActive(true);

                animator.SetFloat("Idle Horizontal", 1f);

                break;
        }
    }

    void Animate()
    {
        if (movement.x > 0)
        {
            tempMovementX = -movement.x;
        }

        else tempMovementX = movement.x;

        if (movement.y < 0 && movement.y < tempMovementX)
        {
            directionY = "down";

            directionX = "";

            animator.SetFloat("Horizontal", 0f);
            animator.SetFloat("Vertical", -1f);
        }

        else if (movement.y > 0 && movement.y > -tempMovementX)
        {
            directionY = "up";

            directionX = "";

            animator.SetFloat("Horizontal", 0f);
            animator.SetFloat("Vertical", 1f);
        }

        if (movement.y > 0)
        {
            tempMovementY = -movement.y;
        }

        else tempMovementY = movement.y;

        if (movement.x < tempMovementY)
        {
            directionX = "left";

            directionY = "";

            animator.SetFloat("Horizontal", -1f);
            animator.SetFloat("Vertical", 0f);
        }

        else if (movement.x > -tempMovementY)
        {
            directionX = "right";

            directionY = "";

            animator.SetFloat("Horizontal", 1f);
            animator.SetFloat("Vertical", 0f);
        }

        if (directionX != "")
        {
            SetCharacter(directionX);
        }

        else if (directionY != "")
        {
            SetCharacter(directionY);
        }
    }
}