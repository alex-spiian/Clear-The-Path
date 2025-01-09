using DG.Tweening;
using UnityEngine;

namespace ClearThePath
{
    public class PlayerMover : MonoBehaviour
    {
        [SerializeField] private float _arcHeight;
        [SerializeField] private float _arcDuration;
        [SerializeField] private float _jumpHeight;
        [SerializeField] private float _jumpDuration;
        [SerializeField] private int _jumpsCount;

        public void Move(Transform startMovingPoint, Transform exitPoint)
        {
            var midPoint = new Vector3(
                (transform.position.x + startMovingPoint.position.x) / 2,
                Mathf.Max(transform.position.y, startMovingPoint.position.y) + _arcHeight,
                (transform.position.z + startMovingPoint.position.z) / 2
            );

            var sequence = DOTween.Sequence();

            sequence.Append(transform.DOPath(
                new[] { transform.position, midPoint, startMovingPoint.position },
                _arcDuration,
                PathType.CatmullRom
            ).SetEase(Ease.OutQuad));

            sequence.AppendCallback(() =>
            {
                JumpToExit(startMovingPoint, exitPoint);
            });
        }

        private void JumpToExit(Transform startMovingPoint, Transform exitPoint)
        {
            var totalDistance = Vector3.Distance(startMovingPoint.position, exitPoint.position);
            var direction = (exitPoint.position - startMovingPoint.position).normalized;
            var jumpStep = totalDistance / _jumpsCount;
            var currentJumpPosition = startMovingPoint.position;

            var jumpSequence = DOTween.Sequence();
            for (var i = 0; i < _jumpsCount; i++)
            {
                currentJumpPosition += direction * jumpStep;
                jumpSequence.Append(transform.DOJump(
                    new Vector3(currentJumpPosition.x, currentJumpPosition.y, currentJumpPosition.z),
                    _jumpHeight,
                    1,
                    _jumpDuration
                ).SetEase(Ease.OutQuad));
            }
        }
    }
}