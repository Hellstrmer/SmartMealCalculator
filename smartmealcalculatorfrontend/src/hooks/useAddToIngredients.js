import { useState, useEffect } from 'react';

export const useAddToIngredients = () => {
  const [NewIngredient, setNewIngredient] = useState([]);
  const [Total, setTotal] = useState({
    TotalAmount: 0,
    TotalKcal: 0,
    TotalPortion: 0,
    TotalKcalPerPortion: 0,
    TotalProtein: 0,
    TotalProteinPerPortion: 0,
    TotalCarbs: 0,
    TotalCarbsPerPortion: 0,
    TotalSugars: 0,
    TotalSugarsPerPortion: 0,
  });
  const UpdateUseCount = async (Ingredient) => {
    try {
      
      console.log('Skickar denna data:', Ingredient);
      console.log('JSON:', JSON.stringify(Ingredient));
      const res = await fetch(`http://localhost/api/Meal/UpdateUseCount`, {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify(Ingredient)
      });
      console.log(res);

      if (!res.ok) {
        throw new Error(`HTTP error! status: ${res.status}`);
      }
      const data = await res.json();
      return data;
    } catch (error) {
      console.log("Error! " + error);
    }
  }

  const AddToIngList = (e, formData, setFormData) => {
    if (e.key === 'Enter') {
      if (String(formData.name || '').trim() &&
        String(formData.kcal || '').trim() &&
        String(formData.protein || '').trim() &&
        String(formData.grams || '').trim() &&
        String(formData.portions || '').trim()) {
        UpdateUseCount(formData.name);
        setNewIngredient(prev => [...prev, formData]);

        setFormData({
          name: '',
          kcal: '',
          protein: '',
          carbs: '',
          sugars: '',
          grams: '',
          portions: ''
        });
      }
    }
  };

  useEffect(() => {
    if (NewIngredient.length === 0) return;
    console.log(NewIngredient);
    const newTotal = {
      TotalAmount: 0,
      TotalKcal: 0,
      TotalPortion: 0,
      TotalKcalPerPortion: 0,
      TotalProtein: 0,
      TotalProteinPerPortion: 0,
      TotalCarbs: 0,
      TotalCarbsPerPortion: 0,
      TotalSugars: 0,
      TotalSugarsPerPortion: 0,
    };    

    NewIngredient.map((Ingredients) => {
      const grams = parseInt(Ingredients.grams);
      const portions = parseInt(Ingredients.portions);
      const kcal = parseInt(Ingredients.kcal);
      const protein = parseInt(Ingredients.protein);
      newTotal.TotalAmount += grams;
      newTotal.TotalPortion = portions;
      newTotal.TotalKcal += (grams / 100) * kcal;
      newTotal.TotalProtein += (grams / 100) * protein;
    })
    newTotal.TotalKcalPerPortion = (newTotal.TotalKcal / newTotal.TotalPortion).toFixed(0);
    newTotal.TotalProteinPerPortion = (newTotal.TotalProtein / newTotal.TotalPortion).toFixed(0);
    setTotal(newTotal)
  }, [NewIngredient]);

  return { NewIngredient, AddToIngList, Total };
}


