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