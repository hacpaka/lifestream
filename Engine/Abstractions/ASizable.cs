namespace Lifestream.Engine.Abstractions;

public class ASizable {
	protected ASizable(uint width, uint height) {
		Width = width;
		Height = height;
	}

	public uint Width {
		get; private set;
	}

	public uint Height {
		get; private set;
	}
}