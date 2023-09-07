// AgentLinkMover.cs

using System;
using UnityEngine;
using System.Collections;
using UnityEngine.AI;
using UnityEngine.Events;

public enum OffMeshLinkMoveMethod {
   Teleport,
   NormalSpeed,
   Parabola,
   Curve,
   Climb
}
 
[RequireComponent (typeof (NavMeshAgent))]
public class AgentLinkMover : MonoBehaviour
{
  public delegate void LinkEvent(OffMeshLink offMeshLink);
  public LinkEvent onLinkStarts;
  public LinkEvent onLinkEnds;
  public float parabolaJumpDuration;

  private Vector3 startPos;
  private Vector3 endPos;
  private OffMeshLinkData data;
  private float normalizedTime;
  private float yOffset;
  private Vector3 relativePos;

  public OffMeshLinkMoveMethod method = OffMeshLinkMoveMethod.Parabola;
   public AnimationCurve curve = new AnimationCurve ();
   IEnumerator Start () {
     NavMeshAgent agent = GetComponent<NavMeshAgent> ();
     agent.autoTraverseOffMeshLink = false;
     while (true) {
       if (agent.isOnOffMeshLink) {
         onLinkStarts?.Invoke(agent.currentOffMeshLinkData.offMeshLink);
         if (method == OffMeshLinkMoveMethod.NormalSpeed)
           yield return StartCoroutine (NormalSpeed (agent));
         else if (method == OffMeshLinkMoveMethod.Parabola)
           yield return StartCoroutine (Parabola (agent, 2.0f, parabolaJumpDuration));
         else if (method == OffMeshLinkMoveMethod.Curve)
           yield return StartCoroutine (Curve (agent, 0.5f));
         else if (method == OffMeshLinkMoveMethod.Climb)
           yield return StartCoroutine (Climb (agent));
         agent.CompleteOffMeshLink ();
         onLinkEnds?.Invoke(agent.currentOffMeshLinkData.offMeshLink);
       }
       yield return null;
     }
   }
   IEnumerator NormalSpeed (NavMeshAgent agent) {
     data = agent.currentOffMeshLinkData;
     endPos = data.endPos + Vector3.up*agent.baseOffset;
     while (agent.transform.position != endPos) {
       agent.transform.position = Vector3.MoveTowards (agent.transform.position, endPos, agent.speed*Time.deltaTime);
       yield return null;
     }
   }
   IEnumerator Parabola (NavMeshAgent agent, float height, float duration)
   {
     data = agent.currentOffMeshLinkData;
     startPos = agent.transform.position;
     endPos = data.endPos; //+ Vector3.up*agent.baseOffset;
     normalizedTime = 0.0f;
     while (normalizedTime < 1.0f) {
       yOffset = height * 2.0f * (normalizedTime - normalizedTime*normalizedTime);
       agent.transform.position = Vector3.Lerp (startPos, endPos, normalizedTime) + yOffset * Vector3.up;
       normalizedTime += Time.deltaTime / duration;
       yield return null;
     }
   }
   IEnumerator Curve (NavMeshAgent agent, float duration) {
     data = agent.currentOffMeshLinkData;
     startPos = agent.transform.position;
     endPos = data.endPos + Vector3.up*agent.baseOffset;
     normalizedTime = 0.0f;
     while (normalizedTime < 1.0f) {
       yOffset = curve.Evaluate (normalizedTime);
       agent.transform.position = Vector3.Lerp (startPos, endPos, normalizedTime) + yOffset * Vector3.up;
       normalizedTime += Time.deltaTime / duration;
       yield return null;
     }
   }
   
   IEnumerator Climb (NavMeshAgent agent) {
     data = agent.currentOffMeshLinkData;
     startPos = data.startPos;
     endPos = data.endPos + Vector3.up*agent.baseOffset;
     if (data.startPos.y < data.endPos.y) // Ascending
     {
       endPos.z = startPos.z;
       endPos.y -= agent.height;
     }
     else //Descending
     {
       startPos.z = endPos.z;
       startPos.y -= agent.height;
       agent.transform.position = startPos;
     }
     transform.forward = -data.offMeshLink.transform.forward;
     while (agent.transform.position != endPos) {
       transform.position = Vector3.MoveTowards (agent.transform.position, endPos, agent.speed*Time.deltaTime);
       yield return null;
     }
   }
}