using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.Linq;

namespace OwenGibson
{
    public class Aquarium : MonoBehaviour
    {
        public List<Fish> fishList = new List<Fish>();
        private Fish smallestFish;
        [HideInInspector] public float totalValue;

        [Header("Prefab References")]
        [SerializeField] private GameObject fishPrefab;
        [SerializeField] private GameObject keepFishPrefab;
        [SerializeField] private GameObject discardFishPrefab;
        [SerializeField] private GameObject fishStatsPrefab;
        [SerializeField] private GameObject aquariumListPrefab;
        [SerializeField] private GameObject fishEatenPrefab;

        [Space(10)]
        [Header("Other GameObject References")]
        [SerializeField] private GameObject canvas;
        [SerializeField] private GameObject totalValueText;

        [Space(10)]
        [Header("Miscellaneous Variables")]
        [Range(5, 25)]
        public int totalNumOfRounds = 10;

        private int numOfRounds = 0;
        private GameOver gameOverScript;

        private GameObject newFishGO;
        private Fish newFish;
        private Fish fishEaten;
        private bool fishEatenInRound = false;

        private void Start()
        {
            gameOverScript = GetComponent<GameOver>();
        }

        //This method runs when the "Find Fish" button is pressed.
        private void FindFishButton()
        {
            newFishGO = Instantiate(fishPrefab);
            newFish = newFishGO.GetComponent<Fish>();

            GameObject _keepFish = Instantiate(keepFishPrefab, canvas.transform, false);
            _keepFish.GetComponent<Button>().onClick.AddListener(KeepFishButton);

            GameObject _discardFish = Instantiate(discardFishPrefab, canvas.transform, false);
            _discardFish.GetComponent<Button>().onClick.AddListener(DiscardFishButton);

            GameObject _fishStatsPanel = Instantiate(fishStatsPrefab, canvas.transform, false);
            _fishStatsPanel.GetComponentInChildren<TextMeshProUGUI>().text = "<b>Species: </b>" + newFish.species +
                                                                             "<br><b>Length: </b>" + newFish.length +
                                                                             "cm<br><b>Price: </b>$" + newFish.price.ToString("0.##");
        }

        //This method runs when the "Keep Fish" button is pressed
        private void KeepFishButton()
        {
            if (fishList.Any()) //checking if aquarium isn't empty
            {
                Debug.Log("Aquarium isn't empty.");
                if (newFish.length >= smallestFish.length * 2) // new fish 2x bigger than smallest fish in aquarium
                {
                    fishList.Remove(smallestFish);
                    totalValue -= smallestFish.price;

                    fishEatenInRound = true;
                    fishEaten = smallestFish;

                    Debug.Log("New fish was more than twice the length of your smallest fish. Existing fish got eaten.");

                }
                fishList.Add(newFish);
                totalValue += newFish.price;

                smallestFish = null;
                foreach (Fish fish in fishList) // find new smallest fish
                {
                    if (smallestFish == null || fish.length < smallestFish.length) smallestFish = fish;
                }
            }
            else // first fish in aquarium
            {
                fishList.Add(newFish);
                totalValue = newFish.price;
                smallestFish = newFish;
            }
            totalValueText.GetComponent<TextMeshProUGUI>().text = "<b>Total Aquarium Value: </b>$" + totalValue.ToString("0.##");
            DestroyNewFishElements();

            if(fishEatenInRound)
            {
                GameObject _eatenPanel = Instantiate(fishEatenPrefab, canvas.transform, false);
                _eatenPanel.transform.Find("EatenOKButton").GetComponent<Button>().onClick.AddListener(DestroyEatenPanel);

                _eatenPanel.transform.Find("Text").GetComponent<TextMeshProUGUI>().text = "Your new fish ate your " + fishEaten.length + "cm long " + fishEaten.species + "!";

                fishEatenInRound = false;
            }

            numOfRounds++;
            if (numOfRounds == totalNumOfRounds) gameOverScript.GameOverElements();
        }

        private void DiscardFishButton()
        {
            DestroyNewFishElements();
            numOfRounds++;
            if (numOfRounds == totalNumOfRounds) gameOverScript.GameOverElements();
        }

        private void ListAquarium()
        {
            GameObject _aquariumListPanel = Instantiate(aquariumListPrefab, canvas.transform, false);
            TextMeshProUGUI _text = _aquariumListPanel.GetComponentInChildren<TextMeshProUGUI>();
            _text.text = "";
            _aquariumListPanel.transform.Find("CloseAquariumButton").GetComponent<Button>().onClick.AddListener(DestroyAquariumListPanel);
            _aquariumListPanel.transform.Find("ReleaseFishButton").GetComponent<Button>().onClick.AddListener(ReleaseAllFish);

            if(fishList.Any())
            {
                foreach (Fish fish in fishList)
                {
                    _text.text += fish.species + ", " + fish.length + "cm, $" + fish.price + "<br>";
                }
            }
            else
            {
                _text.text = "No fish in aquarium";
            }
        }
        private void ReleaseAllFish()
        {
            fishList.Clear();
            DestroyAquariumListPanel();
            totalValue = 0;
            totalValueText.GetComponent<TextMeshProUGUI>().text = "<b>Total Aquarium Value: </b>$" + totalValue;
        }

        private void DestroyNewFishElements()
        {
            newFishGO.GetComponent<SpriteRenderer>().enabled = false;
            Destroy(GameObject.FindGameObjectWithTag("KeepFishButton"));
            Destroy(GameObject.FindGameObjectWithTag("DiscardFishButton"));
            Destroy(GameObject.FindGameObjectWithTag("FishStatsPanel"));
        }

        private void DestroyAquariumListPanel()
        {
            Destroy(GameObject.FindGameObjectWithTag("AquariumListPanel"));
        }

        private void DestroyEatenPanel()
        {
            Destroy(GameObject.FindGameObjectWithTag("EatenPanel"));
        }
    }
}
