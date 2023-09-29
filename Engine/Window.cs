namespace Lifestream.Engine;

using SDL2;

public class Window {
	private string Title {
		get;
	}

	private Action<long, uint> OnUpdate {
		get;
	}

	private Action<Exception> OnError {
		get;
	}

	private uint Interval {
		get;
	}

	private long Index {
		get; set;
	}

	public Window(string title, uint interval, Action<long, uint> onUpdate, Action<Exception> onError) {
		if (interval < 10 || interval > 1000) {
			throw new Exception("Invalid interval!");
		}

		Interval = interval;
		Title = title;

		if (SDL.SDL_Init(SDL.SDL_INIT_VIDEO | SDL.SDL_INIT_TIMER) > 0) {
			throw new Exception($"Initialization failed! {SDL.SDL_GetError()}");
		}

		OnUpdate = onUpdate;
		OnError = onError;
	}

	public void Visualize() {
		IntPtr window = SDL.SDL_CreateWindow(Title,
			SDL.SDL_WINDOWPOS_CENTERED,
			SDL.SDL_WINDOWPOS_CENTERED,
			700,
			700,
			SDL.SDL_WindowFlags.SDL_WINDOW_BORDERLESS);

		if (window == IntPtr.Zero) {
			throw new Exception($"Failed to create a window! {SDL.SDL_GetError()}");
		}

		int timer = SDL.SDL_AddTimer(Interval, (interval, _) => {
			try {
				OnUpdate.Invoke(++Index, interval);
			} catch (Exception e) {
				OnError.Invoke(e);
			}

			Console.WriteLine($"######### {interval}");
			return interval;
		}, window);

		IntPtr renderer = SDL.SDL_CreateRenderer(window, -1,
			SDL.SDL_RendererFlags.SDL_RENDERER_ACCELERATED | SDL.SDL_RendererFlags.SDL_RENDERER_PRESENTVSYNC);

		bool quit = false;
		while (!quit) {
			Draw(renderer);

			while (SDL.SDL_PollEvent(out SDL.SDL_Event e) != 0) {
				switch (e.type) {
					case SDL.SDL_EventType.SDL_QUIT:
						quit = true;
						break;

					case SDL.SDL_EventType.SDL_KEYDOWN:
						switch (e.key.keysym.sym) {
							case SDL.SDL_Keycode.SDLK_q:
								quit = true;
								break;
						}

						break;
				}
			}
		}

		SDL.SDL_RemoveTimer(timer);
		SDL.SDL_DestroyRenderer(renderer);
		SDL.SDL_DestroyWindow(window);
		SDL.SDL_Quit();
	}

	private void Draw(IntPtr renderer) {
		SDL.SDL_SetRenderDrawColor(renderer, 135, 206, 235, 255);
		SDL.SDL_RenderClear(renderer);
		SDL.SDL_RenderPresent(renderer);
	}
}