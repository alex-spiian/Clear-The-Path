using DG.Tweening;
using UnityEngine;

namespace ClearThePath.Environment
{
    public class Door : MonoBehaviour
    {
        [SerializeField] private Transform _doorTransforn;
        [SerializeField] private Vector3 _openRotation;
        [SerializeField] private Vector3 _closeRotation;
        [SerializeField] private float _animationDuration;
        
        public void Open()
        {
            _doorTransforn.DOLocalRotate(_openRotation, _animationDuration);
        }
        
        public void Close()
        {
            _doorTransforn.DOLocalRotate(_closeRotation, _animationDuration);
        }
    }
}