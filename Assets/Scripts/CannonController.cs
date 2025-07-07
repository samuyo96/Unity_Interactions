using UnityEngine;

public class CannonController : MonoBehaviour
{
    public Transform barrel;

    private SimpleControls controls;
    private Vector2 move;

    private void Awake()
    {
        controls = new SimpleControls();
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    private void Update()
    {
        move = controls.gameplay.move.ReadValue<Vector2>();
    }

    private void Aim(float input)
    {

    }
}
