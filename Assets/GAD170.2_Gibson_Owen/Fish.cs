using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OwenGibson
{
    public enum FishSpecies { Undefined, Barramundi, Cod, Bass, Flathead, Tuna, Carp, Grayling, Piranha, Clownfish, Parrotfish }
    public class Fish : MonoBehaviour
    {
        [SerializeField] private FishSpecies chosenFishSpecies;

        [Header("Fish Details")]
        [SerializeField] private string species;
        [SerializeField] private int length; // measured in cm
        [SerializeField] private float price; // measured in $dollars.cents
        private float priceToLengthFactor; // per species factor loosely based on average length, weight, and price per kg
        public void SetFishStats(string newSpecies, int newLength, float newPrice)
        {
            species = newSpecies;
            length = newLength;
            price = newPrice;
        }
        public void Initialise()
        {
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
                    //437.5kg, 150perkg, 180cm
                    break;

                case FishSpecies.Carp:
                    break;

                case FishSpecies.Grayling:
                    break;

                case FishSpecies.Piranha:
                    break;

                case FishSpecies.Clownfish:
                    break;

                case FishSpecies.Parrotfish:
                    break;

                default:
                    Debug.Log("Invalid choice");
                    break;
            }
        }

        private void Start()
        {
            Initialise();
        }
    }
}
