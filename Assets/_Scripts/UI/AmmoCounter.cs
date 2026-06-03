using UniRx;
using TMPro;
using UnityEngine;
using DG.Tweening;
using TopDown.Shooting;

namespace TopDown.UI
{
    public class AmmoCounter : MonoBehaviour
    {
        private CompositeDisposable subscriptions = new CompositeDisposable();
        
        [Header("References")]
        [SerializeField] private TextMeshProUGUI ammoCounterText;
        [SerializeField] private GunController gunController;
        private int ammoInClip;
        private int totalAmmo;
        
        [Header("Popup Effect")]
        [SerializeField] private Vector2 popupIntensity;
        [SerializeField] private float popupDuration;
        
        private void OnEnable()
        {
            gunController.CurrentAmmoInClip.ObserveEveryValueChanged(property => property.Value).Subscribe(value =>
                {
                    ammoInClip = value;
                    UpdateAmmoCounter(ammoInClip, totalAmmo);
                }).AddTo(subscriptions);
            
            gunController.TotalAmmo.ObserveEveryValueChanged(property => property.Value).Subscribe(value =>
                {
                    totalAmmo = value;
                    UpdateAmmoCounter(ammoInClip, totalAmmo);
                }).AddTo(subscriptions);
        }
        private void OnDisable()
        {
            subscriptions.Clear();
        }
        
        private void UpdateAmmoCounter(int currentAmmo, int totalAmmo)
        {
            ammoCounterText.text = $"{currentAmmo}/{totalAmmo}";
            transform.DOPunchScale(popupIntensity, popupDuration)//Popup Animation
                     .OnComplete(() => transform.DORewind());//Reset the transform to initial scale (just in case)
        }
    }
}
