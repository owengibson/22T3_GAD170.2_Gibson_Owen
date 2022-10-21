using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OwenGibson
{
    public enum FishSpecies { Undefined, Barramundi, Cod, Bass, Flathead, Tuna, Salmon, Mackerel, Trout, Sardine, Snapper }
    public class Fish : MonoBehaviour
    {
        [SerializeField] private FishSpecies chosenFishSpecies;

        [Header("Fish Details")]
        public string species;
        public int length; // measured in cm
        public float price; // measured in $dollars.cents
        private float priceToLengthFactor; // per species factor loosely based on average length, weight, and price per kg
        public void SetFishStats(string newSpecies, int newLength, float newPrice)
        {
            species = newSpecies;
            length = newLength;
            price = newPrice;
        }
        public void Initialise()
        {
            chosenFishSpecies = (FishSpecies)Random.Range(1, 11);

            switch (chosenFishSpecies)
            {
                case FishSpecies.Undefined:
                    Debug.Log("Undefined. Choose enum");
                    break;

                case FishSpecies.Barramundi:
                    length = Random.Range(60, 121);
                    priceToLengthFactor = 0.08f;
                    SetFishStats("Barramundi", length, length * priceToLengthFactor);
                    break;

                case FishSpecies.Cod:
                    length = Random.Range(100, 141);
                    priceToLengthFactor = 1.8f;
                    SetFishStats("Atlantic Cod", length, length * priceToLengthFactor);
                    break;

                case FishSpecies.Bass:
                    length = Random.Range(15, 65);
                    priceToLengthFactor = 1.6f;
                    SetFishStats("Australian Bass", length, length * priceToLengthFactor);
                    break;

                case FishSpecies.Flathead:
                    length = Random.Range(35, 71);
                    priceToLengthFactor = 0.47f;
                    SetFishStats("Northern Flathead", length, length * priceToLengthFactor);
                    break;

                case FishSpecies.Tuna:
                    length = Random.Range(115, 246);
                    priceToLengthFactor = 364.58f;
                    SetFishStats("Bluefin Tuna", length, length * priceToLengthFactor);
                    break;

                case FishSpecies.Salmon:
                    length = Random.Range(70, 81);
                    priceToLengthFactor = 1.77f;
                    SetFishStats("Atlantic Salmon", length, length * priceToLengthFactor);
                    break;

                case FishSpecies.Mackerel:
                    length = Random.Range(20, 66);
                    priceToLengthFactor = 0.31f;
                    SetFishStats("Blue Mackerel", length, length * priceToLengthFactor);
                    break;

                case FishSpecies.Trout:
                    length = Random.Range(40, 66);
                    priceToLengthFactor = 0.57f;
                    SetFishStats("Rainbow Trout", length, length * priceToLengthFactor);
                    break;

                case FishSpecies.Sardine:
                    length = Random.Range(10, 21);
                    priceToLengthFactor = 0.05f;
                    SetFishStats("Australian Sardine", length, length * priceToLengthFactor);
                    break;

                case FishSpecies.Snapper:
                    length = Random.Range(35, 51);
                    priceToLengthFactor = 0.94f;
                    SetFishStats("Snapper", length, length * priceToLengthFactor);
                    break;

                default:
                    Debug.Log("Invalid choice");
                    break;
            }
        }

        private void Awake()
        {
            Initialise();
        }
    }
}
