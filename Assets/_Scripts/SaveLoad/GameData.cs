using UnityEngine;

namespace TopDown.SaveLoad
{
    [System.Serializable]
    public class GameData
    {
        public Vector3 playerPosition;
        //public int TotalAmmo; TODO
        //public int CurrentAmmoInClip; TODO

        public GameData()
        {
            this.playerPosition = Vector3.zero;
            //this.TotalAmmo = 0; TODO
            //this.CurrentAmmoInClip = 0; TODO
        }
    }
}
