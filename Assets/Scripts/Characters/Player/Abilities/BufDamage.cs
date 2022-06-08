using UnityEngine;

using Characters.Player.Abilities.Data;

namespace Characters.Player.Abilities
{
    public class BufDamage : Ability, IDeactivated
    {
        private DataBufDamage _dataBufDamage;
        private PlayerCharacteristics _playerCharacteristics;

        private float _shutdownTimeBufDamage;
        private bool _isActive;
    
        public BufDamage(GameObject player) : base(Animator.StringToHash("Buf"))
        {
            _dataBufDamage = Resources.Load<DataBufDamage>("ScriptableObjects/DataBufDamage");
            _dataAbility = _dataBufDamage;
            
            _playerCharacteristics = player.GetComponent<PlayerCharacteristics>();
        }

        public override bool TryActivate()
        {
            if (!base.TryActivate())
                return false;
            
            if (_dataBufDamage.RageCost > _playerCharacteristics.CurrentRage)
                return false;
            
            _shutdownTimeBufDamage = Time.time + _dataBufDamage.Duration;
            
            if (!_isActive)
            {
                _playerCharacteristics.Damage += _dataBufDamage.AddedValue;
                
                EventManagerUIBasicCharacteristics.BufActivate();
                _isActive = true;
            }
            
            return true;
        }

        public void TryDeactivate()
        {
            if (!_isActive
                || _shutdownTimeBufDamage > Time.time)
                return;
                    
            _playerCharacteristics.Damage -= _dataBufDamage.AddedValue;
            EventManagerUIBasicCharacteristics.BufDeactivate();
            _isActive = false;
        }
    }
}
