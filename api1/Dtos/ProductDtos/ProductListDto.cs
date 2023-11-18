namespace api1.Dtos.ProductDtos
{
    public class ProductListDto
    {
        public int TotalCount { get; set; }
        public List<ProductReturnDto> Items { get; set; }
    }
}
