using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(EnemyModel))]
public class EnemyModelEditor : Editor
{
    private void OnSceneGUI()
    {
        EnemyModel enemyModel = (EnemyModel)target;

        if (enemyModel.patrolGizmos)
        {
            Handles.color = Color.cyan;
            for (var i = 0; i < enemyModel.waypoints.Count; i++)
            {
                Handles.DrawSolidArc(enemyModel.waypoints[i].position, Vector3.up, Vector3.forward, 360, 0.2f);
                if (i != enemyModel.waypoints.Count - 1)
                {
                    Handles.DrawLine(enemyModel.waypoints[i].position, enemyModel.waypoints[i + 1].position, 0.1f);
                }
            }
        }
        /*
        Handles.color = Color.white;
        Handles.DrawWireArc(enemyModel.transform.position,Vector3.up,-Vector3.right,180,5);
        */

        if (enemyModel.searchGizmos)
        {
            Handles.color = Color.green;
        }
    }
}