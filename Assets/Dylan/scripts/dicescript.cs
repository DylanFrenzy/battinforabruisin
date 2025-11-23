using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.InputSystem;
using System.Collections;
public class dicescript : MonoBehaviour, IInteractable
{
    private readonly float power = 10f;
    
    private Rigidbody rb;
    private Camera mainCamera;
    
    private Vector3 rotationVelocity = new(100, 100, 100);

    private bool isHeld = false;
    private bool saveNext = true;
    private Vector3 lastPosition;
    private Vector3 dir;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        mainCamera = Camera.main;
    }
    public void Interact()
    {
        isHeld = true;
        rb.isKinematic = true;
        rb.useGravity = false;
    }

    private void Update()
    {
        if (!isHeld) return;

        Vector2 mousePosition = Mouse.current.position.ReadValue();
        Vector3 target = new(mousePosition.x + 400, 60, mousePosition.y + 846);

        dir = transform.position - lastPosition;
        transform.position = Vector3.Lerp(transform.position, target, 0.1f);
        rotationVelocity += new Vector3(100, 100, 100) * Time.deltaTime;
        transform.Rotate(rotationVelocity * Time.deltaTime);

        if (saveNext)
            StartCoroutine(SavePosition());
    }

    public void Disengage()
    {
        if (!isHeld) return; 

        isHeld = false;
        rb.isKinematic = false;
        rb.useGravity = true;

        rb.linearVelocity = dir * power;
        rb.angularVelocity = Random.insideUnitSphere * 10;

        rotationVelocity = new(100, 100, 100);
    }

    private IEnumerator SavePosition()
    {
        saveNext = false;
        lastPosition = transform.position;
        yield return new WaitForSeconds(0.2f);
        saveNext = true;
    }
}
