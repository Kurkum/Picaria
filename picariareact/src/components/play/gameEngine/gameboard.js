import * as PIXI from 'pixi.js';
const lineWidht = 2;
const lineColorBlack = 0x000000;
export function GetGameboard() {
	var board = new PIXI.Graphics();
	board.lineStyle(lineWidht, lineColorBlack);

	board.moveTo(50, 50);
	board.lineTo(50, 550);
	board.lineTo(750, 550);
	board.lineTo(750, 50);
	board.lineTo(50, 50);

	board.moveTo(50, 50);
	board.lineTo(750, 550);

	board.moveTo(50, 550);
	board.lineTo(750, 50);

	board.moveTo(400, 50);
	board.lineTo(400, 550);

	board.moveTo(50, 300);
	board.lineTo(750, 300);

	board.moveTo(400, 50);
	board.lineTo(50, 300);

	board.moveTo(400, 50);
	board.lineTo(50, 300);

	board.moveTo(50, 300);
	board.lineTo(400, 550);

	board.moveTo(50, 300);
	board.lineTo(400, 550);

	board.moveTo(750, 300);
	board.lineTo(400, 550);

	board.moveTo(750, 300);
	board.lineTo(400, 50);

	board.beginFill(lineColorBlack, 1);

	//board.drawCircle(50, 50, circleRadius);
	//board.drawCircle(50, 300, circleRadius);
	//board.drawCircle(50, 550, circleRadius);

	//board.drawCircle(400, 50, circleRadius);
	//board.drawCircle(400, 300, circleRadius);
	//board.drawCircle(400, 550, circleRadius);

	//board.drawCircle(750, 50, circleRadius);
	//board.drawCircle(750, 300, circleRadius);
	//board.drawCircle(750, 550, circleRadius);

	board.endFill();

	return board;
}

export function GetDots() {
	var circleRadius = 8;
	var dotOne = new PIXI.Graphics();

	dotOne.beginFill(lineColorBlack, 1);
	dotOne.drawCircle(50, 50, circleRadius);
	dotOne.endFill();
	dotOne.interactive = true;
	dotOne.buttonMode = true;
	dotOne.click = function(e) {
		var lol = 'Xd';
		console.log(lineWidht);
	};
	dotOne.hitArea = new PIXI.Circle(50, 50, circleRadius);

	return dotOne;
}
