using System;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer2D_Task
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class MedicalKitCollector : MonoBehaviour, IMedicalKitCollector
    {
        private const CollectableTypes TypeToCollect = CollectableTypes.MedicalKit;

        [SerializeField] private int _startAmountOfKits;
        [SerializeField] private int _maxAmountOfKits;

        private readonly Queue<float> _medkits = new Queue<float>();

        public event Action<int> NumberOfKitsChanged;            

        public int MedicalKits => _medkits.Count;

        public int MaxMedicalKits => _maxAmountOfKits;

        public float UseMedicalKit()
        {
            var heal = 0f;

            if (_medkits.Count > 0)
            {
                heal = _medkits.Dequeue();
                NumberOfKitsChanged?.Invoke(_medkits.Count);
            }

            return heal;
        }

        void Awake()
        {
            InitializeMedkits();
        }

        private void InitializeMedkits()
        {
            _medkits.Clear();

            for (int i=0; i< _startAmountOfKits; i++)
            {
                _medkits.Enqueue(MedicalKit.DefaultHealValue);
            }
            NumberOfKitsChanged?.Invoke(_medkits.Count);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            TryCollectItem(collision.gameObject);
        }

        private void OnTriggerEnter2D(Collider2D collider)
        {
            TryCollectItem(collider.gameObject);
        }

        private void TryCollectItem(GameObject gameObject)
        {
            if (CheckIsCollectable(gameObject, out MedicalKit collectable) &&  (MedicalKits < _maxAmountOfKits))
            {
                Collect(collectable);
            }
        }

        private bool CheckIsCollectable(GameObject gameObject, out MedicalKit collectable)
        {
            if (gameObject.TryGetComponent(out collectable))
            {
                return true;
            }

            return false;
        }

        private void Collect(MedicalKit collectable)
        {
            if (collectable.CollectableType != TypeToCollect)
            {
                return;
            }

            _medkits.Enqueue(collectable.AmountOfHeal);
            NumberOfKitsChanged?.Invoke(_medkits.Count);

            collectable.Take();
        }

        private void OnValidate()
        {
            if (_startAmountOfKits < 0)
            {
                _startAmountOfKits = 0;
            }

            if (_maxAmountOfKits < _startAmountOfKits)
            {
                _maxAmountOfKits = _startAmountOfKits;
            }

            if (_startAmountOfKits > _maxAmountOfKits)
            {
                _maxAmountOfKits = _startAmountOfKits;
            }
        }
    }
}