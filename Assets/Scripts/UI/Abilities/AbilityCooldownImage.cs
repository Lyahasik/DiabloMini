using UnityEngine;
using UnityEngine.UI;

namespace UI.Ebilities
{
    [RequireComponent(typeof(Image))]
    public class AbilityCooldownImage : MonoBehaviour
    {
        private Image _image;

        private float _cooldownTime;
        private float _shutdownTime;
        private float _cooldownStartTime;
        private bool _isActivate;

        private void Start()
        {
            _image = GetComponent<Image>();
        }

        private void Update()
        {
            Cooldown();
        }

        private void Cooldown()
        {
            if (!_isActivate)
                return;

            float currentTime = Time.time;
            if (_shutdownTime <= currentTime)
            {
                _image.fillAmount = 0;
                _isActivate = false;
            }
            else
            {
                _image.fillAmount = (_shutdownTime - currentTime) / _cooldownTime;
            }
        }

        public void StartCooldown(float cooldownTime)
        {
            _cooldownTime = cooldownTime;
            _cooldownStartTime = Time.time;
            _shutdownTime = _cooldownStartTime + cooldownTime;
            _isActivate = true;
        }
    }
}
