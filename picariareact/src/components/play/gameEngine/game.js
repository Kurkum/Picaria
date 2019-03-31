import { Position, PositionStatus } from './position';
import axios from 'axios';

const colors = {
	PlayerOne: 0x00ff00,
	PlayerTwo: 0xff0000,
	FreeToCapture: 0x0000ff
};

const gameStates = {
	PawnSetup: 'pawnsetup',
	PawnMovement: 'pawnmovement'
};

const players = {
	PlayerOne: 'playerone',
	PlayerTwo: 'playertwo'
};

const moveStates = {
	PickingPawn: 'pickingpawn',
	MovingPanw: 'movingpawn'
};

export class PicariaEngine {
	constructor() {
		this.GameState = gameStates.PawnSetup;
		this.CurrentMove = players.PlayerOne;
		this.MoveStatus = moveStates.PickingPawn;
	}

	Positions = [
		new Position(50, 50, PositionStatus.FreeToCapture),
		new Position(400, 50, PositionStatus.FreeToCapture),
		new Position(750, 50, PositionStatus.FreeToCapture),
		new Position(50, 300, PositionStatus.FreeToCapture),
		new Position(400, 300, PositionStatus.FreeToCapture),
		new Position(750, 300, PositionStatus.FreeToCapture),
		new Position(50, 550, PositionStatus.FreeToCapture),
		new Position(400, 550, PositionStatus.FreeToCapture),
		new Position(750, 550, PositionStatus.FreeToCapture)
	];

	setDots(dots) {
		this.dots = dots;
	}

	PawnClick = (e) => {
		if (this.CurrentMove == players.PlayerTwo) {
			return;
		}

		var dot = e.currentTarget;
		var validMoveCompleted = false;
		if (this.GameState == gameStates.PawnSetup) {
			if (dot.pos.status == PositionStatus.FreeToCapture) {
				dot.pos.status = PositionStatus.PlayerOne;
				validMoveCompleted = true;
				this.drawCircle(dot, colors.PlayerOne);
			}
		} else {
		}

		if (validMoveCompleted) {
			this.CurrentMove = players.PlayerTwo;
			this.GetAiMove();
		}
	};

	drawCircle = (dot, color) => {
		dot.clear();
		dot.beginFill(color, 1);
		dot.drawCircle(dot.pos.x, dot.pos.y, 8);
		dot.endFill();
	};

	GetAiMove = () => {
		var headers = {
			'Content-Type': 'application/json'
		};
		axios
			.post('https://localhost:5001/api/Picaria/Move', JSON.stringify(this.Positions), { headers: headers })
			.then((response) => {
				console.log(response);
				debugger;
			});
	};
}
