using UnityEngine;
using UnityEngine.UI;

namespace CaptainNemo.UI
{
    public class EndScreen : MonoBehaviour
    {
        [SerializeField] private Button restartButton;

        private void Start()
        {
            restartButton.onClick.AddListener(OnEndButtonClicked);
        }

        private void OnEndButtonClicked()
        {
            //GameManager.Instance.EndGame();
            restartButton.onClick.RemoveListener(OnEndButtonClicked);
            gameObject.SetActive(false);
        }
    }
}
