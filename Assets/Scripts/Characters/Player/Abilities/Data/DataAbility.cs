using UnityEngine;

namespace Characters.Player.Abilities.Data
{
    public class DataAbility : ScriptableObject
    {
        [SerializeField] private string nameAnimation;
        [SerializeField] private float rageCost;
        [SerializeField] private float cooldown;

        public string NameAnimation => nameAnimation;
        public float RageCost => rageCost;
        public float Cooldown => cooldown;
    }
}
