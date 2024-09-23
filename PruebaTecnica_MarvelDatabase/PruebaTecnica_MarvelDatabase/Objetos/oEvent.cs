namespace PruebaTecnica_MarvelDatabase.Objetos
{
    internal class oEvent
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime? Start { get; set; }
        public DateTime? End { get; set; }
        public int Total_Comics { get; set; }
    }
}
