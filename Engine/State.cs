namespace Lifestream.Engine;

struct SNode {
	public byte Water {
		get; set;
	}

	public byte Resource {
		get; set;
	}

	public SNode(byte water, byte resource) {
		Water = water;
		Resource = resource;
	}
}

public class State {
	private SNode[,] data;

	public uint Width {
		get;
	}

	public uint Height {
		get;
	}

	public State(uint width, uint height) {
		if (width < 10 || width > 1000) {
			throw new Exception("Invalid width!");
		}

		Width = width;

		if (height < 10 || height > 1000) {
			throw new Exception("Invalid height!");
		}

		Height = height;

		data = new SNode[width, height];
		Random random = new();

		for (int i = 0; i < width; i++) {
			for (int j = 0; j < height; j++) {

				data[i, j] = new SNode((byte)random.Next(0, 255), (byte)random.Next(0, 255));
			}
		}
	}
}