using UnityEngine;

namespace Assets.Base_Scripting_Transformations.Scripts
{
    public class LoopScaler : MonoBehaviour
    {
        private const float BaseScale = 1f;

        [SerializeField] [Range(1,2)] private float _targetScale = 1;
        [SerializeField] [Range(0, 1)] private float _scaleSpeed;

        private ScaleOperation _scaleOperation = ScaleOperation.Increase;

        private enum ScaleOperation
        {
            Increase,
            Decrease
        }

        private bool Increase => _scaleOperation == ScaleOperation.Increase;

        private void Update()
        {
            Scale();
        }

        private void Scale()
        {
            if(_targetScale == BaseScale)
            {
                return;
            }

            var scale = transform.localScale.x;
            var scaleDelta = _scaleSpeed * Time.deltaTime;
            var targetScale = Increase ? _targetScale : BaseScale;

            var maxScaleDelta = Mathf.Abs(targetScale - scale);
            scaleDelta = Mathf.Min(scaleDelta, maxScaleDelta);
            
            var newScale = scale + (Increase ? scaleDelta : -scaleDelta);

            transform.localScale = new Vector3(newScale, newScale, newScale);

            if (AlmostEquals(newScale, targetScale))
            {
                SwitchScaleOperation();
            }
        }

        private bool AlmostEquals(float a, float b)
        {
            const float accuracy = 0.0000001f;

            return Mathf.Abs(a - b) < accuracy;
        }

        private void SwitchScaleOperation()
        {
            _scaleOperation = Increase ?
                ScaleOperation.Decrease :
                ScaleOperation.Increase;
        }        
    }
}