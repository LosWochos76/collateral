using SnakeGame.Model;
using System.Windows;

namespace SnakeGame;

public partial class MainWindow : Window
{
    public Game Game { get; set; }

    public MainWindow()
    {
        InitializeComponent();

        Game = new Game();
        DataContext = Game;
    }

    private void Window_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
    {
        if (e.Key == System.Windows.Input.Key.Left)
            Game.TurnSnake(Direction.Left);

        if (e.Key == System.Windows.Input.Key.Right)
            Game.TurnSnake(Direction.Right);

        if (e.Key == System.Windows.Input.Key.Up)
            Game.TurnSnake(Direction.Up);

        if (e.Key == System.Windows.Input.Key.Down)
            Game.TurnSnake(Direction.Down);

        if (e.Key == System.Windows.Input.Key.Escape)
            Application.Current.Shutdown();
    }
}
