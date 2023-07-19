using UnityEngine;

public class Bush : MonoBehaviour
{
    [SerializeField] private Material bushShaderMaterial;
    private void OnTriggerEnter(Collider other)
    {
        //other.GetComponent<MeshRenderer>().material = bushShaderMaterial;
        Debug.Log(other.name);
    }
}