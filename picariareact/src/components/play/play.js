import React, { Component } from 'react';
import * as PIXI from 'pixi.js';
import myImage from '../../logo.svg';
import { GetGameboard, BuildDot, BuildViableMovesSigns } from './gameEngine/gameboard';
import { PicariaEngine } from './gameEngine/game';
export default class play extends Component {
	constructor(props) {
		super(props);
		this.pixiContainer = null;
		this.app = new PIXI.Application(800, 600, { backgroundColor: 0xffffff });
		this.engine = new PicariaEngine(this.endGame);
	}

	endGame = (message) => {
		console.log(message);
	}

	updatePixiContainer = (element) => {
		this.pixiContainer = element;

		if (this.pixiContainer && this.pixiContainer.children.length <= 0) {
			this.pixiContainer.appendChild(this.app.view);
			this.setup();
		}
	};

	setup = () => {
		PIXI.loader.add('avatar', myImage).load(this.initialize);
	};

	initialize = () => {
		var gameboard = GetGameboard();
		this.app.stage.addChild(gameboard);

		var positions = BuildViableMovesSigns(this.engine.Positions, this.engine.PawnClick);
		this.engine.setDots(positions);
		positions.forEach((element) => {
			this.app.stage.addChild(element);
		});
	};
	render() {
		return (
			<div className="d-flex justify-content-center">
				<div className="mt-5" ref={this.updatePixiContainer} />
			</div>
		);
	}
}
