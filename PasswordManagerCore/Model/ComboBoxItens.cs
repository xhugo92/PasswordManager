namespace PasswordManagerCore.Model
{
    public class ComboBoxItens
    {
        public ComboBoxItens(string name, bool isSelected, FiltertypeEnum filtertype)
        {
            Name = name;
            IsSelected = isSelected;
            FilterType = filtertype;
        }
        public string Name { get; set; }

        public bool IsSelected { get; set; }

        public FiltertypeEnum FilterType { get; set; }


    }

    public enum FiltertypeEnum
    {
        Source,
        Username
    }
}
