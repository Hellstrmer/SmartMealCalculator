import React from 'react'
import { NavLink } from 'react-router-dom'
import { FaExclamationTriangle  } from 'react-icons/fa';
const NotFoundPage = () => {
  return (
    <section >
        <FaExclamationTriangle />
        <h1>404 Not Found</h1>
        <p>This page does not exist</p>
        <NavLink to='/'>Go Back</NavLink>
    </section>
  )
}

export default NotFoundPage