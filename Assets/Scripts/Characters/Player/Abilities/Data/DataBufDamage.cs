using UnityEngine;

namespace Characters.Player.Abilities.Data
{
    [CreateAssetMenu(fileName = "DataBufDamage", menuName = "Scriptable Object/Data Buf Damage")]
    public class DataBufDamage : DataAbility
    {
        [SerializeField] private int addedValue;
        [SerializeField] private float duration;

        public int AddedValue => addedValue;
        public float Duration => duration;
    }
}
