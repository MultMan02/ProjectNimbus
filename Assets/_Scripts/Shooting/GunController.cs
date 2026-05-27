using UniRx;
using UnityEngine;

namespace TopDown.Shooting
{
    public class GunController : MonoBehaviour
    {
        [Header("Cooldown")]
        [SerializeField] private float cooldown = 0.25f;
        private float cooldownTimer;
        
        [Header("References")]
        [SerializeField] private GameObject bulletPrefab;
        [SerializeField] private Transform firepoint;
        [SerializeField] private Animator muzzleFlashAnimator;
        
        [Header("Ammo")]
        [SerializeField] private int initialAmmo;
        [SerializeField] private int clipSize;
        
        public IntReactiveProperty TotalAmmo { get; private set; } = new IntReactiveProperty(0);
        public IntReactiveProperty CurrentAmmoInClip { get; private set; } = new IntReactiveProperty(0);

        private void Awake()
        {
            TotalAmmo.Value = initialAmmo;
            
            if(initialAmmo <= clipSize)
                CurrentAmmoInClip.Value = initialAmmo;
            else
            {
                CurrentAmmoInClip.Value = clipSize;
            }
        }
        
        private void Update()
        {
            cooldownTimer += Time.deltaTime;
        }

        private void Shoot()
        {
            if (cooldownTimer < cooldown) return;
            if (CurrentAmmoInClip.Value <= 0) return;
            
            GameObject bullet = Instantiate(bulletPrefab, firepoint.position, firepoint.rotation, null);
            bullet.GetComponent<Projectile>().ShootBullet(firepoint);
            
            muzzleFlashAnimator.SetTrigger("shoot");
            cooldownTimer = 0f;
            CurrentAmmoInClip.Value--;
        }
        
        #region Input
        private void OnShoot()
        {
            Shoot();
        }
        #endregion
    }
}
