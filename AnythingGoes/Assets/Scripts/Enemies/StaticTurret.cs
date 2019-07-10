using UnityEngine;
using System.Collections;

public class StaticTurret : MonoBehaviour
{
    
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
   


    private void Update()
    {
        if (!LevelController.Instance.GameIsPlaying)
        {
            return;
        }
        
            if (Time.time - lastAttackTime > FireRate)
            {
                lastAttackTime = Time.time;

                BaseProjectile p = Instantiate(projectile, LevelController.Instance.ProjectileParent);
            p.strength = 100f;
                p.transform.position = Muzzle.position;
                p.transform.right = Muzzle.transform.right;
                p.thisRigid.AddForce(Muzzle.right * 1000f);





            }
       
    }
}
