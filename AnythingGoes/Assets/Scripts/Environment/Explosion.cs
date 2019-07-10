using UnityEngine;
using System.Collections;
using UnityEngine.Experimental.Rendering.LWRP;

public class Explosion : MonoBehaviour
{
    [SerializeField]
    Light2D light;
    public void SetExplosion(Color col, float Size)
    {
        this.transform.localScale = this.transform.localScale * Size;
        light.color = col;
    }
    // Use this for initialization
    void Start()
    {
        Destroy(this.gameObject, 1f);
    }

    
}
