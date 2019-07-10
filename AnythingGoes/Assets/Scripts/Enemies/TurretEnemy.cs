using UnityEngine;
using System.Collections;

public class TurretEnemy : MonoBehaviour
{
    [SerializeField]
    EnemyStats stats;
    [SerializeField]
    Transform Hinge;
    [SerializeField]
    Transform Muzzle;
    [SerializeField]
    float AttackRange;
    [SerializeField]
    float FireRate;
    [SerializeField]
    BaseProjectile projectile;
    float lastAttackTime;
    Transform target;
    // Use this for initialization
    void Start()
    {

    }

  
    private void Update()
    {
        if (!LevelController.Instance.GameIsPlaying)
        {
            return;
        }
        if (Mathf.Abs((this.transform.position - LevelController.Instance.player.transform.position).magnitude) < AttackRange&& Mathf.Abs((this.transform.position - LevelController.Instance.player.transform.position).magnitude) > 5)
        {
            
                target = LevelController.Instance.player.transform;
           
        }
        else
        {
            
            target = null;
        }
        if (target != null)
        {
            //Hinge.LookAt(target.position);
            Hinge.right = Hinge.position - target.position;
            if(Time.time - lastAttackTime > FireRate)
            {
                lastAttackTime = Time.time;
                
                BaseProjectile p = Instantiate(projectile, LevelController.Instance.ProjectileParent);
                p.strength = stats.Damage;
                p.transform.position = Muzzle.position;
                p.transform.right = Muzzle.transform.right;
                p.thisRigid.AddForce(Muzzle.right * 1000f);
             




            }
        }
    }
}
