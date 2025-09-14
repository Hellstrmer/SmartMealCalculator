import React from 'react'
import { Route, 
  createBrowserRouter, 
  createRoutesFromElements,
  RouterProvider,
} from 'react-router-dom';
import './App.css'
import MainLayout from './layouts/MainLayout.jsx';
import HomePage from './pages/HomePage.jsx';
import SearchProducts from './pages/SearchProducts.jsx';
import NotFoundPage from './pages/NotFoundPage.jsx';


function App() {
  const router = createBrowserRouter(
    
    createRoutesFromElements(    
    <Route path='/' element={<MainLayout />}>
      <Route index element={ <HomePage />} />
      <Route path='/searchproduct' element={<SearchProducts/> } />
      <Route path='*' element={<NotFoundPage/> } /> 
    </Route>
    )
  );

  return <RouterProvider router={router} />;
}

export default App

