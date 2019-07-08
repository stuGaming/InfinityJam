using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FlyingEnemy : MonoBehaviour
{
    [SerializeField]
    float patrolSpeed;
    [SerializeField]
    float AttackSpeed;
    [SerializeField]
    float AttackRange;
    [SerializeField]
    float AttackDamage;

    [SerializeField]
    List<Transform> patrolPoints;
    int patrolTarget = 0;

    Transform target;
    Rigidbody2D thisRigid;
    // Use this for initialization
    void Start()
    {
        thisRigid = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
            return;
        if(Mathf.Abs((this.transform.position - LevelController.Instance.player.transform.position).magnitude)<AttackRange)
        {
            RaycastHit2D hit = Physics2D.Raycast(this.transform.position, LevelController.Instance.player.transform.position - this.transform.position);
            PlayerStats player = hit.transform.GetComponent<PlayerStats>();
            if (player != null)
            {
                target = player.transform;
            }
        }
        
    }
    private void FixedUpdate()
    {
        if (target != null)
        {
            thisRigid.MovePosition(this.transform.position + ((target.position - this.transform.position).normalized*AttackSpeed*Time.fixedDeltaTime));
        }
        else
        {
            thisRigid.MovePosition(this.transform.position + ((patrolPoints[patrolTarget].position - this.transform.position).normalized * AttackSpeed * Time.fixedDeltaTime));
            if(Mathf.Abs((this.transform.position- patrolPoints[patrolTarget].position).magnitude) < 0.1f)
            {
                patrolTarget++;
                if (patrolTarget >= patrolPoints.Count)
                {
                    patrolTarget = 0;
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerStats player = collision.transform.GetComponent<PlayerStats>();
        if (player != null)
        {
            player.Damage(AttackDamage);
        }
        Destroy(this.gameObject);
    }
}
