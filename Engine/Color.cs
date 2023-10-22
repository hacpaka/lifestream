namespace Lifestream.Engine;

public struct Color {
	public byte R {
		get; private set;
	}

	public byte G {
		get; private set;
	}

	public byte B {
		get; private set;
	}

	public byte A {
		get; private set;
	}

	public Color(byte r, byte g, byte b, byte a = 255) {
		R = r;
		G = g;
		B = b;
		A = a;
	}

	public static Color White() {
		return new Color(255, 255, 255);
	}

	public static Color Black() {
		return new Color(0, 0, 0);
	}
}