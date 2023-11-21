namespace api1.Dtos.CategoryDtos
{
    public class CategoryReturnDto
    {
        public string Name { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? DeleteDate { get; set; }
        public bool IsDelete { get; set; }
    }
}
