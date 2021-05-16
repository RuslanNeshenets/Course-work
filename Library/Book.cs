namespace MyLibrary
{
    public class Book
    {
        protected string name;
        private string author;
        private string subject;
        protected int copies = 1;
        public Book(string name, string author, string subject = null)
        {
            this.name = name;
            this.author = author;
            this.subject = subject;

        }
        public string Name { get { return name; } }
        public string Author { get { return author; } }
        public string Subject { get { return subject; } set { subject = value; } }
        public int Copies { get { return copies; } set { copies = value; } }
    }
}