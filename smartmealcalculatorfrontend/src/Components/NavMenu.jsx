import React from 'react'
import { NavLink } from 'react-router-dom'
import '../App.css'
import '../NavMenu.css'

const NavMenu = () => {
    const LinkClass = ({ isActive }) => 
        isActive 
    ? 'ActiveNav' :
     'nav-link';

    return (
        <div className="navbar-container">
            

            <div className="nav-scrollable">
                <nav className="flex-column">
                    <div className="nav-item px-3">
                        <NavLink className={ LinkClass } to="/">
                            <span className="bi bi-plus-square-fill-nav-menu" aria-hidden="true"></span> Måltid
                        </NavLink>
                    </div>
                    <div className="nav-item px-3">
                        <NavLink className={ LinkClass }  to="/searchproduct">
                            <span className="bi bi-list-nested-nav-menu" aria-hidden="true"></span> Sök varor
                        </NavLink>
                    </div>
                    <div className="nav-item px-3">
                        <NavLink className={ LinkClass }  to="/login">
                            <span className="bi bi-list-nested-nav-menu" aria-hidden="true"></span> Logga in
                        </NavLink>
                    </div>
                </nav>
            </div>
        </div>
    )
}

export default NavMenu