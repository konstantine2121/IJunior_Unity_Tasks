using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class Movement : MonoBehaviour
{
    [SerializeField] private int _rightLevelEdge = 20;
    
    [SerializeField] private float _speed;

    private bool _stop = false;
    
    private Rigidbody2D rigidbody;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        rigidbody.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
    }

    private void FixedUpdate()
    {
        if (!_stop)
        {
            transform.Translate(Time.fixedDeltaTime * _speed, 0, 0);
        }

        if (transform.position.x > _rightLevelEdge)
        {
            _stop = true;
        }
    }


}
