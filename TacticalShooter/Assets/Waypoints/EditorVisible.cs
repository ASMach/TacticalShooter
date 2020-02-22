using UnityEngine;
using UnityEditor;


[ExecuteInEditMode]
public class EditorVisible : MonoBehaviour
{
    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, 0.2f);
    }
}
