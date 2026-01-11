import { useState } from 'react';

export const useIngredients = () => {
    const [loading, setLoading] = useState(false);
    const [IngredientsList, setIngredient] = useState([]);

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
    };
    return { IngredientsList, loading, fetchIngrediens };
}