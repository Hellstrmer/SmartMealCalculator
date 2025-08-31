import React from 'react'

const AddFields = ({title="defaultTitle", subtitle}) => {
   let Product = "Sök Produkt...";
    let Kcal;
    let Grams;
    let Portions;
    
    return (
        <div className='AddIngredients'>
            <h1>{title} </h1>
            <h2>{subtitle} </h2>
            <input placeholder={Product} />
            <input placeholder={`${Kcal || ''} (/100g)`} />
            <input placeholder={`${Grams || ''} (g)`} />
            <input placeholder={`${Portions || ''} (st)`} />
        </div>
    )
}

export default AddFields