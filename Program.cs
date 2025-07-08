using System.Runtime.InteropServices;

namespace MouseMover
{
    class Program
    {
        // Import necessary Windows API functions for mouse control
        [DllImport("user32.dll")]
        static extern bool SetCursorPos(int x, int y);

        [DllImport("user32.dll")]
        static extern bool GetCursorPos(out POINT lpPoint);

        [StructLayout(LayoutKind.Sequential)]
        public struct POINT
        {
            public int X;
            public int Y;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct INPUT
        {
            public uint type;
            public InputUnion u;
        }

        [StructLayout(LayoutKind.Explicit)]
        public struct InputUnion
        {
            [FieldOffset(0)]
            public MOUSEINPUT mi;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct MOUSEINPUT
        {
            public int dx;
            public int dy;
            public uint mouseData;
            public uint dwFlags;
            public uint time;
            public IntPtr dwExtraInfo;
        }

        const uint INPUT_MOUSE = 0;
        const uint MOUSEEVENTF_MOVE = 0x0001;

        [DllImport("user32.dll", SetLastError = true)]
        static extern uint SendInput(uint nInputs, INPUT[] pInputs, int cbSize);

        private static readonly Random random = new Random();

        static async Task Main(string[] args)
        {
            Console.WriteLine("Mouse Mover - Keep Status Active");
            Console.WriteLine("================================");
            Console.WriteLine("Application started. Mouse will move every 20-30 seconds.");
            Console.WriteLine("Press Ctrl+C to stop the application.");
            Console.WriteLine();

            // Set up graceful shutdown
            Console.CancelKeyPress += (sender, e) =>
            {
                Console.WriteLine("\nShutting down gracefully...");
                Environment.Exit(0);
            };

            try
            {
                while (true)
                {
                    // Generate random interval between 20-30 seconds
                    int intervalSeconds = random.Next(20, 31);
                    
                    Console.WriteLine($"Waiting {intervalSeconds} seconds until next mouse movement...");
                    await Task.Delay(intervalSeconds * 1000);

                    // Move the mouse cursor slightly
                    MoveMouse();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                Console.WriteLine("Press any key to exit...");
                Console.ReadKey();
            }
        }

        static void MoveMouse()
        {
            try
            {
                // Simulate a small mouse move event (does not visibly move cursor)
                INPUT[] inputs = new INPUT[1];
                inputs[0].type = INPUT_MOUSE;
                inputs[0].u.mi = new MOUSEINPUT
                {
                    dx = 1, // move by 1 pixel
                    dy = 0,
                    mouseData = 0,
                    dwFlags = MOUSEEVENTF_MOVE,
                    time = 0,
                    dwExtraInfo = IntPtr.Zero
                };
                SendInput(1, inputs, Marshal.SizeOf(typeof(INPUT)));

                // Move back by -1 pixel to original position
                inputs[0].u.mi.dx = -1;
                SendInput(1, inputs, Marshal.SizeOf(typeof(INPUT)));

                Console.WriteLine($"Simulated mouse & input move event at {DateTime.Now:HH:mm:ss}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error simulating mouse move: {ex.Message}");
            }
        }
    }
}
