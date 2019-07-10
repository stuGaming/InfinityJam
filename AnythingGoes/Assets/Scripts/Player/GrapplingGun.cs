using UnityEngine;
using System.Collections;
using System;

public class GrapplingGun : MonoBehaviour
{
    [SerializeField]
    LineRenderer Chain;
    [SerializeField]
    GameObject hook;
    [SerializeField]
    PlayerInput playerInput;
    [SerializeField]
    Transform muzzle;
    [SerializeField]
    float pullForce;
    [SerializeField]
    float maxDistance;
    [SerializeField]
    float breakDistance;
    private Collider2D latchedObject;
    private bool _chainExtended = false;
    private Rigidbody2D thisRigid;
    
    internal bool grappling
    {
        get
        {
            return _chainExtended;
        }
    }

    private bool ChainExtended
    {
        get
        {
            return _chainExtended;
        }
        set
        {
            _chainExtended = value;
            Chain.gameObject.SetActive(_chainExtended);
           
           
             hook.SetActive(_chainExtended);
        }
    }
    // Use this for initialization
    void Start()
    {
        thisRigid = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        bool fireChain =playerInput.SecondaryFire;
        bool releaseChain = playerInput.SecondaryReleased;
        if (fireChain)
        {
            SetupChain();
        }
        if (ChainExtended)
        {

            CheckBreak();
            if (ChainExtended)
            {
                

                UpdateChainPositions();
            }
               
            
        }
        if (releaseChain)
        {
            ChainExtended = false;
        }
    }

    private void PullTowards()
    {
        if (!LevelController.Instance.GameIsPlaying)
            return;
       
        thisRigid.AddForce((pullForce * Time.deltaTime * (hook.transform.position - this.transform.position).normalized));
        //thisRigid.velocity = Vector3.zero;
       // thisRigid.MovePosition(this.transform.position + (pullForce * Time.deltaTime * (hook.transform.position - this.transform.position)));
    }
    private void FixedUpdate()
    {
        if (ChainExtended)
        {
            PullTowards();
        }
    }
    private void CheckBreak()
    {
        LayerMask mask = LayerMask.NameToLayer("Terrain");
        RaycastHit2D hit = Physics2D.Raycast(this.transform.position,    (hook.transform.position - this.transform.position), float.PositiveInfinity,1<<LayerMask.NameToLayer("Terrain"));
        Debug.DrawLine(this.transform.position, hook.transform.position, Color.red);
        if (hit.collider != latchedObject)
        {
            
            ChainExtended = false;
           
        }else if(Mathf.Abs((hit.point - (Vector2)hook.transform.position).magnitude)>0.5f)
        {
            ChainExtended = false;
            
        }
        else if (Mathf.Abs((hook.transform.position - this.transform.position).magnitude) < breakDistance)
        {
            ChainExtended = false;
        }
        if (hit.collider == null)
        {
            Debug.Log("Missed");
        }
    }

    private void UpdateChainPositions()
    {
        Chain.SetPosition(1, this.transform.position);
        Chain.SetPosition(0, hook.transform.position);
        hook.transform.right = hook.transform.position - this.transform.position;
    }

    private void SetupChain()
    {
        int mask = ~(LayerMask.NameToLayer("Terrain"));
       
        RaycastHit2D hit = Physics2D.Raycast(muzzle.position, muzzle.right,float.PositiveInfinity, 1 << LayerMask.NameToLayer("Terrain"));
        if (hit.collider!=null)
        {
            if((hit.point - (Vector2)this.transform.position).sqrMagnitude > maxDistance)
            {

                Debug.Log("Missed all "+ (hit.point - (Vector2)this.transform.position).sqrMagnitude);
                ChainExtended = false;
                return;
            }
            latchedObject = hit.collider;
            ChainExtended = true;
            Chain.positionCount = 2;
            hook.transform.position = hit.point;
            Chain.SetPosition(1, this.transform.position);
            Chain.SetPosition(0, hook.transform.position);
            Debug.Log("Latched" + hit.transform.name + hit.transform.gameObject.layer);
            //thisRigid.velocity = Vector3.zero;
        }
        else
        {
            Debug.Log("Missed all");
            ChainExtended = false;
        }
       
    }
}
