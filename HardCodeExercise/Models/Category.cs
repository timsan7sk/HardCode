namespace HardCodeExercise.Models
{
	public class Category
	{
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Overview { get; set; }
        //public List<Field> Fields { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? DeleteDate { get; set; }
	}
}

