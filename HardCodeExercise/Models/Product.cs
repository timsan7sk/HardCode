namespace HardCodeExercise.Models
{
	public class Product
	{
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Overview { get; set; }
        //public Category Category { get; set; }
        public Guid CategoryId { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? DeleteDate { get; set; }
	}
}

