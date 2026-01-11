import React, { useState } from 'react'
import AddFields from '../Components/AddFields'
// import IngredientsList from '../Components/IngredientsList'
import { useIngredients } from '../hooks/useIngredients'
import { useFormData } from '../hooks/useFormData';
import { useAddToIngredients } from '../hooks/useAddToIngredients';
import { useAllIngredients } from '../hooks/useAllIngredients';


const SearchProducts = () => {
  const { IngredientsList, loading, fetchIngrediens } = useIngredients();
  const { AllIngredientsList, fetchAllIngrediens } = useAllIngredients();
  const { formData, updateField, setFormData } = useFormData();
  const { AddToIngList, UpdateTotal} = useAddToIngredients();

  return (
    <> 
    <h1>Sök Varor</h1>
    <AddFields 
        IngredientsList={IngredientsList}
        formData={formData}
        updateField={updateField}
        loading={loading}
        fetchIngrediens={fetchIngrediens}
        AddToIngList={AddToIngList}
      />
    {/* <AllIngredientsList /> */}
    </>
  )
}

export default SearchProducts