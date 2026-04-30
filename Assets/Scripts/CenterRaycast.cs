using UnityEngine;


public class CenterRaycast : MonoBehaviour
{
    public Color rayColor = Color.red;
    public float rayDistance = 100f;

    void Update()
    {
        DrawCenterRay();

        if (Input.GetMouseButtonDown(0))
        {
            CheckRaycastHit();
        }
    }

    void DrawCenterRay()
    {
        Vector2 screenCenter = new Vector2(Screen.width / 2, Screen.height / 2);

        Ray ray = Camera.main.ScreenPointToRay(screenCenter);

        Debug.DrawRay(ray.origin, ray.direction * rayDistance, rayColor);
    }

    void CheckRaycastHit()
    {
        Ray ray = Camera.main.ScreenPointToRay(new Vector2(Screen.width / 2, Screen.height / 2));
        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo, rayDistance))
        {
            string hitObjectName = hitInfo.collider.gameObject.name;
            if (hitObjectName == "balloon")
            {
                Destroy(hitInfo.collider.gameObject);
                GameManager.Instance.KillBalloon();
            }
        }
    }
}