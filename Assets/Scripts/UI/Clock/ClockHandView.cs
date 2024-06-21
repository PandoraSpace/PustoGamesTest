using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI.Clock
{
    public class ClockHandView : MonoBehaviour, IDragHandler
    {
        private Transform _transform;

        public Transform Transform => _transform;
        public event Action<Vector2, Transform> OnDragEvent;
        private Camera _camera;

        public void OnDrag(PointerEventData eventData)
        {
            OnDragEvent?.Invoke(_camera.ScreenToWorldPoint(eventData.position), _transform);
        }
        
        private void Awake()
        {
            _transform = transform;
            _camera = Camera.main;
        }
    }
}