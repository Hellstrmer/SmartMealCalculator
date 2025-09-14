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
        <span>{Total.TotalPerPortion} Kcal/Portion</span>
        <FaCalculator />
    </div>
  )
}

export default TotalCalories