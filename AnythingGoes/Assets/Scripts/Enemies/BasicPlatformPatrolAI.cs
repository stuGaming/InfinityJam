using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicPlatformPatrolAI : MonoBehaviour
{
    [SerializeField]
    Explosion explosion;
    public Transform inFront;
    public Transform target;
    public float PatrolSpeed;
    public float forgetSpeed = 0.5f;
    public float lastSeen;
    public float AttackDistance;
    public float lookDistance = 0;
    public float attackRate = 0;
    private float lastAttack = 0;
    private bool atEdge = false;
    private bool _goingRight = false;
    public Animator anim;
    [SerializeField]
    float attackStrength;
    private bool GoingRight
    {
        get
        {
            return _goingRight;
        }
        set
        {
            if (Time.time - lastAttack < attackRate)
            {
                return;
            }
            if (value)
            {

                Vector3 theScale = transform.localScale;
                theScale.x = Mathf.Abs(theScale.x);
                transform.localScale = theScale;
            }
            else
            {

                Vector3 theScale = transform.localScale;
                theScale.x = -1 * Mathf.Abs(theScale.x);
                transform.localScale = theScale;
            }

            _goingRight = value;
        }
    }

    private Rigidbody2D thisRigid;
    private Vector3 m_Velocity;
    private float m_MovementSmoothing;
    LayerMask playerMask;
    // Start is called before the first frame update
    void Start()
    {
        lastAttack = -1 * attackRate;
        playerMask = LayerMask.NameToLayer("Player");

        thisRigid = this.GetComponent<Rigidbody2D>();
        GoingRight = (1 == UnityEngine.Random.Range(0, 1));
    }
    public void Move(float move)
    {
        if (atEdge)
        {
            if(anim!=null)
                anim.SetBool("Run", false);
            return;
        }
           


        // Move the character by finding the target velocity
        Vector3 targetVelocity = new Vector2(move * 10f, thisRigid.velocity.y);
        // And then smoothing it out and applying it to the character
        thisRigid.velocity = Vector3.SmoothDamp(thisRigid.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);
        if (anim != null)
            anim.SetBool("Run", true);




    }
    private void FixedUpdate()
    {
        if (!LevelController.Instance.GameIsPlaying)
            return;
        if (Time.time - lastAttack < attackRate)
        {
            thisRigid.velocity = Vector3.zero;
            if (anim != null)
                anim.SetBool("Run", false);
        }
        else if (target != null && (target.position - this.transform.position).sqrMagnitude < AttackDistance)
        {
            thisRigid.velocity = Vector3.zero;
            if (anim != null)
                anim.SetBool("Run", false);
            if (Time.time - lastAttack > attackRate&& target != null && (target.position - this.transform.position).sqrMagnitude < AttackDistance)
            {
                StartCoroutine(WaitAndExplode());
                attackRate = 100f;
               
               
               
                lastAttack = Time.time;
            }
           
        }
        else
        {
            if (GoingRight)
                Move(PatrolSpeed * Time.fixedDeltaTime);
            else
                Move(PatrolSpeed * -1 * Time.fixedDeltaTime);

        }

    }

    private IEnumerator WaitAndExplode()
    {
        float timer = 0;
        while (timer < 1)
        {
            transform.localScale = transform.localScale * (1 + (Time.deltaTime/5));
            timer += Time.deltaTime;
            yield return null;
        }
       
            
    
       
        
        if (target != null && (target.position - this.transform.position).sqrMagnitude < AttackDistance*2)
        {
            if (target.GetComponent<PlayerStats>() != null)
                target.GetComponent<PlayerStats>().Damage(attackStrength);
        }
        Explosion e = Instantiate(explosion);
        e.transform.position = this.transform.position;
        e.SetExplosion(Color.red, 4);
        Destroy(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (!LevelController.Instance.GameIsPlaying)
            return;
        RaycastHit2D hit;
        RaycastHit2D[] allHits = Physics2D.RaycastAll(transform.position, transform.right, lookDistance);
        Debug.DrawRay(transform.position, -1 * transform.right * lookDistance);
        // If it hits something...
        if (Time.time - lastSeen > forgetSpeed)
        {
           
            target = null;
        }
        
        for (int i = 0; i < allHits.Length; i++)
        {
            
            if (allHits[i].transform.tag == "Player")
            {

                PlayerStats character = allHits[i].transform.GetComponent<PlayerStats>();
                if (character != null)
                {
                    GoingRight = true;
                    Debug.Log("Found target");
                    target = allHits[i].transform;
                    lastSeen = Time.time;
                }
            }
        }

        allHits = Physics2D.RaycastAll(transform.position, -1 * transform.right, lookDistance);

        // If it hits something...
        for (int i = 0; i < allHits.Length; i++)
        {
            
            if (allHits[i].transform.tag == "Player")
            {
                PlayerStats character = allHits[i].transform.GetComponent<PlayerStats>();
                if (character != null)
                {
                    GoingRight = false;
                    Debug.Log("Found target");
                    target = allHits[i].transform;
                    lastSeen = Time.time;
                }
            }
        }

       

        hit = Physics2D.Raycast(inFront.position, -1 * inFront.up, 1);

        // If it hits something...
        if (hit.collider == null || hit.distance > 0.5f)
        {
            if (target == null)
            {
                GoingRight = !GoingRight;
            }
            else
            {
                thisRigid.velocity = Vector3.zero;
                atEdge = true;
            }



        }
        else
        {
            atEdge = false;
        }



    }
}
