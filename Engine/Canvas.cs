using Lifestream.Engine.Abstractions;

namespace Lifestream.Engine;

public class Canvas: ASizable {
	private readonly List<Layer> layers = new();

	public int Size {
		get {
			return layers.Count;
		}
	}

	public Canvas(uint width, uint height): base(width, height) {
		if (Width < 1) {
			throw new Exception("Invalid canvas width!");
		}

		if (Height < 1) {
			throw new Exception("Invalid canvas height!");
		}
	}

	public void AddLayer() {
		layers.Add(new Layer(Width, Height));
	}

	public Layer GetLayer(int index) {
		if (index >= Size) {
			throw new Exception($"Invalid index: {index}!");
		}

		return layers[index];
	}

	public void Iterate(Action<uint, uint, Color> handler) {
		for (uint x = 0; x < Width; x++) {
			for (uint y = 0; y < Height; y++) {

				for (int i = 0; i < Size; i++) {
					handler(x, y, layers[i].Get(x, y));
				}
			}
		}
	}
}