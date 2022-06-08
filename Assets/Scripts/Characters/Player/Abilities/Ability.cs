using UnityEngine;

using Characters.Player.Abilities.Data;

namespace Characters.Player.Abilities
{
    public abstract class Ability
    {
        private readonly int _animationId;
        public int AnimationId => _animationId;
        
        protected DataAbility _dataAbility;
        public string NameAnimation => _dataAbility.NameAnimation;
        public float Cooldown => _dataAbility.Cooldown;
        
        private float _shutdownTime;

        protected Ability(int animationId)
        {
            _animationId = animationId;
        }

        public virtual bool TryActivate()
        {
            if (_shutdownTime > Time.time)
                return false;

            _shutdownTime = Time.time + _dataAbility.Cooldown;
            return true;
        }
    }
}
