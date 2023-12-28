using UnityEngine;

namespace Assets.Base_Scripting_Transformations.Scripts
{
    public class Rotator : MonoBehaviour
    {
        [SerializeField] private Vector3 _rotationAxis = Vector3.up;
        [SerializeField][Range(0, 50)] private float _rotationSpeed;

        private void Update()
        {
            transform.Rotate(_rotationAxis, _rotationSpeed * Time.deltaTime);
        }
    }
}