using CommunityToolkit.Mvvm.Input;
using NotasJosueValencia.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace NotasJosueValencia.ViewModels;

internal class NotesJVViewModel : IQueryAttributable
{
    public ObservableCollection<ViewModels.NoteJVViewModel> AllNotes { get; }
    public ICommand NewCommand { get; }
    public ICommand SelectNoteCommand { get; }

    public NotesJVViewModel()
    {
        AllNotes = new ObservableCollection<ViewModels.NoteJVViewModel>(Models.Note.LoadAll().Select(n => new NoteJVViewModel(n)));
        NewCommand = new AsyncRelayCommand(NewNoteAsync);
        SelectNoteCommand = new AsyncRelayCommand<ViewModels.NoteJVViewModel>(SelectNoteAsync);
    }

    private async Task NewNoteAsync()
    {
        await Shell.Current.GoToAsync(nameof(Views.NoteJVPage));
    }

    private async Task SelectNoteAsync(ViewModels.NoteJVViewModel note)
    {
        if (note != null)
            await Shell.Current.GoToAsync($"{nameof(Views.NoteJVPage)}?load={note.Identifier}");
    }

    void IQueryAttributable.ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.ContainsKey("deleted"))
        {
            string noteId = query["deleted"].ToString();
            NoteJVViewModel matchedNote = AllNotes.Where((n) => n.Identifier == noteId).FirstOrDefault();

            // If note exists, delete it
            if (matchedNote != null)
                AllNotes.Remove(matchedNote);
        }
        else if (query.ContainsKey("saved"))
        {
            string noteId = query["saved"].ToString();
            NoteJVViewModel matchedNote = AllNotes.Where((n) => n.Identifier == noteId).FirstOrDefault();

            // If note is found, update it
            if (matchedNote != null)
            {
                matchedNote.Reload();
                AllNotes.Move(AllNotes.IndexOf(matchedNote), 0);
            }
            // If note isn't found, it's new; add it.
            else
                AllNotes.Insert(0, new NoteJVViewModel(Models.Note.Load(noteId)));
        }
    }
}
