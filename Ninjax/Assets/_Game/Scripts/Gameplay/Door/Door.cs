using UnityEngine;
using System.Collections;

namespace Gameplay
{
    public class DoubleDoor : MonoBehaviour
    {
        [SerializeField] private Transform leftDoor;
        [SerializeField] private Transform rightDoor;
        [SerializeField] private Vector3 openPositionLeft;
        [SerializeField] private Vector3 openPositionRight;
        [SerializeField] private float moveTime = 1f;

        private Vector3 closedPositionLeft;
        private Vector3 closedPositionRight;
        private bool isOpen = false;

        private void Start()
        {
            closedPositionLeft = leftDoor.localPosition;
            closedPositionRight = rightDoor.localPosition;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player") && !isOpen)
            {
                if (currentCoroutine != null)
                {
                    StopCoroutine(currentCoroutine);
                }
                currentCoroutine = OpenDoors();
                StartCoroutine(currentCoroutine);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player") && isOpen)
            {
                if (currentCoroutine != null)
                {
                    StopCoroutine(currentCoroutine);
                }
                currentCoroutine = CloseDoors();
                StartCoroutine(currentCoroutine);
            }
        }

        private IEnumerator currentCoroutine;

        private IEnumerator OpenDoors()
        {
            isOpen = true;
            Vector3 startPosLeft = leftDoor.localPosition;
            Vector3 startPosRight = rightDoor.localPosition;

            float startTime = Time.time;
            while (Time.time < startTime + moveTime)
            {
                float t = (Time.time - startTime) / moveTime;
                leftDoor.localPosition = Vector3.Lerp(startPosLeft, openPositionLeft, t);
                rightDoor.localPosition = Vector3.Lerp(startPosRight, openPositionRight, t);
                yield return null;
            }
        }

        private IEnumerator CloseDoors()
        {
            isOpen = false;
            Vector3 startPosLeft = leftDoor.localPosition;
            Vector3 startPosRight = rightDoor.localPosition;

            float startTime = Time.time;
            while (Time.time < startTime + moveTime)
            {
                float t = (Time.time - startTime) / moveTime;
                leftDoor.localPosition = Vector3.Lerp(startPosLeft, closedPositionLeft, t);
                rightDoor.localPosition = Vector3.Lerp(startPosRight, closedPositionRight, t);
                yield return null;
            }
        }
    }
}
