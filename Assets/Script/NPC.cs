using UnityEngine;

public class NPC : MonoBehaviour
{
    [SerializeField] private ActionType actionType;

    [SerializeField] private float distanceDetected;
    [SerializeField] private LayerMask mask;

    private void OnMouseDown()
    {
        RaycastHit2D cast = Physics2D.Raycast(transform.position, Vector2.right, distanceDetected, mask);

        if(cast.collider != null)
        {
            Observer.Notify(actionType,0);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;

        Gizmos.DrawLine(transform.position , new Vector3(transform.position.x + distanceDetected , transform.position.y , transform.position.z));
    }
}
