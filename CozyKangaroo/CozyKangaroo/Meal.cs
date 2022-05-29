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

        // Getters
        public String getName()
        {
            return name;
        }
        public String getDescription()
        {
            return description;
        }
        public double getPrice()
        {
            return price;
        }
        public bool getOnOffer()
        {
            return onOffer;
        }
        public List<String> getIngredients()
        {
            return ingredients;
        }
        public List<String> getAllergens()
        {
            return allergens;
        }
        public String getImageURI()
        {
            return imageURI;
        }

        // Setters
        public void setName(String aName)
        {
            name = aName;
        }
        public void setDescription(String aDescription)
        {
            description = aDescription;
        }
        public void setPrice(double aPrice)
        {
            price = aPrice;
        }
        public void setOnOffer(bool aOnOffer)
        {
            onOffer = aOnOffer;
        }
        public void setIngredients(List<String> aIngredients)
        {
            ingredients = aIngredients;
        }
        public void setAllergens(List<String> aAllergens)
        {
            allergens = aAllergens;
        }
        public void getImageURI(String aImageURI)
        {
            imageURI = aImageURI;
        }
    }
}
