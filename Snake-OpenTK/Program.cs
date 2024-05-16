using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Graphics.OpenGL;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace primitivs_1
{
    class Program
    {
        public class Game : GameWindow
        {
            private static int fieldSize = 10;
            private bool[,] field = new bool[fieldSize, fieldSize];
            private float frameTime = 0.0f;
            private int fps = 0;
            private int posX = 0;
            private int posY = 0;
            protected string title = "Snake-OpenTK";

            public Game(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings)
                : base(gameWindowSettings, nativeWindowSettings)
            {
                Console.WriteLine(GL.GetString(StringName.Version));
                Console.WriteLine(GL.GetString(StringName.Vendor));
                Console.WriteLine(GL.GetString(StringName.Renderer));
                Console.WriteLine(GL.GetString(StringName.ShadingLanguageVersion));

                VSync = VSyncMode.On;
            }

            protected override void OnLoad()
            {
                base.OnLoad();

                GL.ClearColor(173 / 255.0f, 216 / 255.0f, 230 / 255.0f, 1.0f);
            }

            protected override void OnResize(ResizeEventArgs e)
            {
                base.OnResize(e);
            }

            protected override void OnUpdateFrame(FrameEventArgs args)
            {
                if (posX < fieldSize - 1) posX++;

                frameTime += (float)args.Time;
                fps++;
                if (frameTime >= 1.0f)
                {
                    Title = $"{title} - {fps} FPS";
                    frameTime = 0.0f;
                    fps = 0;
                }

                var key = KeyboardState;
                string movementDirection = "right";

                if (key.IsKeyDown(Keys.Escape))
                {
                    movementDirection = "right";
                }

                if (key.IsKeyDown(Keys.W))
                {
                    movementDirection = "up";
                }
                else if (key.IsKeyDown(Keys.A))
                {
                    movementDirection = "left";
                }
                else if (key.IsKeyDown(Keys.S))
                {
                    movementDirection = "down";
                }
                else if (key.IsKeyDown(Keys.D))
                {
                    movementDirection = "right";
                }

                field[posX, posY] = true;
                field[posX - 1, posY] = false;

                if (posX == fieldSize - 1 && posY < fieldSize - 1) posY++;

                base.OnUpdateFrame(args);
            }

            protected override void OnRenderFrame(FrameEventArgs args)
            {



                GL.Clear(ClearBufferMask.ColorBufferBit);

                GL.PointSize(20.0f);

                GL.Begin(PrimitiveType.Points);

                GL.Color3(1.0f, 0.0f, 0.0f);



                for (int i = 0; i < fieldSize; i++)
                {
                    for (int j = 0; j < fieldSize; j++)
                    {
                        if (field[j, i] == true)
                        {
                            GL.Vertex2(j / 18.0f, i / 18.0f);
                        }
                    }
                }

                GL.End();

                SwapBuffers();
                base.OnRenderFrame(args);
            }

            protected override void OnUnload()
            {
                base.OnUnload();
            }
        }

        static void Main(string[] args)
        {
            var nativeWindowSettings = new NativeWindowSettings()
            {
                ClientSize = new Vector2i(800, 800),
                Location = new Vector2i(370, 300),
                WindowBorder = WindowBorder.Resizable,
                WindowState = WindowState.Normal,

                Flags = ContextFlags.Default,
                APIVersion = new Version(3, 3),
                Profile = ContextProfile.Compatability,
                API = ContextAPI.OpenGL,

                NumberOfSamples = 0
            };

            using (Game game = new Game(GameWindowSettings.Default, nativeWindowSettings))
            {
                game.Run();
            }
        }
    }
}