using UnityEngine;

public class MemberModel : MonoBehaviour, IDataPersistence
{
    public MemberInfo info;
    public SkillBase readySkill;

    public bool isRunning;
    public bool isCrouching;
    public bool isInteractedWithSomething;
    public GameObject selection;

    public Transform soundSource;
    public float runningNoiseRadius;
    public void LoadData(GameData data)
    {
        data.memberPositions.TryGetValue(info.id, out Vector3 position);
        transform.localPosition = position;
        data.memberState.TryGetValue(info.id, out isCrouching);
    }

    public void SaveData(ref GameData data)
    {
        if (data.memberPositions.ContainsKey(info.id)) data.memberPositions.Remove(info.id);
        data.memberPositions.Add(info.id,transform.localPosition);
        
        if (data.memberState.ContainsKey(info.id)) data.memberState.Remove(info.id);
        data.memberState.Add(info.id,isCrouching);
    }
}