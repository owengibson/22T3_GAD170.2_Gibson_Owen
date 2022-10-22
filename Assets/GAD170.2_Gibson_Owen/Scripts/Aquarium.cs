using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.Linq;
using Unity.VisualScripting;

namespace OwenGibson
{
    public class Aquarium : MonoBehaviour
    {
        public List<Fish> fishList = new List<Fish>();
        private Fish smallestFish;
        [HideInInspector] public float totalValue;

        [SerializeField] private GameObject fishPrefab;
        [SerializeField] private UIManager uiManager;
        [Range(5, 25)]
        public int totalNumOfRounds = 10;

        private int numOfRounds = 0;

        private GameObject newFishGO;
        [HideInInspector] public Fish newFish;
        [HideInInspector] public Fish fishEaten;
        private bool fishEatenInRound = false;


        //This method runs when the "Find Fish" button is pressed. Starts a new turn
        private void FindFishButton()
        {
            newFishGO = Instantiate(fishPrefab);
            newFish = newFishGO.GetComponent<Fish>();
            uiManager.FindFishUI();
        }

        //This method runs when the "Keep Fish" button is pressed
        public void KeepFishButton()
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
                    Destroy(smallestFish.GameObject());

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
            uiManager.UpdateAquariumValue();
            uiManager.DestroyNewFishUI();
            newFishGO.GetComponent<SpriteRenderer>().enabled = false;


            if (fishEatenInRound)
            {
                uiManager.FishEatenUI();
                fishEatenInRound = false;
            }

            numOfRounds++;
            if (numOfRounds == totalNumOfRounds) uiManager.GameOver();
        }

        public void DiscardFishButton()
        {
            uiManager.DestroyNewFishUI();
            newFishGO.GetComponent<SpriteRenderer>().enabled = false;

            numOfRounds++;
            if (numOfRounds == totalNumOfRounds) uiManager.GameOver();
        }

        public void ReleaseAllFish()
        {
            fishList.Clear();

            GameObject[] allFish = GameObject.FindGameObjectsWithTag("Fish");
            foreach (GameObject fish in allFish) Destroy(fish);

            uiManager.DestroyAquariumListUI();
            totalValue = 0;
            uiManager.UpdateAquariumValue();
        }
    }
}
