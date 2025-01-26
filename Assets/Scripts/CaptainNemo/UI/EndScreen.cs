using UnityEngine;
using UnityEngine.SceneManagement;
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
            restartButton.onClick.RemoveListener(OnEndButtonClicked);
            SceneManager.LoadScene("Game");
        }
    }
}
