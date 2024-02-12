using UnityEngine;

namespace Gameplay
{
    public class PatrolPoint : MonoBehaviour
    {
        [field: SerializeField] public float patrolTime { get; private set; } = 2;
    }
}