namespace TaskManagement.Domain.Values.Objects
{
    public class Include
    {
        public List<string> Includes { get; }

        public Include()
        {
            Includes = new List<string>();
        }

        public Include(params string[] includes)
            : this()
        {
            Includes.AddRange(includes);
        }

        public static Include New(params string[] parameters)
        {
            return new Include(parameters);
        }
    }
}
