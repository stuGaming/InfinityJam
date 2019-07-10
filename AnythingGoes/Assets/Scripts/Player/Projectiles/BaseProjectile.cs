using UnityEngine;
using System.Collections;

public class BaseProjectile : MonoBehaviour
{
    [SerializeField]
    Explosion explosion;
    public Rigidbody2D thisRigid;
    public float strength;
    // Use this for initialization
    private void Start()
    {
        
    }
    public void SetSize(float x)
    {
        this.transform.localScale = this.transform.localScale * x;
    }
    // Update is called once per frame
    void Update()
    {
       
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(this.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            PlayerStats stats = collision.transform.GetComponent<PlayerStats>();
            if(stats!=null)
            {
                stats.Damage(strength);
            }
        }
        else
        {
            EnemyStats stats = collision.transform.GetComponent<EnemyStats>();
            if (stats != null)
            {
                stats.TakeDamage(strength);
            }
        }
        var e = Instantiate(explosion, LevelController.Instance.ProjectileParent);
        e.transform.position = this.transform.position;
        e.SetExplosion(Color.red,1+(strength/100f));
        Destroy(this.gameObject);
    }
}
