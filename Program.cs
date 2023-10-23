using Lifestream.Engine;

namespace Lifestream {
	internal static class Program {
		private const uint INTERVAL = 200;

		private const uint SIZE = 10;
		private const uint WIDTH = 100;
		private const uint HEIGHT = 70;

		private static void Main() {
			new Window("Lifestream", WIDTH, HEIGHT, SIZE, canvas => {
				canvas.AddLayer();
				canvas.AddLayer();
			}, (index, canvas) => {
				Random random = new();

				Layer layer1 = canvas.GetLayer(0);
				for (uint i = 0; i < canvas.Width; i++) {
					for (uint j = 0; j < canvas.Height; j++) {
						layer1.Set(i, j, new Color(0, 0, (byte)random.Next(0, 255)));
					}
				}

				Layer layer2 = canvas.GetLayer(1);
				for (uint i = 0; i < canvas.Width; i++) {
					for (uint j = 0; j < canvas.Height; j++) {
						layer2.Set(i, j, new Color(0, (byte)random.Next(0, 255), 0, (byte)random.Next(0, 255)));
					}
				}

				Console.WriteLine($"==========> {index}");
			}, e => {

				Console.WriteLine($"[ERROR]: {e}");
			}, INTERVAL).Visualize();
		}
	}
}