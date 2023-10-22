namespace Lifestream.Engine;

using SDL2;

public class Window {
	private Canvas Canvas {
		get;
	}

	private string Title {
		get;
	}

	private Action<long, Canvas> OnUpdate {
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

	private uint Width {
		get;
	}

	private uint Height {
		get;
	}

	private State State {
		get;
	}

	public uint Size {
		get;
	}

	public Window(string title, uint width, uint height, uint size, Action<Canvas> onStart, Action<long, Canvas> onUpdate, Action<Exception> onError, uint interval) {
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

		Width = width;
		Height = height;
		Size = size;

		Canvas = new Canvas(width / size, height / size);
		onStart.Invoke(Canvas);
	}

	public void Visualize() {
		IntPtr window = SDL.SDL_CreateWindow(Title,
			SDL.SDL_WINDOWPOS_CENTERED,
			SDL.SDL_WINDOWPOS_CENTERED,
			(int)Width,
			(int)Height,
			SDL.SDL_WindowFlags.SDL_WINDOW_BORDERLESS);

		if (window == IntPtr.Zero) {
			throw new Exception($"Failed to create a window! {SDL.SDL_GetError()}");
		}

		int timer = SDL.SDL_AddTimer(Interval, (interval, _) => {
			try {
				OnUpdate.Invoke(++Index, Canvas);
			} catch (Exception e) {
				OnError.Invoke(e);
			}

			return interval;
		}, window);

		IntPtr renderer = SDL.SDL_CreateRenderer(window, -1,
			SDL.SDL_RendererFlags.SDL_RENDERER_ACCELERATED | SDL.SDL_RendererFlags.SDL_RENDERER_PRESENTVSYNC);

		SDL.SDL_SetRenderDrawBlendMode(renderer, SDL.SDL_BlendMode.SDL_BLENDMODE_BLEND);

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
		SDL.SDL_RenderClear(renderer);

		Canvas.Iterate((x, y, color) => {
			SDL.SDL_SetRenderDrawColor(renderer, color.R, color.G, color.B, color.A);

			SDL.SDL_Rect rect = new() {
				x = (int)(x * (int)Size),
				y = (int)(y * (int)Size),
				w = (int)Size,
				h = (int)Size
			};

			SDL.SDL_RenderFillRect(renderer, ref rect);
		});

		SDL.SDL_RenderPresent(renderer);
	}
}