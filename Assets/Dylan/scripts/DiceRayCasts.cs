using UnityEngine;

public class DiceRayCasts : MonoBehaviour
{
    public int myNumber;

    public LayerMask layerMask;

    public bool casting;

    public DiceRoller diceRoller;

    private void Start()
    {
        diceRoller = GetComponentInParent<DiceRoller>();
    }

    void FixedUpdate()
    {
        if (casting)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 1f, layerMask))

            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
                diceRoller.diceNumber = myNumber;
            }
            else
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1f, Color.white);
                
            }
        }
    }
}
