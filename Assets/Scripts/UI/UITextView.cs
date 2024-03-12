using TMPro;
using UnityEngine;

namespace UI
{
    public enum TextTypes
    {
        HitPoints, Bullet, Kills
    }
    public sealed class UITextView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _controlledText;
        public TextTypes type;

        public void SetText(string text)
        {
            _controlledText.text = text;
        }
    }
}
