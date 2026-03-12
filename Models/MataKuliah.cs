namespace SttbApi.Models
{
    public class MataKuliah
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Credits { get; set; }

        public int ProgramStudiId { get; set; }

        public ProgramStudi? ProgramStudi { get; set; }
    }
}
