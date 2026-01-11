import React from 'react'
import { FaCalculator } from 'react-icons/fa'

const TotalCalories = ( {Total}) => {
    let TotalAmount;
    let TotalPortion;
    let TotalPerPortion;
  return (
    <div className='total-calories'> 
    <h4 >Totalt:</h4>
        <span>{String(Total.TotalAmount)} g</span>
        <span>{Total.TotalPortion} st</span>
        <span>{Total.TotalKcalPerPortion} Kcal/Portion</span>
        <span>{Total.TotalProteinPerPortion} g Protein/Portion</span>
        <FaCalculator />
    </div>
  )
}

export default TotalCalories