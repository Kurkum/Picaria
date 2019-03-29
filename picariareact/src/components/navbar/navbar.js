import React, { Component } from 'react';
import './navbar.css';
import { NavLink } from 'react-router-dom';
import NotificationBell from './notificationBell';

export default class Navbar extends Component {
	render() {
		return (
			<div className="headerGrid">
				<div className="headerOne d-flex flex-column justify-content-center">
					<div className="sidebar d-flex justify-content-between align-items-baseline ">
						<div className="brand">
							<h6>Picaria.NET</h6>
						</div>
						<div className="d-flex align-items-baseline">
							<a
								role="button"
								href="https://bitbucket.org/OskarCukrowicz/spidey.net/src/master/"
								className="btn btn-outline-primary btn-sm mr-3"
							>
								Source code
							</a>
						</div>
					</div>
				</div>

				<hr className="headerTwo mt-0" />

				<div className="d-flex justify-content-between align-items-center headerThree">
					<div className="d-flex">
						<div className="mr-4 text-muted">
							<NavLink to="/play" className="navbarLink" activeClassName="selected">
								<i className="fas fa-home pr-1" />
								Play
							</NavLink>
						</div>
						<div className="mr-4 text-muted">
							<NavLink to="/home" className="navbarLink" activeClassName="selected">
								<i className="fas fa-home pr-1" />
								Home
							</NavLink>
						</div>
					</div>
					<div />
					<div />
					<div />
				</div>

				<hr className="headerFour mb-0" />
			</div>
		);
	}
}
