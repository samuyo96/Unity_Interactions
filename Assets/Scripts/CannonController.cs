using System.Collections;
using UnityEngine;

public class CannonController : MonoBehaviour
{
    [Header("Transforms")]
    [SerializeField] private Transform barrel;
    [SerializeField] private Transform[] wheels;
    [SerializeField] private Transform spawnPoint;

    [Header("Aim Settings")]
    public float speed = 10f;
    public float maxRotation = 20f;
    public float minRotation = -10f;

    [Header("Body Rotation Settings")]
    public float bodyRotationSpeed = 15f;
    public float maxBodyRotation = 60;
    public float minBodyRotation = -60f;

    [Header("Projectile")]
    public GameObject projectilePrefab;
    public float projectileForce = 30f;
    public float cadence = 1f;

    private SimpleControls controls;
    private Vector2 move;
    private Vector2 aimRotation = Vector2.zero;
    private Vector2 bodyRotation = new Vector2(0, 180f);
    private Vector2 wheelRotation = Vector2.zero;

    private void Awake()
    {
        controls = new SimpleControls();
        bodyRotation.y = transform.eulerAngles.y;
        maxBodyRotation += bodyRotation.y;
        minBodyRotation += bodyRotation.y;
    }

    private void OnEnable()
    {
        controls.Enable();
        controls.gameplay.fire.performed += ctx => Fire();
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    private void Update()
    {
        move = controls.gameplay.move.ReadValue<Vector2>();
        Debug.Log($"Move Input: {move}");
        Aim(move.y);
        RotateBody(move.x);
    }

    private void Aim(float input)
    {
        aimRotation.x = Mathf.Clamp(aimRotation.x + input * Time.deltaTime * speed, minRotation, maxRotation);
        barrel.localEulerAngles = aimRotation;
    }

    private void RotateBody(float input)
    {
        bodyRotation.y = Mathf.Clamp(bodyRotation.y + input * Time.deltaTime * bodyRotationSpeed, minBodyRotation, maxBodyRotation);
        transform.eulerAngles = bodyRotation;
        RotateWheels();
    }

    private void RotateWheels()
    {
        wheelRotation.x = bodyRotation.y;
        wheels[0].localEulerAngles = wheels[1].localEulerAngles = -wheelRotation;
        wheels[2].localEulerAngles = wheels[3].localEulerAngles = wheelRotation;
    }

    private void Fire()
    {
        GameObject projectile = Instantiate(projectilePrefab, spawnPoint.position, spawnPoint.rotation);
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        rb.AddForce(spawnPoint.forward * projectileForce, ForceMode.Impulse);
        StartCoroutine(FireCooldown());
    }

    private IEnumerator FireCooldown()
    {
        controls.gameplay.fire.Disable();
        yield return new WaitForSeconds(cadence);
        controls.gameplay.fire.Enable();
    }
}
