# Mouse Mover - Messenger Status Keeper

A simple C# console application that moves the mouse cursor periodically to keep your messengers aka Microsoft Teams status active (green).

## Features

- Moves mouse cursor at random intervals between 20-30 seconds
- Moves cursor slightly (±5 pixels) from current position
- Returns cursor to original position after movement
- Graceful shutdown with Ctrl+C
- Console logging of all mouse movements

## How It Works

1. The application runs continuously in the background
2. Every 20-30 seconds (random interval), it:
   - Gets the current mouse cursor position
   - Moves it slightly (±5 pixels in X and Y directions)
   - Immediately returns it to the original position
3. This simulates user activity, keeping messenger status active

## Building and Running

### Prerequisites
- .NET 6.0 or later
- Windows operating system (uses Windows API)

### Build
```powershell
dotnet build
```

### Run
```powershell
dotnet run
```

### Run as Release
```powershell
dotnet run --configuration Release
```

## Usage

1. Start the application
2. Minimize the console window
3. Let it run
4. Press Ctrl+C to stop the application when done

## Important Notes

- This application only works on Windows (uses Windows API)
- Keep the console window open (can be minimized)
- The mouse movements are very small and shouldn't interfere with normal usage
- Use responsibly and in accordance with your organization's policies

## Safety Features

- Movements are limited to ±5 pixels to avoid disrupting work
- Cursor returns to original position immediately
- Screen boundary checks prevent cursor from going off-screen
- Graceful shutdown with Ctrl+C

## Disclaimer

This tool is for personal productivity and should be used responsibly. Make sure to comply with your organization's policies regarding automated tools.
