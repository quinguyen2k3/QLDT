namespace QLDT.Model
{
        public class TrainingUnit : BaseEntity
        {
            public string Name { get; set; }
            public string Note { get; set; }
            public ICollection<Class> Classes { get; set; }
        }

}
