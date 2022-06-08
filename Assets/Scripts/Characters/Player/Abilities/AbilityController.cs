using System.Collections.Generic;
using System.Linq;
using Extensions;
using UnityEngine;

namespace Characters.Player.Abilities
{
    public class AbilityController : MonoBehaviour
    {
        private GameObject _player;
        private PlayerMoving _playerMoving;
        private PlayerPursuitTarget _playerPursuitTarget;
        private Attack _playerAttack;
        private Animator _playerAnimator;
        
        private List<Ability> abilities = new List<Ability>();
        private List<IDeactivated> deactivatedAbilities = new List<IDeactivated>();
        
        void Start()
        {
            _player = FindObjectOfType<PlayerCharacteristics>().gameObject;
            _playerMoving = _player.GetComponent<PlayerMoving>();
            _playerPursuitTarget = _player.GetComponent<PlayerPursuitTarget>();
            _playerAttack = _player.GetComponent<Attack>();
            _playerAnimator = _player.GetComponent<Animator>();
            
            abilities.Add(new BufDamage(_player));

            CompleteDeactivatedAbilities();
        }

        private void Update()
        {
            DeactivateAbility();
        }

        private void CompleteDeactivatedAbilities()
        {
            foreach (Ability ability in abilities)
            {
                if (ability is IDeactivated)
                {
                    deactivatedAbilities.Add((IDeactivated) ability);
                }
            }
        }

        public bool TryActivateAbility(int abilityId)
        {
            Ability ability = abilities.ElementAt(abilityId);

            if (!ability.TryActivate())
                return false;
            
            FreezePlayer();
            Debug.Log(_playerAnimator.FindAnimationClip(ability.NameAnimation).length);
            Invoke(nameof(UnfreezePlayer), _playerAnimator.FindAnimationClip(ability.NameAnimation).length);
            _playerAnimator.SetTrigger(ability.AnimationId);
            
            return true;
        }

        public float CooldownAbility(int abilityId)
        {
            return abilities.ElementAt(abilityId).Cooldown;
        }

        private void DeactivateAbility()
        {
            foreach (IDeactivated deactivatedAbility in deactivatedAbilities)
            {
                deactivatedAbility.TryDeactivate();
            }
        }

        private void FreezePlayer()
        {
            _playerMoving.IsFreeze = true;
            _playerPursuitTarget.IsFreeze = true;
            _playerAttack.IsFreeze = true;
        }

        private void UnfreezePlayer()
        {
            _playerMoving.IsFreeze = false;
            _playerPursuitTarget.IsFreeze = false;
            _playerAttack.IsFreeze = false;
        }
    }
}
