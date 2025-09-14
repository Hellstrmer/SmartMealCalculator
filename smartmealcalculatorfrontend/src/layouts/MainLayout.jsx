import React from 'react'
import { Outlet } from 'react-router-dom'
import Header from '../Components/Header';
import NavMenu from '../Components/NavMenu';


const MainLayout = () => {
  return (
    <div className="app-container">
        <Header/>
        <div className="main-layout">
            <NavMenu />
            <main className="content">
                <Outlet />
            </main>
        </div>
    </div>
  );
};

export default MainLayout