using UnityEngine;
public class EnemyView : MonoBehaviour
{
    public Enemy enemy;
    public HearRunning HearRunning;

    private void Awake()
    {
        enemy = GetComponent<Enemy>();
        HearRunning = new HearRunning(enemy);
    }
}