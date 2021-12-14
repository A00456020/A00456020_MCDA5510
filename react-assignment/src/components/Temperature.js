import React from 'react';
import "../App.css";
const Teamperature = (props)=> {
return(
props.data.isCelsius=== true ?
<div>
<p> Current Temperature: {props.degrees} degrees celsius</p>
<button onClick={() => props.data.changetoOther(false)}>Change to Degrees Fahrenheit</button>
</div> :
<div>
<p> Current Temperature: {(32+ (props.degrees* (9/5))).toFixed(2)} degrees fahrenheit</p>
<button onClick={() => props.data.changetoOther(true)}>Change to Degrees Celsius</button>
</div>
)
}
export default Teamperature;