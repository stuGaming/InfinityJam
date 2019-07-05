using UnityEngine;
using System.Collections;

public class VelocityDamage : MonoBehaviour
{
    Rigidbody2D thisRigid;
    [SerializeField]
    float damageThreshold;
    [SerializeField]
    GameObject damageIndicator;
    // Use this for initialization
    void Start()
    {
        thisRigid = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void FixedUpdate()
    {
        damageIndicator.SetActive(Mathf.Abs(thisRigid.velocity.magnitude)>damageThreshold);
    }
}
