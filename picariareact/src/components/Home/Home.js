import React, { Component } from 'react';

class Home extends Component {
	render() {
		return (
			<div>
				<div className="d-flex mt-3">
					<div className="card w-50 mr-5">
						<div className="card-header">
							<span>Project Description</span>
						</div>

						<div className="card-body">
							<div>
								<p>
									Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor
									incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud
									exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute
									irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla
									pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia
									deserunt mollit anim id est laborum.
								</p>
							</div>
						</div>
					</div>
				</div>
			</div>
		);
	}
}

export default Home;
