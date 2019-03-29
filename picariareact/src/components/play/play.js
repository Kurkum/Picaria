import React, { Component } from 'react';
import * as PIXI from 'pixi.js';
import myImage from '../../logo.svg';
import { GetGameboard, GetDots } from './gameEngine/gameboard';

export default class play extends Component {
	constructor(props) {
		super(props);
		this.pixiContainer = null;
		this.app = new PIXI.Application(800, 600, { backgroundColor: 0xffffff });
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
		var dot = GetDots();

		this.app.stage.addChild(gameboard);

		this.app.stage.addChild(dot);
	};
	render() {
		return <div className="mt-5" ref={this.updatePixiContainer} />;
	}
}
