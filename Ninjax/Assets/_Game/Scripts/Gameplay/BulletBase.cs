using System.Collections;
using Scriptables;
using UnityEngine;

namespace Gameplay
{
    public class BulletBase : MonoBehaviour
    {
        [SerializeField] private BulletPooling _pooling;
        [SerializeField] private float _lifeTime = 5;

        public void Initialize(BulletPooling pooling)
        {
            _pooling = pooling;
        }

        private void OnEnable()
        {
            StartCoroutine(Put_Coroutine(_lifeTime));
        }

        private void OnDisable()
        {
            StopCoroutine(Put_Coroutine(_lifeTime));
        }

        private IEnumerator Put_Coroutine(float delay)
        {
            yield return new WaitForSecondsRealtime(delay);
            _pooling.Put(this);
        }
    }
}