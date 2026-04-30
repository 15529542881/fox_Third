using UnityEngine;

public class PickupChuiZiTrigger : MonoBehaviour
{
    private bool playerInRange = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Animal"))
        {
            playerInRange = true;
            GameManager.Instance.ShowHint("Press E to take Hammer");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Animal"))
        {
            playerInRange = false;
        }
    }
}