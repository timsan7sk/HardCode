namespace HardCodeExercise.Models
{
	public class Type
	{
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Overview { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? DeleteDate { get; set; }
	}
}

