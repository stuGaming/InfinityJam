using UnityEngine;
using System.Collections;

public class PlayerInput : MonoBehaviour
{
    public Vector2 InputPosition { get; internal set; }
    public bool Firing { get; internal set; }
    public bool SecondaryFire { get; internal set; }
    public bool SecondaryReleased { get; internal set; }
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!LevelController.Instance.GameIsPlaying)
            return;
        InputPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Firing = Input.GetButton("Fire");
        SecondaryFire = Input.GetButtonDown("SecondaryFire");
        SecondaryReleased = Input.GetButtonUp("SecondaryFire");
    }
}
