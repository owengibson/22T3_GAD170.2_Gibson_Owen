using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace OwenGibson
{
    public class GameOver : MonoBehaviour
    {
        [SerializeField] private GameObject gameOverPanelPrefab;
        [SerializeField] private GameObject canvas;
        [SerializeField] private Aquarium aquarium;

        public void GameOverElements()
        {
            GameObject _gameOverPanel = Instantiate(gameOverPanelPrefab, canvas.transform, false);
            _gameOverPanel.transform.Find("PlayAgainButton").GetComponent<Button>().onClick.AddListener(PlayAgain);

            TextMeshProUGUI totalValueText = _gameOverPanel.transform.Find("Final Value").GetComponent<TextMeshProUGUI>();
            totalValueText.text = "$" + aquarium.totalValue.ToString("0.##");
        }

        private void PlayAgain()
        {
            SceneManager.LoadScene("FishingGame");
        }
    }
}