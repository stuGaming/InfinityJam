using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AdvancedAspectRatioFitter : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        AspectRatioFitter fitter = this.GetComponent<AspectRatioFitter>();
        if(fitter ==null)
        {
            Debug.LogError("Couldnt find aspect ratio fitter, please ensure it is attached to the object");
            return;
        }
        RectTransform rect = this.GetComponent<RectTransform>();
        if (rect == null)
        {
            Debug.LogError("Must be atatched to a rect transform");
            return;
        }
        fitter.aspectRatio = (float)Screen.width / (float)Screen.height;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
