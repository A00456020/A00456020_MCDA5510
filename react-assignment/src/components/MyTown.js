import React from 'react';
import Temperature from './Temperature';
class MyTown extends React.Component
{
constructor(){
super();
this.state = {
degrees : null,
isCelsius : true,
}
}
componentDidMount(){
fetch('https://api.openweathermap.org/data/2.5/weather?q=Halifax&units=metric&appid=cb1d074f677a31135d69b228ddeeeb72')
.then((res)=> res.json())
.then(temperature =>this.setState({degrees: temperature["main"]["temp"]}))
}
changetoOther(item)
{
this.setState({
isCelsius : item,
})
}
render(){
return(
<div>
<img src= 'https://www.novascotia.com/sites/default/files/styles/xlarge/public/2019-05/aerial-HalifaxCitadel-1920x1080.jpg' alt = 'an image of Halifax, NS' width="900"height="450" />
<h1>I currently reside in Halifax city in Nova Scotia, Canada.</h1>
<p>Halifax is a prominent port of the Eastern canada and a major business and financial centre of the Atlantic Canada. Primarily Halifax is known for its rich Maritime history and for being one of the deepest port in the world.</p>
{this.state.degrees> 20 ? <img alt='an image of sunny weather'src = "https://raw.githubusercontent.com/simonachkar/react-mcda5510/main/assignment/assets/sunny.png"/> : this.state.degrees< 10 ? <img alt='Cold Weather image'src = "https://raw.githubusercontent.com/simonachkar/react-mcda5510/main/assignment/assets/cold.png"/> : <img alt='weather_mild'src = "https://raw.githubusercontent.com/simonachkar/react-mcda5510/main/assignment/assets/mild.png"/>}
<Temperature data = {{isCelsius : this.state.isCelsius,changetoOther:this.changetoOther.bind(this)}}degrees = {this.state.degrees}/>
</div>
)
}
}
export default MyTown;