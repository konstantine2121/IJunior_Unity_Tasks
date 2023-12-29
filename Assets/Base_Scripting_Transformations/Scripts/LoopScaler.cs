using UnityEngine;

namespace Assets.Base_Scripting_Transformations.Scripts
{
    //Невнимательно прочитал задание, поэтому этот скрипт можно не проверять.
    public class LoopScaler : MonoBehaviour
    {
        private const float BaseScale = 1f;

        [SerializeField] [Range(1,2)] private float _targetScale = 1;
        [SerializeField] [Range(0, 1)] private float _scaleSpeed;

        private ScaleOperation _scaleDirection = ScaleOperation.Increase;

        private void Update()
        {
            Scale();
        }

        private void Scale()
        {
            var scaleSpeed = _scaleSpeed * Time.deltaTime;
        //    var scaleMultiplier = 1 
            Vector3 scaleDleta = 

            
            transform.localScale = transform.localScale ;
        }


        private void SwitchDirection()
        {
            _scaleDirection = _scaleDirection == ScaleOperation.Increase ?
                ScaleOperation.Decrease :
                ScaleOperation.Increase;
        }

        private enum ScaleOperation
        {
            Increase,
            Decrease
        }
    }
}