using Microsoft.Xna.Framework;
using System;

public class Timer
{
    private double elapsedTime;
    private bool isRunning;

    public void Start()
    {
        isRunning = true;
    }

    public void Pause()
    {
        isRunning = false;
    }

    public void Reset()
    {
        elapsedTime = 0;
        isRunning = false;
    }
    public void Update(GameTime gameTime)
    {
        if (isRunning)
        {
            elapsedTime += gameTime.ElapsedGameTime.TotalMilliseconds;
        }
    }

    public string GetFormattedTime()
    {
        int totalMilliseconds = (int)elapsedTime;
        int minutes = totalMilliseconds / 60000;
        int seconds = (totalMilliseconds / 1000) % 60;
        int milliseconds = totalMilliseconds % 1000;

        return $"{minutes:D2}:{seconds:D2}:{milliseconds:D3}";
    }
}