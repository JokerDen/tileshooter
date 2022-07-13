using UnityEngine;

public class PlayerAimMover : MonoBehaviour
{
    public LayerMask aimLayerMask;
    
    private Vector3 startPos;

    private bool launched;

    private Rigidbody[] bodies;

    public Camera cam;

    private Pole[] poles;

    private void Start()
    {
        startPos = transform.position;

        bodies = GetComponentsInChildren<Rigidbody>();
        foreach (var body in bodies)
        {
            body.isKinematic = true;
            body.collisionDetectionMode = CollisionDetectionMode.ContinuousSpeculative;
        }

        poles = FindObjectsOfType<Pole>();
    }

    public Rigidbody[] hands;
    public Transform[] handPoints;

    public float force;
    public float connectDistance;

    private Pole target;
    
    void Update()
    {
        if (launched)
        {
            // ControlHands();

            return;
        }
        
        var ray = cam.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, float.MaxValue, aimLayerMask, QueryTriggerInteraction.Ignore))
        {
            var pos = startPos;
            pos.x = hit.point.x;
            transform.position = pos;
        }

        if (Input.GetMouseButtonUp(0))
        {
            launched = true;
            foreach (var body in bodies)
            {
                body.isKinematic = false;
                body.velocity = Vector3.zero;
            }
        }
    }

    private void ControlHands()
    {
        if (Input.GetMouseButtonDown(0))
        {
            target = null;
            float closestDist = float.MaxValue;
            foreach (var pole in poles)
            {
                var dist = Vector3.Distance(handPoints[0].position, pole.transform.position);
                if (dist < closestDist)
                {
                    target = pole;
                    closestDist = dist;
                }
            }
        }

        if (Input.GetMouseButton(0))
        {
            if (target != null)
            {
                for (int i = 0; i < hands.Length; i++)
                {
                    var hand = hands[i];
                    var handPoint = handPoints[i];

                    var dir = target.transform.position - handPoint.position;
                    hand.AddForceAtPosition(dir.normalized * force, handPoint.position, ForceMode.Impulse);
                }
            }
        }

        if (Input.GetMouseButtonUp(0))
            target = null;
    }
}
