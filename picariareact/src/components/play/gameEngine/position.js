export const PositionStatus = {
	PlayerOne: 'playerOne',
	PlayerTwo: 'playerTwo',
	FreeToCapture: 'freetocapture'
};

export class Position {
	constructor(x, y, status) {
		this.x = x;
		this.y = y;
		this.status = status;
	}

	changeStatus = (status) => {
		this.status = status;
	};
}
