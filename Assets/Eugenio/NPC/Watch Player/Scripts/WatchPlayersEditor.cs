using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(WatchPlayers))]
public class WatchPlayersEditor : Editor
{
    private void OnSceneGUI()
    {
        WatchPlayers fov = (WatchPlayers)target;
        Handles.color = Color.white;
        Handles.DrawWireArc(fov.transform.position, Vector3.up, Vector3.forward, 360, fov.WatchDistance);

        Vector3 viewAngle01 = DirectionFromAngle(fov.transform.eulerAngles.y, -fov.FOV_F / 2);
        Vector3 viewAngle02 = DirectionFromAngle(fov.transform.eulerAngles.y, fov.FOV_F / 2);



        Handles.color = Color.yellow;
        Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngle01 * fov.WatchDistance);
        Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngle02 * fov.WatchDistance);

        Handles.color = Color.gray;

        for (int i = 3; i <= 10; i++)
        {
            Vector3 viewAngleLeft = DirectionFromAngle(fov.transform.eulerAngles.y, -fov.FOV_F / i);
            Vector3 viewAngleRight = DirectionFromAngle(fov.transform.eulerAngles.y, fov.FOV_F / i);

            Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngleLeft * fov.WatchDistance);
            Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngleRight * fov.WatchDistance);
        }

        if (fov.IsTargetDetected)
        {
            Handles.color = Color.green;
            Handles.DrawLine(fov.transform.position, fov.transform.position);
        }
    }

    private Vector3 DirectionFromAngle(float eulerY, float angleInDegrees)
    {
        angleInDegrees += eulerY;

        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
}
