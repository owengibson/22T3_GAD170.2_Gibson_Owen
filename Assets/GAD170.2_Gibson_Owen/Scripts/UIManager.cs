using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;

namespace OwenGibson
{
    public class UIManager : MonoBehaviour
    {
        [Header("Prefab References")]
        [SerializeField] private GameObject keepFishPrefab;
        [SerializeField] private GameObject discardFishPrefab;
        [SerializeField] private GameObject fishStatsPrefab;
        [SerializeField] private GameObject aquariumListPrefab;
        [SerializeField] private GameObject fishEatenPrefab;
        [SerializeField] private GameObject gameOverPanelPrefab;

        [Space(10)]
        [Header("Other References")]
        [SerializeField] private GameObject canvas; // not using gameObject in case script is moved.
        [SerializeField] private Aquarium aquarium;
        [SerializeField] private TextMeshProUGUI totalValueText;

        private Slider slider;
        private TextMeshProUGUI textSliderValue;

        private void Start()
        {
            slider = transform.Find("HomeScreen").transform.Find("NumberOfRounds").GetComponent<Slider>();
            textSliderValue = transform.Find("HomeScreen").transform.Find("NumberOfRounds").transform.Find("Value").GetComponent<TextMeshProUGUI>();

            ShowSliderValue();
        }


        // -------------- Instantiating methods --------------//

        public void FindFishUI() // Spawns all UI elements for when a fish is found. 2 buttons and the fish's stats.
        {
            GameObject _keepFish = Instantiate(keepFishPrefab, canvas.transform, false);
            _keepFish.GetComponent<Button>().onClick.AddListener(aquarium.KeepFishButton);

            GameObject _discardFish = Instantiate(discardFishPrefab, canvas.transform, false);
            _discardFish.GetComponent<Button>().onClick.AddListener(aquarium.DiscardFishButton);

            GameObject _fishStatsPanel = Instantiate(fishStatsPrefab, canvas.transform, false);
            _fishStatsPanel.GetComponentInChildren<TextMeshProUGUI>().text = "<b>Species: </b>" + aquarium.newFish.species +
                                                                             "<br><b>Length: </b>" + aquarium.newFish.length +
                                                                             "cm<br><b>Price: </b>$" + aquarium.newFish.price.ToString("0.##");
        }

        public void FishEatenUI() // Spawns all UI elements for when a fish is eaten. Panel with text.
        {
            GameObject _eatenPanel = Instantiate(fishEatenPrefab, canvas.transform, false);
            _eatenPanel.transform.Find("EatenOKButton").GetComponent<Button>().onClick.AddListener(DestroyEatenUI);

            _eatenPanel.transform.Find("Text").GetComponent<TextMeshProUGUI>().text = "Your new fish ate your " + aquarium.fishEaten.length + "cm long " + aquarium.fishEaten.species + "!";
        }

        public void ListAquarium() // Spawns all UI elements to list the current fish in the aquarium. 1 panel, 2 buttons, and a text field.
        {
            GameObject _aquariumListPanel = Instantiate(aquariumListPrefab, canvas.transform, false);
            TextMeshProUGUI _text = _aquariumListPanel.GetComponentInChildren<TextMeshProUGUI>();
            _text.text = "";
            _aquariumListPanel.transform.Find("CloseAquariumButton").GetComponent<Button>().onClick.AddListener(DestroyAquariumListUI);
            _aquariumListPanel.transform.Find("ReleaseFishButton").GetComponent<Button>().onClick.AddListener(aquarium.ReleaseAllFish);

            if (aquarium.fishList.Any())
            {
                foreach (Fish fish in aquarium.fishList)
                {
                    _text.text += fish.species + ", " + fish.length + "cm, $" + fish.price + "<br>";
                }
            }
            else
            {
                _text.text = "No fish in aquarium";
            }
        }

        public void GameOver() // Spawns all UI elements for the game over screen. 1 panel, 1 button, 3 text fields.
        {
            GameObject _gameOverPanel = Instantiate(gameOverPanelPrefab, canvas.transform, false);
            _gameOverPanel.transform.Find("PlayAgainButton").GetComponent<Button>().onClick.AddListener(PlayAgain);

            TextMeshProUGUI totalValueText = _gameOverPanel.transform.Find("Final Value").GetComponent<TextMeshProUGUI>();
            totalValueText.text = "$" + aquarium.totalValue.ToString("0.##");
        }

        //---------------- Destructive methods ---------------//

        public void DestroyHomeScreen()
        {
            aquarium.totalNumOfRounds = (int)Math.Round(slider.value);
            Destroy(GameObject.FindGameObjectWithTag("HomeScreen"));
        }

        public void DestroyNewFishUI() // Destroys all UI elements created in FindFishUI()
        {
            Destroy(GameObject.FindGameObjectWithTag("KeepFishButton"));
            Destroy(GameObject.FindGameObjectWithTag("DiscardFishButton"));
            Destroy(GameObject.FindGameObjectWithTag("FishStatsPanel"));
        }

        private void DestroyEatenUI() // Destroys all UI elements created in FishEatenUI()
        {
            Destroy(GameObject.FindGameObjectWithTag("EatenPanel"));
        }

        public void DestroyAquariumListUI() // Destroys all UI elements created in ListAquarium()
        {
            Destroy(GameObject.FindGameObjectWithTag("AquariumListPanel"));
        }

        //-------------- Other methods ---------------//

        public void ShowSliderValue() // Updates text above slider.
        {
            textSliderValue.text = slider.value.ToString();
        }

        public void UpdateAquariumValue() // Updates UI text for aquarium value.
        {
            totalValueText.text = "<b>Total Aquarium Value: </b>$" + aquarium.totalValue.ToString("0.##");
        }
        private void PlayAgain() // Restarts game.
        {
            SceneManager.LoadScene("FishingGame");
        }
    }
}