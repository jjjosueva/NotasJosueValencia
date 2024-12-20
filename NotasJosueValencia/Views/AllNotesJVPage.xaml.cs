namespace NotasJosueValencia.Views;

public partial class AllNotesJVPage : ContentPage
{
    public AllNotesJVPage()
    {
        InitializeComponent();
    }

    private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
        notesCollection.SelectedItem = null;
    }
}
