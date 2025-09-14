import React from 'react'
import MealCard from '../Components/MealCard'
import IngredientsList from '../Components/IngredientsList'
import SaveIngredients from '../Components/SaveIngredients'

const HomePage = () => {
  return (
        <>
    <h1>Måltid</h1>
    <MealCard />
    {/* <IngredientsList /> */}
    <SaveIngredients />
    </>
  )
}

export default HomePage