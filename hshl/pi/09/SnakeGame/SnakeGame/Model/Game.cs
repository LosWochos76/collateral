using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Threading;

namespace SnakeGame.Model;

public class Game : INotifyPropertyChanged
{
    private Random rnd = new Random();
    public event PropertyChangedEventHandler? PropertyChanged;

    public Snake Snake { get; set; }
    public ObservableCollection<Block> Apples { get; set; }

    private int level;
    public int Level 
    { 
        get { return level; }
        set
        {
            level = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Level)));
        }
    }

    private int score;
    public int Score
    {
        get { return score; }
        set
        {
            score = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Score)));
        }
    }

    public Game()
    {
        Snake = new Snake(this);
        Apples = new ObservableCollection<Block>();
        ResetApples();

        var dp = new DispatcherTimer();
        dp.Interval = new TimeSpan(0, 0, 0, 0, 200);
        dp.Tick += Dp_Tick;
        dp.Start();
    }

    private void ResetApples()
    {
        Apples.Clear();
        Level++;
        
        for (int i=0; i<Level+2; i++)
        {
            var new_x = rnd.Next(0, 79) * 10;
            var new_y = rnd.Next(0, 59) * 10;
            Apples.Add(new Block(new_x, new_y));
        }
    }

    private void Dp_Tick(object? sender, EventArgs e)
    {
        Snake.Move();
        CheckIfSnakeEatsApple();
    }

    private void CheckIfSnakeEatsApple()
    {
        Block to_be_removed = null;
        foreach (var apple in Apples)
        {
            if (Snake.Head.Equals(apple))
            {
                Snake.ExtendBody();
                Score++;
                to_be_removed = apple;
                break;
            }
        }

        if (to_be_removed != null)
        {
            Apples.Remove(to_be_removed);
        }

        if (Apples.Count == 0)
        {
            ResetApples();
        }
    }

    public void TurnSnake(Direction direction)
    {
        Snake.Turn(direction);
    }
}
