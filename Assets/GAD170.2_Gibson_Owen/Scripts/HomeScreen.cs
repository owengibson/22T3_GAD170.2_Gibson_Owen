using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

namespace OwenGibson
{

    public class HomeScreen : MonoBehaviour
    {
        private Slider slider;
        private TextMeshProUGUI textSliderValue;
        [SerializeField] private Aquarium aquarium;
        void Start()
        {
            slider = transform.Find("NumberOfRounds").GetComponent<Slider>();
            textSliderValue = transform.Find("NumberOfRounds").transform.Find("Value").GetComponent<TextMeshProUGUI>();

            ShowSliderValue();
        }

        public void ShowSliderValue()
        {
            textSliderValue.text = slider.value.ToString();
        }

        public void DestroyHomeScreen()
        {
            aquarium.totalNumOfRounds = (int)Math.Round(slider.value);
            Destroy(gameObject);
        }
    }
}
