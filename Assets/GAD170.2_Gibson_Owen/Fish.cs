using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OwenGibson
{
    public class Fish : MonoBehaviour
    {
        [Header("Fish Details")]
        [SerializeField] private string species;
        [SerializeField] private float length;
        [SerializeField] private float price;
        public void SetInitialState(string newSpecies, float newLength, float newPrice)
        {
            species = newSpecies;
            length = newLength;
            price = newPrice;
        }
    }
}
