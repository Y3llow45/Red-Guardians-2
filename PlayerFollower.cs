using UnityEngine;

public class PlayerFollower : MonoBehaviour
{
    public Transform target;
    public float followSpeed = 5f;
    public float spacing = 1.5f;

    private void Start()
    {
        AssignTarget();
        transform.rotation = Quaternion.Euler(0, 90, 0);
    }

    private void Update()
    {
        if (target == null) return;

        Vector3 desiredPosition = target.position + (transform.position - target.position).normalized * spacing;
        transform.position = Vector3.Lerp(transform.position, desiredPosition, followSpeed * Time.deltaTime);
    }

    private void AssignTarget()
    {
        if (gameObject.name == "SoldierClone")
        {
            target = GameObject.Find("Player")?.transform;
        }
    }
}