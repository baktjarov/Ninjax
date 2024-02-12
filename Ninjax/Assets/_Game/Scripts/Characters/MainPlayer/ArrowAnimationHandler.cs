using Gameplay;
using UnityEngine;

public class ArrowAnimationHandler : MonoBehaviour
{
    [SerializeField] private string _onArrowTaken_Key = "OnArrowTaken";
    [SerializeField] private string _onArrowShoot_Key = "OnShoot";

    [Header("Components")]
    [SerializeField] private Transform _arrow;
    [SerializeField] private AnimationEvents _animationEvents;

    private void OnEnable()
    {
        _animationEvents.onAnimationEvent += OnAnimationEvent;
    }

    private void OnDisable()
    {
        _animationEvents.onAnimationEvent -= OnAnimationEvent;
    }

    private void OnAnimationEvent(string key)
    {
        if (key == _onArrowTaken_Key)
        {
            _arrow.gameObject.SetActive(true);
        }

        if (key == _onArrowShoot_Key)
        {
            _arrow.gameObject.SetActive(false);
        }
    }
}