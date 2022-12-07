namespace dziennik
{
    public class NamedObject 
    {   
        public string Name {get; set;}
        public string Path { get; set; }

        public NamedObject(string name)
        {
            this.Name = name;
            this.Path = Name + ".txt";
        }
       
    }
}
