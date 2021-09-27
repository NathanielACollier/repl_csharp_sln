namespace Avalonia_DataGrid_CellTemplateTesting.model
{
    public class Person : ViewModelBase
    {
        
        public string Firstname
        {
            get { return GetValue(() => Firstname); }
            set { SetValue(() => Firstname, value);}
        }

        public string Lastname
        {
            get { return GetValue(() => Lastname); }
            set { SetValue(() => Lastname, value);}
        }
        
    }
}