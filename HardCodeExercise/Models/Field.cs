namespace HardCodeExercise.Models
{
	public class Field
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public string Overview { get; set; }
		public Guid CategoryId { get; set; }
		//public Type Type { get; set; }
		public Guid TypeId { get; set; }
		public DateTime CreateDate { get; set; }
		public DateTime? DeleteDate { get; set; }
	}
}

