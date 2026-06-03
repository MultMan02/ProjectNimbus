using UniRx;
using UnityEngine;
using TopDown.Audio;

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

        [Header("Sound Effects")]
        [SerializeField] private AudioClip shotSound;
        [SerializeField] private AudioClip reloadSound;
        [SerializeField] private AudioClip emptySound;
        
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
            if (CurrentAmmoInClip.Value <= 0)
            {
                SoundManager.Instance?.PlaySound(emptySound);
                return;
            }
            
            GameObject bullet = Instantiate(bulletPrefab, firepoint.position, firepoint.rotation, null);
            bullet.GetComponent<Projectile>().ShootBullet(firepoint);
            
            muzzleFlashAnimator.SetTrigger("shoot");
            cooldownTimer = 0f;
            CurrentAmmoInClip.Value--;
            SoundManager.Instance?.PlaySound(shotSound);
        }

        private void Reload()
        {
            //Check if you have ammo to reload
            if (TotalAmmo.Value <= 0) return;
            
            //How much ammo is missing in a clip
            int missingAmmo = clipSize - CurrentAmmoInClip.Value;
            
            //Return if no ammo is missing
            if (missingAmmo == 0) return;
            
            int reloadAmmo;
            
            
            
            if (TotalAmmo.Value >= missingAmmo)
                reloadAmmo = missingAmmo;
            else
                reloadAmmo = TotalAmmo.Value;
            
            CurrentAmmoInClip.Value += reloadAmmo;
            TotalAmmo.Value -= reloadAmmo;
            SoundManager.Instance?.PlaySound(reloadSound);
        }
        
        #region Input
        private void OnShoot()
        {
            Shoot();
        }

        private void OnReload()
        {
            Reload();
        }
        #endregion
    }
}
