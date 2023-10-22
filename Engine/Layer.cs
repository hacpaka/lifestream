using Lifestream.Engine.Abstractions;

namespace Lifestream.Engine;

public class Layer: ASizable{
	private Color[,] Colors {
		get;
	}

	public Layer(uint width, uint height): base(width, height) {
		Colors = new Color[Width, Height];
	}

	public void Set(uint x, uint y, Color color) {
		if (x >= Width || y >= Height) {
			throw new Exception($"Invalid position: {x},{y}!");
		}

		Colors[x, y] = color;
	}

	public Color Get(uint x, uint y) {
		if (x >= Width || y >= Height) {
			throw new Exception($"Invalid position: {x},{y}!");
		}

		return Colors[x, y];
	}
}