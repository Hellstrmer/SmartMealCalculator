import React from 'react'
import { useState, useEffect } from 'react';

import AddFiels from './AddFields'
import Ingredients from './ingredients'
import TotalCalories from './TotalCalories'
import SaveIngredients from './SaveIngredients'
import { useIngredients } from '../hooks/useIngredients'
import { useFormData } from '../hooks/useFormData';
import { useAddToIngredients } from '../hooks/useAddToIngredients';

const MealCard = () => {
  const { IngredientsList, loading, fetchIngrediens } = useIngredients();
  const { formData, updateField, setFormData } = useFormData();
  const { NewIngredient, AddToIngList, Total} = useAddToIngredients();
  
  useEffect(() => {
    if (formData.portions.length > 0) {
      console.log("name: " + formData.name + "grams: " + formData.grams);
    }
  }, [formData.name, formData.grams, formData.portions]);

  // useEffect(() => {
  //   if (NewIngredient.length > 0) {
  //     let TotalCount = {
  //       TotalAmount: 0,
  //       TotalKcalPortion: 0,
  //       TotalKcalPerPortion: 0,
  //       TotalProtein: 0,
  //       TotalProteinPerPortion: 0,
  //       TotalCarbs: 0,
  //       TotalCarbsPerPortion: 0,
  //       TotalSugars: 0,
  //       TotalSugarsPerPortion: 0,
  //     };
  //     NewIngredient.map((Ingredients) => {
  //       TotalCount.TotalAmount += parseInt(Ingredients.grams);
  //       TotalCount.TotalKcalPortion += parseInt(Ingredients.portions);
  //       TotalCount.TotalProtein += parseInt(Ingredients.protein);
  //     })
  //     TotalCount.TotalKcalPerPortion = (TotalCount.TotalAmount / TotalCount.TotalKcalPortion).toFixed();
  //     TotalCount.TotalProteinPerPortion = (TotalCount.TotalAmount / TotalCount.TotalProtein).toFixed();
  //     console.log(TotalCount);
  //     setTotal(TotalCount)
  //   }
  // }, [NewIngredient])

  return (
    <>
      <AddFiels IngredientsList={IngredientsList}
        formData={formData}
        updateField={updateField}
        loading={loading}
        fetchIngrediens={fetchIngrediens}
        AddToIngList={(e) => AddToIngList(e, formData, setFormData)}
      />
      <Ingredients
        NewIngredient={NewIngredient}
      />
      <TotalCalories
        Total={Total}
      />
    </>
  )
}

export default MealCard