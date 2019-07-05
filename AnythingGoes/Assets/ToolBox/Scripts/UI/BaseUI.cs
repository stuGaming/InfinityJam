using UnityEngine;
using System.Collections;

public class BaseUI : MonoBehaviour
{
    private Animator anim;
    // Use this for initialization
    void Awake()
    {
        anim = this.GetComponent<Animator>();
    }

    public void Show()
    {
        if (anim != null)
        {
            anim.SetBool("Show", true);
        }
        else
        {
            this.gameObject.SetActive(true);
        }
    }
    public void Hide()
    {
        if (anim != null)
        {
            anim.SetBool("Show", false);
        }
        else
        {
            this.gameObject.SetActive(false);
        }
    }
    void OnDestroy()
    {
        if(Mediator.Instance!=null)
            Mediator.UnRegisterAllHandlers(this);
    }
}
