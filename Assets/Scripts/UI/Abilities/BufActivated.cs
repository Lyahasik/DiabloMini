using UnityEngine;
using UnityEngine.UI;

namespace UI.Ebilities
{
    [RequireComponent(typeof(Image))]
    public class BufActivated : MonoBehaviour
    {
        private Image _image;

        private void OnEnable()
        {
            EventManagerUIBasicCharacteristics.OnBufActivate += Activate;
            EventManagerUIBasicCharacteristics.OnBufDeactivate += Deactivate;
        }

        private void Start()
        {
            _image = GetComponent<Image>();
        }

        private void Activate()
        {
            _image.enabled = true;
        }

        private void Deactivate()
        {
            _image.enabled = false;
        }
    }
}
