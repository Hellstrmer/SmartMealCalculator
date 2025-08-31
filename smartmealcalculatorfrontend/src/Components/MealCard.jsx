import React from 'react'
import AddFiels from './AddFields'
import Ingredients from './ingredients'
import TotalCalories from './TotalCalories.Jsx'
import SaveIngredients from './SaveIngredients'

const MealCard = () => {
  return (
     <>
    <AddFiels subtitle="SubTitle" />
    <Ingredients />
    <TotalCalories/>
    <SaveIngredients />
    </>
  )
}

export default MealCard