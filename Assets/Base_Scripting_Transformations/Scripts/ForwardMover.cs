using UnityEngine;

namespace Assets.Base_Scripting_Transformations.Scripts
{
    public class ForwardMover : MonoBehaviour
    {
        [SerializeField][Range(0, 5)] private float _moveSpeed;
        
        private Vector3 Direction => transform.forward;

        public void Update()
        {
            var speed = _moveSpeed * Time.deltaTime;
            var translation = Direction * speed;

            transform.Translate(translation);
        }
    }
}