using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace SeminarManager;

public partial class SeminarViewModel : ObservableValidator
{
    private NavigationStore navigation;
    private DataRepository repository;
    private Seminar model;

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [Required]
    [MinLength(3)]
    [NotifyCanExecuteChangedFor(nameof(OkCommand))]
    private string name = string.Empty;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(DozentName))]
    private Person dozent;

    public ObservableCollection<Person> Teilnehmer { get; private set; } = new ObservableCollection<Person>();
    public ObservableCollection<Person> Persons { get; set; } = new ObservableCollection<Person>();
    public PersonMultiSelectViewModel MultiSelect { get; set; }

    public string DozentName
    {
        get
        {
            return dozent != null ? dozent.Vorname + " " + dozent.Nachname : string.Empty;
        }
    }

    public bool HasNoErrors { get { return !HasErrors; } }

    [RelayCommand(CanExecute=nameof(HasNoErrors))]
    public void Ok()
    {
        model.Name = Name;
        model.Dozent = Dozent;

        model.Teilnehmer.Clear();
        foreach (var obj in MultiSelect.SelectedElements)
            model.Teilnehmer.Add(obj);

        repository.Seminars.Save(model);
        navigation.NavigateTo(new SeminarListViewModel(navigation, repository));
    }

    [RelayCommand]
    public void Cancel()
    {
        navigation.NavigateTo(new SeminarListViewModel(navigation, repository));
    }

    public SeminarViewModel(NavigationStore navigation, DataRepository repository, Seminar model)
    {
        this.navigation = navigation;
        this.repository = repository;
        this.model = model;

        foreach (var obj in repository.Persons.Elements)
            Persons.Add(obj);

        Name = model.Name;
        Dozent = model.Dozent;

        foreach (var obj in model.Teilnehmer)
            Teilnehmer.Add(obj);

        MultiSelect = new PersonMultiSelectViewModel(repository, Teilnehmer);

        ValidateAllProperties();
    }
}