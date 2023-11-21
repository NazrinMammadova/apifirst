using api1.Dtos.ProductDtos;

namespace api1.Dtos.CategoryDtos
{
    public class CategoryListDto
    {
        public int TotalCount { get; set; }
        public int Page { get; set; }
        public List<CategoryReturnDto> Items { get; set; }
    }
}
