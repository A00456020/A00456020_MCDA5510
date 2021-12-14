import React, {useState}from 'react';
import "./App.css";
import AboutMe from './components/AboutMe';
import MyTown from './components/MyTown';

const App = ()=> {
const [state, setState]= useState(true)
return (
<div>
<>
<button onClick={() => setState(true)}> About Me </button>
<button onClick={() => setState(false)}> My Town </button>
</>
<>
{state === true ? <AboutMe /> : <MyTown/> }
</>
</div>
)
}
export default App;