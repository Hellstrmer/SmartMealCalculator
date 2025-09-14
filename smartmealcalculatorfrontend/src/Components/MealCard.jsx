import React from 'react'
import { useState, useEffect } from 'react';

import AddFiels from './AddFields'
import Ingredients from './ingredients'
import TotalCalories from './TotalCalories'
import SaveIngredients from './SaveIngredients'

const MealCard = () => {
  const [loading, setLoading] = useState(true);
  const [IngredientsList, setIngredient] = useState([]);
  const [NewIngredient, setNewIngredient] = useState([]);
  const [Total, setTotal] = useState({
    TotalAmount: 0,
    TotalPortion: 0,
    TotalPerPortion: 0
  });
  const [formData, setFormData] = useState({
    name: "",
    kcal: "",
    grams: "",
    portions: ""
  });

  const updateField = (field, value) => {
    setFormData(prev => ({ ...prev, [field]: value }));
  }

  const fetchIngrediens = async (Name) => {
    if (!Name) {
      setIngredient([]);
      return;
    }
    setLoading(true);
    try {
      const res = await fetch(`http://localhost/api/Meal/GetIngredients?name=${encodeURIComponent(Name)}`);
      const data = await res.json();

      if (Array.isArray(data)) {
        setIngredient(data);
      } else {
        console.log("No results!");
        setIngredient([]);
      }

    } catch (error) {
      console.log("Error!: " + error);
      setIngredient([]);
    } finally {
      setLoading(false);
    }
  }

  useEffect(() => {
    if (formData.portions.length > 0) {
      console.log("name: " + formData.name + "grams: " + formData.grams);
    }

  }, [formData.name, formData.grams, formData.portions]);

  const AddToIngList = (e) => {
    if (e.key === 'Enter') {
      if (String(formData.name || '').trim() &&
        String(formData.kcal || '').trim() &&
        String(formData.grams || '').trim() &&
        String(formData.portions || '').trim()) {
        setNewIngredient(prev => [...prev, formData]);

        setFormData({
          name: "",
          kcal: "",
          grams: "",
          portions: ""
        });
        UpdateTotal();

      }
    }
  }


  const UpdateTotal = () => {

    NewIngredient.map((Ingredients, index) => {
      console.log(typeof (Ingredients.grams));
      // TotalCount.TotalAmount += Ingredients.grams;
      // TotalCount.TotalPortion = Ingredients.portions;

    })
    //TotalCount.TotalPerPortion = TotalCount.TotalAmount / TotalCount.TotalPortion;
    //setTotal(TotalCount)
  }

  useEffect(() => {
    if (NewIngredient.length > 0) {
      let TotalCount = {
        TotalAmount: 0,
        TotalPortion: 0,
        TotalPerPortion: 0
      };

      NewIngredient.map((Ingredients) => {
        TotalCount.TotalAmount += parseInt(Ingredients.grams);
        TotalCount.TotalPortion = parseInt(Ingredients.portions);
      })
      TotalCount.TotalPerPortion = TotalCount.TotalAmount / TotalCount.TotalPortion;
      setTotal(TotalCount)

    }
  }, [NewIngredient])

  return (
    <>
      <AddFiels IngredientsList={IngredientsList}
        formData={formData}
        updateField={updateField}
        loading={loading}
        fetchIngrediens={fetchIngrediens}
        AddToIngList={AddToIngList}
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