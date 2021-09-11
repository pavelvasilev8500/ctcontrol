namespace ControlLibrary.Data
{
    public class Data
    {
        public bool LanguageSwither { get; set; }
        public int Id { get; set; }
        public bool Switcher{get; set;}

        public Data(bool languageSwither, int id , bool swither)
        {
            LanguageSwither = languageSwither;
            Id = id;
            Switcher = swither;
        }
    }
}
