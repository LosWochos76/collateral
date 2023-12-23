using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Avalonia.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Splat;

namespace GraphSearch;

public partial class MainWindowViewModel : ObservableObject
{
    public ObservableCollection<Block> Items { get; private set; } = new();
    public ObservableCollection<string> Algorithms { get; private set; } = new() { "Breitensuche", "A*" };
    public Block Start { get; set; }
    public Block Destination { get; set; }
    private IAlgorithm algo;

    [ObservableProperty]
    private string selectedAlgorithms = "Breitensuche";

    public IEnumerable<Block> GetNeighborsOf(Block b)
    {
        var result = new List<Block>();
        foreach (var block in Items)
            if (b.Color != Colors.Black && block.IsNeighborOf((b)))
                result.Add(block);
        
        return result;
    }

    public MainWindowViewModel()
    {
        for (int x=0; x<30; x++)
            for (int y=0; y<30; y++)
                Items.Add(new Block(x, y));

        Init();
    }

    private void Init()
    {
        foreach (var b in Items)
            b.Color = Colors.White;
        
        Random rnd = new Random();
        
        for (int i = 0; i < 400; i++)
            Items[rnd.Next(Items.Count)].Color = Colors.Black;
        
        Start = Items[rnd.Next(Items.Count)];
        Start.Color = Colors.Blue;
        Destination = Items[rnd.Next(Items.Count)];
        Destination.Color = Colors.Green;
    }

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(RunCommand))]
    private bool isStopped = true;

    [RelayCommand(CanExecute=nameof(IsStopped))]
    public void Run()
    {
        if (algo != null)
            algo.Stop();
        
        if (SelectedAlgorithms == "Breitensuche") ;
            algo = new BreadthFirstSearch(this);

        if (SelectedAlgorithms == "A*")
            algo = new AStar(this);
            
        algo.Run();
    }
    
    [RelayCommand]
    public void Generate()
    {
        if (algo != null)
            algo.Stop();

        Init();
    }

    [RelayCommand]
    public void Reset()
    {
        if (algo != null)
            algo.Stop();
        
        foreach (var block in Items)
            if (block.Color != Colors.Black)
                block.Color = Colors.White;
        
        Start.Color = Colors.Blue;
        Destination.Color = Colors.Green;
    }
}