using UnityEngine;

namespace Gameplay
{
    public class PatrolPoint: MonoBehaviour
    {
        [SerializeField] private Transform _point;
        [SerializeField] private float _patrolTime = 5f;

        public Transform Point { get { return _point; } }
        public float patrolTime { get { return _patrolTime; } }
    }
}