using UniRx;
using TMPro;
using UnityEngine;
using TopDown.Shooting;

namespace TopDown.UI
{
    public class AmmoCounter : MonoBehaviour
    {
        private CompositeDisposable subscriptions = new CompositeDisposable();
        
        [SerializeField] private TextMeshProUGUI ammoCounterText;
        [SerializeField] private GunController gunController;
        private int ammoInClip;
        private int totalAmmo;
        
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
        }
    }
}
