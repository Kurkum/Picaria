import React, { Component } from 'react';
import './App.css';
import Home from './components/Home/Home';
import Play from './components/play/play';
import Navbar from './components/navbar/navbar';
import { BrowserRouter as Router, Route, Switch, Redirect } from 'react-router-dom';

class App extends Component {
	render() {
		return (
			<Router>
				<div>
					<Navbar />
					<div className="contentGridContainer">
						<div className="content">
							<Switch>
								<Route path="/home" render={() => <Home />} />
								<Route path="/play" render={() => <Play />} />
								<Route exact path="/" render={() => <Redirect to="/home" />} />
							</Switch>
						</div>
					</div>
				</div>
			</Router>
		);
	}
}

export default App;
