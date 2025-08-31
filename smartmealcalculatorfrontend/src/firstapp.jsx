import React from 'react'

const App = () => {
  const name = "Jesper";
  const x = 10;
  const y = 20;
  const names = ["Jesper", "Johan", "Daniel", "John"];
  const loggedIn = false;
  const styles = {
    color: "red",
    fontSize: '50px'
  }


  return (
    <>
    <div className="First">App</div>
    <p style={styles} > Hello {name}</p>
    <p>The sum of {x} and {y} is {x+y} </p>
    <ul> 
      { names.map((name, index) => (
        <li key={index}>{name}</li>
      ))}
    </ul>
      { loggedIn ? <h1>Hello Member</h1> : <h1>Hello Guest</h1>}
      { loggedIn && <h1>Hello Member</h1>}
  </>
  )
}
export default App