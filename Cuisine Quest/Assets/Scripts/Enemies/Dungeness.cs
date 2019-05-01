using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dungeness : EnemyAbstract {
    
    public enum Behaviors
    {
        MoveRandom,
        MoveTowards,
        Attack,
        Defend,
        Die
    }
    public Behaviors CurrentBehavior;

    Vector2 currentArea;
    public Transform Target;
    private Vector2 movementTarget;
    public float MovementSpeed = 10f;
    public float BackingUpSpeed = 20f;
    private float currentSpeed = 10f;

    public int ClawAttackPower = 2;

    public float AttackDistance = 5f;
    public float AttackReadyTime = 2.5f;
    private float attackReadyStart;
    private bool attackStarted = false;
    public float AttackSpeed = 10f;

    public float PlayerTargetTime = 2.0f;
    private float playerTargetStart;
    private bool playerTargetted = false;

    float yBoundMax = 0;
    float yBoundMin = -4;
    float xBoundMax = 6;
    float xBoundMin = -6;

    // Use this for initialization
    void Start () {
        targetPlayer();
        movementTarget = transform.position;
        currentArea = GameObject.FindGameObjectWithTag("LevelHandler").GetComponent<LevelHandler>().CurrentArea.transform.position;
    }
	
	// Update is called once per frame
	void Update () {
        if (health.getCurrentHealth() <= 0) die();

        if(Target.position.y > transform.position.y && Mathf.Abs(Target.position.x -transform.position.x) < 2)
        {
            if (!playerTargetted)
            {
                playerTargetted = true;
                playerTargetStart = Time.fixedTime;
            }
            else if(playerTargetStart + PlayerTargetTime < Time.fixedTime)
            {
                CurrentBehavior = Behaviors.Attack;
                playerTargetted = false;
            }
        }
        else
        {
            playerTargetted = false;
        }
        if(CurrentBehavior == Behaviors.MoveRandom)
        {
            MoveRandom();
        }else if(CurrentBehavior == Behaviors.Attack)
        {
            Attack();
        }
	}

    private void die()
    {
        Drop();
        Destroy(gameObject);
    }

    public override void Attack()
    {
        if (!attackStarted)
        {
            GetComponent<Animator>().SetTrigger("Attack");
            attackStarted = true;
            attackReadyStart = Time.fixedTime;
            movementTarget = transform.position + new Vector3(0, AttackDistance, 0);
        }

        if(attackReadyStart + AttackReadyTime < Time.fixedTime)
        {
            transform.position = Vector3.MoveTowards(transform.position, movementTarget, AttackSpeed * Time.deltaTime);

            if(Vector3.Distance(transform.position, movementTarget) < 0.1)
            {
                attackStarted = false;
                CurrentBehavior = Behaviors.MoveRandom;
            }
        }
    }

    public override void Move()
    {
        throw new System.NotImplementedException();
    }

    private void targetPlayer()
    {
        Target = GameObject.FindGameObjectWithTag("Player").transform;
    }


    private void MoveRandom()
    {
        if (movementTarget == null) setRandomLocation();
        if(Vector3.Distance(movementTarget, transform.position) < 0.1)
        {
            setRandomLocation();
        }else if(Target.transform.position.y < transform.position.y)
        {
            float newY = Mathf.Clamp(Target.transform.position.y, yBoundMin + currentArea.y, yBoundMax + currentArea.y);
            movementTarget = new Vector2(movementTarget.x, newY);
            currentSpeed = BackingUpSpeed;
        }
        else
        {
            currentSpeed = MovementSpeed;
        }
        

        transform.position = Vector3.MoveTowards(transform.position ,movementTarget, currentSpeed * Time.deltaTime);
        
    }

    private void setRandomLocation()
    {
        
        movementTarget = new Vector2(Random.Range(xBoundMin, xBoundMax) + currentArea.x, Random.Range(yBoundMin, yBoundMax) + currentArea.y);
    }

    public void ClawHit(int damage)
    {
        Debug.Log("Claw Hit");
    }

    public void LegHit(int damage)
    {
        Debug.Log("Leg Hit");
    }


}
