using System;
using System.Collections.Generic;
using System.Text;

namespace CozyKangaroo
{
    class Meal
    {
        private String name;
        private String description;
        private double price;
        private bool onOffer;
        private List<String> ingredients;
        private List<String> allergens;
        private bool available;
        private String imageURI;

        // Constructors
        // meal1 = new Meal("name", "description", 1.23, true, new List<String> {"ingredient1", "ingredient2", "ingredient3"}, new List<String> {"allergen1", "allergen2"}, true, "https://imagelocation.com/image.png");
        public Meal(String aName, String aDescription, double aPrice, bool aOnOffer, List<String> aIngredients, List<String> aAllergens, bool aAvailable, String aImageURI)
        {
            name = aName;
            description = aDescription;
            price = aPrice;
            onOffer = aOnOffer;
            ingredients = aIngredients;
            allergens = aAllergens;
            available = aAvailable;
            imageURI = aImageURI;
        }

        // Getters and Setters
        public String Name
        {
            get => name;
            set => name = value;
        }
        public String Description
        {
            get => description;
            set => description = value;
        }
        public double Price
        {
            get => price;
            set => price = value;
        }
        public bool OnOffer
        {
            get => onOffer;
            set => onOffer = value;
        }
        public List<String> Ingredients
        {
            get => ingredients;
            set => ingredients = value;
        }
        public List<String> Allergens
        {
            get => allergens;
            set => allergens = value;
        }
        public String ImageURI
        {
            get => imageURI;
            set => imageURI = value;
        }
    }
}
