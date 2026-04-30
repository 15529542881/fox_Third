using UnityEngine;

public class DeliverChuiZiTrigger : MonoBehaviour
{
    private bool playerInRange = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Animal"))
        {
            playerInRange = true;
            GameManager.Instance.ShowHint("Press E to give Hammer");
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