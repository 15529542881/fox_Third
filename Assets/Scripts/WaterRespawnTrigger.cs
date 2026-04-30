using UnityEngine;

public class WaterRespawnTrigger : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Animal"))
        {
            GameManager.Instance.RespawnPlayer();
        }
    }
}