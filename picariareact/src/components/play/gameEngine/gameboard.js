import * as PIXI from 'pixi.js';
const lineWidht = 2;
const lineColorBlack = 0x000000;

var gameStates = {
	PawnSetup: 'pawnsetup',
	PawnMove: 'pawnmove'
};

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

export function BuildViableMovesSigns(positions, onclickHandler) {
	var signs = [];
	var circleRadius = 8;
	positions.forEach((position) => {
		var sign = new PIXI.Graphics();

		sign.interactive = true;
		sign['pos'] = position;
		sign.buttonMode = true;
		sign.click = (e) => {
			onclickHandler(e);
		};
		sign.hitArea = new PIXI.Circle(position.x, position.y, circleRadius);
		signs.push(sign);
	});

	return signs;
}
