import React from 'react'
import { useState, useEffect } from 'react';

const Ingredients = ({NewIngredient}) => {
    let ProductName;
    let EnergyKcal100g;
    let Amount;
    let Portions;
    let PerPortion;


 return (
        <div className="ingredient-rows">
            {NewIngredient.map((Ingredient, index) => (
                <div key={index} className="ingredient-row">
                    <span>{Ingredient.name}</span>
                    <span>{Ingredient.kcal} Kcal/100g</span>
                    <span>{Ingredient.grams} g</span>
                    <span>{Ingredient.portions} st</span>
                </div>
            ))}            
        </div>
    )
}

export default Ingredients