using UnityEngine;
using UnityEngine.EventSystems;

using Characters.Player.Abilities;

namespace UI.Ebilities
{
    public class ActivationIcon : MonoBehaviour, IPointerDownHandler
    {
        private AbilityController _abilityController;

        [SerializeField] private int iconNumber;
        [SerializeField] private AbilityCooldownImage abilityCooldownImage;

        private void Start()
        {
            _abilityController = FindObjectOfType<AbilityController>();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            int idAbility = iconNumber - 1;

            if (_abilityController.TryActivateAbility(idAbility))
                abilityCooldownImage.StartCooldown(_abilityController.CooldownAbility(idAbility));
        }
    }
}
