using Lifestream.Engine;

namespace Lifestream {
	internal static class Program {
		private const uint INTERVAL = 100;

		private const uint WIDTH = 700;
		private const uint HEIGHT = 700;

		private static void Main() {
			new Window("Lifestream", WIDTH, HEIGHT, (index, _) => {
				Console.WriteLine($"==========> {index}");
			}, e => {
				Console.WriteLine($"[ERROR]: {e}");
			}, INTERVAL).Visualize();
		}
	}
}