using UnityEngine;
using UnityEngine.UI;

namespace CaptainNemo.UI
{
    public class StartScreen: MonoBehaviour
    {
        [SerializeField]private Button StartButton;

        private void OnEnable()
        {
            StartButton.onClick.AddListener(StartGame);
        }

        private void StartGame()
        {
            //GameManager.Instance.StartGame();
            StartButton.onClick.RemoveListener(StartGame);
            gameObject.SetActive(false);
        }
    }
}
