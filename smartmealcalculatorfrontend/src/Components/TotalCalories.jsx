import React from 'react'

const TotalCalories = () => {
    let TotalAmount;
    let TotalPortion;
    let TotalPerPortion;
  return (
    <div className='AddIngredients'>        
    <h4 >Totalt:</h4>
        <span>{TotalAmount} g</span>
        <span>{TotalPortion} st</span>
        <span>{TotalPerPortion} Kcal/Portion</span>
    </div>
  )
}

export default TotalCalories