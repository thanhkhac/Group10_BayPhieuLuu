using UnityEngine;

namespace ThanhNK
{
    public static class PlayerData 
    {
        public static int PLayerHealth { get; set; } = 100;
        public static int PLayerMana { get; set; } = 100;
        
        public static int OldPLayerHealth { get; set; } = 100;
        public static int OldPlayerMana { get; set; } = 100;
        
        public static int Point { get; set; } = 0;
        
        public static int Kills { get; set; } = 0;
        
        public static int OldKills { get; set; } = 0;
        
        public static int OldPoints { get; set; } = 0;
        
        public static int CurrentLevel { get; set; } = 1;
    }
}