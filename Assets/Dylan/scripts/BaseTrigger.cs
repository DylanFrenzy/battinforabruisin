using UnityEngine;

public class BaseTrigger : MonoBehaviour
{
    public int myBase;
    

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Player Enter Base");
        GameManager.Instance.PlayerOnBase(myBase, other.GetComponent<Player>().myTeam);
    }

}
