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

  const AddToIngList = (e, formData, setFormData) => {
    if (e.key === 'Enter') {
      if (String(formData.name || '').trim() &&
        String(formData.kcal || '').trim() &&
        String(formData.protein || '').trim() &&
        String(formData.grams || '').trim() &&
        String(formData.portions || '').trim()) {
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