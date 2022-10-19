using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OwenGibson
{
    public class FishSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject fishPrefab;
        private GameObject newFish;
        private GameObject aquariumScriptGO;
        private Aquarium aquariumScript;
        private Fish newFishClass;

        private void Start()
        {
            aquariumScriptGO = GameObject.FindGameObjectWithTag("Aquarium");
            aquariumScript = aquariumScriptGO.GetComponent<Aquarium>();
            
        }

        public void FindFishButton()
        {
            newFish = Instantiate(fishPrefab);
            newFishClass = newFish.GetComponent<Fish>();

            aquariumScript.fishList.Add(newFishClass);
            Debug.Log("Added " + newFishClass.species + " to the aquarium.");
        }

        public void ListAquariumButton()
        {
            foreach(Fish fish in aquariumScript.fishList)
            {
                Debug.Log(fish.species + ", " + fish.length + "cm, $" + fish.price);
            }
        }
    }
}
