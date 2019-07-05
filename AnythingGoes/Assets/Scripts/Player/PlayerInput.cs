using UnityEngine;
using System.Collections;

public class PlayerInput : MonoBehaviour
{
    public Vector2 InputPosition { get; internal set; }
    public bool Firing { get; internal set; }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        InputPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Firing = Input.GetButton("Fire");
    }
}
