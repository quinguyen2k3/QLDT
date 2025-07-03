namespace QLDT.Model
{
        public class TrainingFormat : BaseEntity
        {
            public string Name { get; set; }
            public string Note { get; set; }
            public ICollection<Class> Classes { get; set; }
        }

}
