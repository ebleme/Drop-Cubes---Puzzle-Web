using UnityEngine;

public class RaycastTest : MonoBehaviour
{
    [SerializeField] private LayerMask layerMask;
    
    private bool isHit;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        isHit = Physics.Raycast(transform.position, transform.right, out RaycastHit hit, 10, layerMask );
        
        if (isHit)
        {
            Debug.Log(hit.transform.name);
        }
    }

    private void OnDrawGizmos()
    {
        if (isHit)
        {
            Gizmos.color = Color.purple;
            Gizmos.DrawLine(transform.position, transform.position + transform.right * 10);
        }
    }
}