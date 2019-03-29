import React from 'react';
import PropTypes from 'prop-types';
import { withStyles } from '@material-ui/core/styles';
import Popper from '@material-ui/core/Popper';
import Typography from '@material-ui/core/Typography';
import Button from '@material-ui/core/Button';
import Paper from '@material-ui/core/Paper';
import { HubConnectionBuilder, LogLevel } from '@aspnet/signalr';
import './navbar.css';

export default class NotificationBell extends React.Component {
	state = {
		anchorEl: null,
		notifications: [],
		hubConnection: null,
		unreadNotificationsPresent: false
	};

	componentDidMount() {
		const hubConnection = new HubConnectionBuilder()
			.withUrl('http://localhost:5000/notification')
			.configureLogging(LogLevel.Information)
			.build();

		this.setState({ hubConnection }, () => {
			this.state.hubConnection.start();

			this.state.hubConnection.on('newNotification', (notification) => {
				const notifications = this.state.notifications.concat([ notification ]);
				this.setState({
					notifications: notifications,
					unreadNotificationsPresent: true
				});
			});
		});
	}

	handleClick = (event) => {
		if (this.state.notifications.length > 0) {
			const { currentTarget } = event;
			this.setState((state) => ({
				anchorEl: state.anchorEl ? null : currentTarget,
				unreadNotificationsPresent: false
			}));
		}
	};

	render() {
		const { classes } = this.props;
		const { anchorEl } = this.state;
		const open = Boolean(anchorEl);
		const id = open ? 'no-transition-popper' : null;

		return (
			<div>
				<div onClick={this.handleClick}>
					{this.state.unreadNotificationsPresent ? (
						<div>
							<i className="far fa-bell fa-lg" />
							<div className="dot" />
						</div>
					) : (
						<div className="noNotificationBell">
							<i className="far fa-bell fa-lg" />
						</div>
					)}
				</div>

				<Popper id={id} open={open} anchorEl={anchorEl} className="mt-2 notificationContainer">
					<Paper>
						<span className="font-weight-bold notificationFont m-1">Notifications</span>
						<div>
							<hr className="notificationHr" />
						</div>
						{this.state.notifications.map((el, index) => (
							<div key={index}>
								<div className="d-flex flex-column m-1 notificationFont">
									<span className="font-weight-bold notificationFont">{el.title}</span>
									<span>{el.content}</span>
									<div>
										<hr className="notificationHr" />
									</div>
								</div>
							</div>
						))}
					</Paper>
				</Popper>
			</div>
		);
	}
}
