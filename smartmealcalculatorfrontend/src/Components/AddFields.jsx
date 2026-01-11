import React from 'react'
import { Typeahead } from 'react-bootstrap-typeahead';
import { useState, useEffect } from 'react';

const AddFields = ({ IngredientsList, formData, updateField, loading, fetchIngrediens, AddToIngList}) => {
    const [selected, setSelected] = useState([]);

    useEffect(() => {
        //console.log("Ing: ", IngredientsList);
    }, [IngredientsList]);

    const handleInputChange = (Name) => {
        fetchIngrediens(Name);
    }

    const options = IngredientsList
    ?.sort((a, b) => (b.useCount || 0) - (a.useCount || 0))
    .map((Ingredient, index) => ({
        id: index,
        label: Ingredient.productName + ", " + Ingredient.brands
    })) || [];


    const handleSelectionChange = (selectedOptions) => {
        setSelected(selectedOptions);
        if (selectedOptions.length > 0) {
            const selectedIngredient = IngredientsList[selectedOptions[0].id];
            const KcalValue = selectedIngredient.energyKcal100g;
            const proteinValue = selectedIngredient.proteins100g;
            //console.log(selectedIngredient);
            updateField('name', selectedIngredient.productName);
            updateField('kcal', KcalValue);
            updateField('protein', proteinValue);
            //console.log('Kcal: ' + KcalValue);
        } else {
            updateField('kcal', '');
        }
    }

    return (
        <div className='AddIngredients'>
            <Typeahead className='Typeahead'
                allowNew
                id="custom-selections-example"
                clearButton={false}
                newSelectionPrefix="Lägg till: "
                options={options}
                selected={selected}
                onChange={handleSelectionChange}
                onInputChange={handleInputChange}
                placeholder="Produkt..."
                labelKey="label"
                loading={loading}
                minLength={2}
                multiple={false}
            />
            <input 
                id='kcal'
                placeholder="Lägg till kalorier (/100g)"
                value={formData.kcal}
               onChange={(e) => updateField('kcal', e.target.value)}
                onKeyDown={AddToIngList}
            />
            {/* Lägg till flera rader med carbs osv när du listat ut hur det ska se ut */}
            <input 
                id='protein'
                placeholder="Lägg till protein (/100g)"
                value={formData.protein}
               onChange={(e) => updateField('protein', e.target.value)}
                onKeyDown={AddToIngList}
            />
            <input 
                id='grams'
                placeholder="Lägg till mängd (g)"
                value={formData.grams}
                onChange={(e) => updateField('grams', e.target.value)}
                onKeyDown={AddToIngList}
            />
            <input 
                id='portions'
                placeholder="Antal portioner (st)"
                value={formData.portions}
                onChange={(e) => updateField('portions', e.target.value)}
                onKeyDown={AddToIngList}
            />
        </div>
    )
}


export default AddFields