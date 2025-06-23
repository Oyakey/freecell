extends Area2D

const objectType = "FREECELL";

var card: Area2D = null;

func canAppendCard():
	return card == null;
