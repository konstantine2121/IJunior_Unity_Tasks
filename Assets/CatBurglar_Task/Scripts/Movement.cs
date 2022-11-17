using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private int _rightLevelEdge = 20;
    [SerializeField] private float _speed;
    private bool _stop = false;

    // Update is called once per frame
    private void Update()
    {
        if (!_stop)
        {
            transform.Translate(Time.deltaTime * _speed, 0, 0);
        }

        if(transform.position.x > _rightLevelEdge)
        {
            _stop = true;
        }
    }


}
