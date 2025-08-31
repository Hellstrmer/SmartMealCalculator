import React from 'react'

const Ingredients = () => {
    let ProductName;
    let EnergyKcal100g;
    let Amount;
    let Portions;
    let PerPortion;
    
    return (
       <div className="ingredient-row">               
    <span>{ProductName}</span>               
    <span>{EnergyKcal100g} Kcal/100g</span>                
    <span>{Amount} g)</span>               
    <span>{Portions} st)</span>               
    <span>{PerPortion} Kcal/Portion)</span>            
    </div> 
    )
}

export default Ingredients