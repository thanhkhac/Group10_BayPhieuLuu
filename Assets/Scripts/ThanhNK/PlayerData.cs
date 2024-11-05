using UnityEngine;

namespace ThanhNK
{
    public static class PlayerData 
    {
        public static int PLayerHealth { get; set; } = 100;
        public static int PLayerMana { get; set; } = 100;
        
        public static int OldPLayerHealth { get; set; } = 100;
        public static int OldPlayerMana { get; set; } = 100;
        
        public static int CurrentLevel { get; set; } = 0;
    }
}