using Lifestream.Engine;

namespace Lifestream {
	internal static class Program {
		private static void Main() {
			new Window("Lifestream", 100, (index, _) => {
				Console.WriteLine($"==========> {index}");
			}).Visualize();
		}
	}
}