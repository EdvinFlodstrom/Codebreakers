using UnityEngine;

public class DroneMovement : MonoBehaviour
{
    [SerializeField] private Transform firepoint;
    [SerializeField] private Transform leftEdge;
    [SerializeField] private Transform rightEdge;

    [SerializeField] private float speed;
    private bool movingLeft = true;

    void FixedUpdate()
    {
        if (movingLeft)
        {
            if (transform.position.x >= leftEdge.position.x)
                MoveInDirection(-1);
            else
                movingLeft = !movingLeft;
        }
        else
        {
            if (transform.position.x <= rightEdge.position.x)
                MoveInDirection(1);
            else
                movingLeft = !movingLeft;
        }
    }
    private void MoveInDirection(float _direction)
    {
        if (_direction == 1) transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
        else transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);

        transform.position = new Vector3(transform.position.x + Time.deltaTime * speed * _direction, transform.position.y, transform.position.z);
    }
}
