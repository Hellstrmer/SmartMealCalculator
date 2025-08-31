import React from 'react'
import { useState } from 'react'

const IngredientsList = () => {
    let Ingredients = [
        {
            "ProductName": "Pasta",
            "Brand": "Ica",
            "EnergyKcal100g": "350",
            "Proteins": 100,
            "Carbs": 50,
            "Fat": 20,
        },
        {
            "ProductName": "Ägg",
            "Brand": "Ica",
            "EnergyKcal100g": "350",
            "Proteins": 100,
            "Carbs": 50,
            "Fat": 20,
        }
    ];
    console.log(Ingredients);
    const [showAll, setShowAll] = useState(false);

    let showIng = [];

    if (!showAll) {
        showIng = Ingredients.slice(0, 1);
    }
    else {
        showIng = Ingredients;
    }

    return (
        <div className="ingredient-row">
            {showIng.map((Ingredient, index) => (
                <div key={index} className="ingredient-row">
                    <span>{Ingredient.ProductName}</span>
                    <span>{Ingredient.Brand}</span>
                    <span>{Ingredient.EnergyKcal100g} Kcal/100g</span>
                    <span>{Ingredient.Proteins}g protein/100g</span>
                    <span>{Ingredient.Carbs}g carbs/100g</span>
                    <span>{Ingredient.Fat}g fat/100g</span>
                </div>
            ))}
            <button onClick={() => setShowAll((prevState) => !prevState)} >{ showAll ? 'Less' : 'More'} </button>
        </div>
    )
}

export default IngredientsList