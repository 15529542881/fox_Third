using UnityEngine;

/// <summary>
/// 从屏幕中心发射射线，鼠标点击时检测并打印击中物体名称
/// </summary>
public class CenterRaycast : MonoBehaviour
{
    // 射线可视化的颜色（仅在Scene视图可见）
    public Color rayColor = Color.red;
    // 射线的最大检测距离
    public float rayDistance = 100f;

    void Update()
    {
        // 每一帧都绘制射线（仅在Scene视图可见，方便调试）
        DrawCenterRay();

        // 检测鼠标左键点击
        if (Input.GetMouseButtonDown(0))
        {
            // 执行射线检测并打印结果
            CheckRaycastHit();
        }
    }

    /// <summary>
    /// 绘制从屏幕中心发出的射线（调试用）
    /// </summary>
    void DrawCenterRay()
    {
        // 屏幕中心的坐标（屏幕坐标：x和y范围是0到屏幕宽/高）
        Vector2 screenCenter = new Vector2(Screen.width / 2, Screen.height / 2);

        // 从摄像机（主相机）向屏幕中心位置创建射线
        Ray ray = Camera.main.ScreenPointToRay(screenCenter);

        // 在Scene视图绘制射线（仅编辑器可见，运行时不会显示在Game视图）
        Debug.DrawRay(ray.origin, ray.direction * rayDistance, rayColor);
    }

    /// <summary>
    /// 执行射线检测并打印击中物体名称
    /// </summary>
    void CheckRaycastHit()
    {
        // 创建屏幕中心的射线
        Ray ray = Camera.main.ScreenPointToRay(new Vector2(Screen.width / 2, Screen.height / 2));
        // 存储射线检测结果
        RaycastHit hitInfo;

        // 执行射线检测
        if (Physics.Raycast(ray, out hitInfo, rayDistance))
        {
            // 检测到物体时，打印物体名称
            string hitObjectName = hitInfo.collider.gameObject.name;
            Debug.Log($"射线击中物体：{hitObjectName}");
            if (hitObjectName == "balloon")
            {
                Destroy(hitInfo.collider.gameObject);
                GameManager.Instance.KillBalloon();
            }
        }
    }
}