import { useState } from 'react';

export const useFormData = () => {
    const [formData, setFormData] = useState({
        name: '',
        kcal: '',
        protein: '',
        grams: '',
        portions: ''
    });
    const updateField = (field, value) => {
        setFormData(prev => ({ ...prev, [field]: value }));
    };

    return { formData, updateField, setFormData};
}

