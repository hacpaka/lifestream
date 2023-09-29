namespace Lifestream.Engine;

struct SNode {
	public byte Water {
		get; set;
	}

	public SNode(byte water) {
		Water = water;
	}

	public static SNode Default {
		get {
			return new SNode(0);
		}
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

		for (int i = 0; i < width; i++) {
			for (int j = 0; j < height; j++) {
				data[i, j] = SNode.Default;
			}
		}
	}
}